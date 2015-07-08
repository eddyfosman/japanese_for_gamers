using UnityEngine;
using System.Collections.Generic;

namespace EnhancedUI
{
    /// <summary>
    /// Delegates to pass item selection events to the controller. 
    /// These are optional.
    /// 
    /// ItemSelectedDelegate: used when you don't need to know the sub-component selected, just references the entire list item
    /// ItemSelectedIntDelegate: used for item components with an integer argument
    /// ItemSelectedFloatDelegate: used for item components with an float argument
    /// ItemSelectedStringDelegate: used for item components with an string argument
    /// </summary>
    public delegate void ItemSelectedDelegate(ListItemBase listItemBase);
    public delegate void ItemSelectedIntDelegate(ListItemBase listItemBase, int arg);
    public delegate void ItemSelectedFloatDelegate(ListItemBase listItemBase, float arg);
    public delegate void ItemSelectedStringDelegate(ListItemBase listItemBase, string arg);

    /// <summary>
    /// This class controls the enhanced scroller functionality. The scroller will recycle
    /// items and only create new ones as needed, never destroying them to prevent garbage
    /// collection.
    /// 
    /// Note that the data index can usually be passed as a float parameter in most of the member
    /// functions. This allows you to set the scroll position partway through a single UI element.
    /// For instance, if you pass a data index of 5.2f to JumpToIndex, this will move the scroll
    /// position to the sixth element (zero based array > index 5 is the sixth element), and 20%
    /// inside that element.
    /// 
    /// You can have list items with variable sizes by passing a list of sizes in the Reload function.
    /// However, constant sized list items will have less calculation overhead and should be considered 
    /// if you are experiencing lags while scrolling.
    /// </summary>
    public class EnhancedScroller : MonoBehaviour 
    {
        /// <summary>
        /// Callback for when an item is selected. This is set in your controller
        /// </summary>
        public ItemSelectedDelegate itemSelected;

        /// <summary>
        /// Callback for when an item is selected with an int argument. This is set in your controller
        /// </summary>
        public ItemSelectedIntDelegate itemSelectedInt;

        /// <summary>
        /// Callback for when an item is selected with an float argument. This is set in your controller
        /// </summary>
        public ItemSelectedFloatDelegate itemSelectedFloat;

        /// <summary>
        /// Callback for when an item is selected with an string argument. This is set in your controller
        /// </summary>
        public ItemSelectedStringDelegate itemSelectedString;

        /// <summary>
        /// The possible values of the scrollbar's visibility
        /// </summary>
        public enum ScrollbarDisplayMode
        {
            ShowIfRelevant,     // Only shows if the list items are too large to fit in the scroll area
            Always,             // Always shows the scrollbar, even if the list items are entirely in the scroll area
            Never               // Never shows the scrollbar. This is good for when you are looping since the scrollbar is meaningless in this scenario.
        }

        /// <summary>
        /// Pointer to the scroll rect component
        /// </summary>
        public UnityEngine.UI.ScrollRect scrollRect;

        /// <summary>
        /// Pointer to the list item container
        /// </summary>
        public RectTransform listItemContainer;

        /// <summary>
        /// Pointer to the scrollbar
        /// </summary>
        public UnityEngine.UI.Scrollbar scrollbar;

        /// <summary>
        /// Pointer to the list item prefab used for this scroller
        /// </summary>
        public GameObject listItemPrefab;

        /// <summary>
        /// How the scrollbar should be displayed
        /// </summary>
        public ScrollbarDisplayMode scrollbarDisplayMode;

        /// <summary>
        /// Whether or not the scroller should loop the data infinitely
        /// </summary>
        public bool loop;

        /// <summary>
        /// Sets the scroller's data from the controller. This overload uses a constant
        /// value for the list item size which is faster when calculating item position.
        /// </summary>
        /// <param name="data">A list of objects to be used</param>
        /// <param name="listItemSize">A fixed item size for every element</param>
        /// <param name="startDataIndex">Where the scroller should start displaying</param>
        public void Reload(List<object> data, float listItemSize,
                            float startDataIndex = 0)
        {
            // set the internal data list
            _data = data;

            // set the constant size of the list items
            _constantSize = listItemSize;

            // Set the size of the list item container
            SetListItemContainer();

            // Jump to the correct place in the scroll area, based on the start data index
            JumpToIndex(startDataIndex);
        }

        /// <summary>
        /// Sets the scroller's data from the controller. This overload allows you to pass a list of
        /// item sizes which will be used to calculate positions of each ui list item element. There is
        /// more overhead in calculating positions using this method than just passing a constant
        /// list item size.
        /// </summary>
        /// <param name="data">A list of objects to be used</param>
        /// <param name="listItemSizes">A list of item sizes for the scroller. This list should be the same length as the data list length. A separate list is used to allow you to pass different sizes for different scroll lists while keeping the data consistent.</param>
        /// <param name="startDataIndex">Where the scroller should start displaying</param>
        public void Reload(List<object> data, List<float> listItemSizes,
                            float startDataIndex = 0)
        {
            // Turn off constant calculations
            _constantSize = -1f;

            // Check to make sure that the data and list item size arrays are the same length
            if (data.Count != listItemSizes.Count)
            {
                Debug.LogError("The data (" + data.Count.ToString() + ") and listItemSizes (" + listItemSizes.Count.ToString() + ") counts are not equal");
                return;
            }

            // set the internal data and item size lists
            _data = data;
            _listItemSizes = listItemSizes;

            // reset the item offsets. Note this does not dealloc because 
            // it is using the SmallList class, so no garbage collection
            // will occur.
            _listItemOffsets.Clear();
            float farSide = 0;
            for (int i = 0; i < _listItemSizes.Count; i++)
            {
                farSide += _listItemSizes[i];
                _listItemOffsets.Add(farSide);
            }

            // Calculate the number of jump markers to use. A jump marker
            // is set every 1000 data items
            float jumpMarkerCount = ((float)_data.Count / (float)DATA_INDICES_PER_JUMP_MARKER);

            if (jumpMarkerCount < 0)
            {
                // There were fewer than 1000 data items, so no jump
                // markers will be used
                _jumpMarkerCount = 0;
            }
            else
            {
                // Set the internal jump marker count for this data set
                _jumpMarkerCount = Mathf.FloorToInt(jumpMarkerCount);

                // Add new jump markers to the list if necessary
                for (int i = _jumpMarkers.Count; i < _jumpMarkerCount; i++)
                {
                    _jumpMarkers.Add(new JumpMarker() { dataIndex = 0, offset = 0 });
                }

                // Set the jump markers at every 1000 data items, calculating the offset at
                // that item.
                for (int i = 0; i < _jumpMarkerCount; i++)
                {
                    _jumpMarkers[i].dataIndex = Mathf.FloorToInt(_data.Count / _jumpMarkerCount) * i;
                    _jumpMarkers[i].offset = (_jumpMarkers[i].dataIndex == 0 ? 0 : _listItemOffsets[_jumpMarkers[i].dataIndex - 1]);
                }
            }

            // Set the size of the list item container
            SetListItemContainer();

            // Jump to the correct place in the scroll area, based on the start data index
            JumpToIndex(startDataIndex);
        }

        /// <summary>
        /// Moves the scroll are to the given data index. Note the data index 
        /// can be a fraction. i.e. 5.2 will move to data index 5, with an extra
        /// offset of 20% of index 5's size.
        /// </summary>
        /// <param name="index"></param>
        public void JumpToIndex(float dataIndex)
        {
            // Turn off the scroller to avoid jumping around
            _scrollActive = false;

            // If the the list item container is larger than the scroll area
            if (_ListItemContainerSize > _ScrollRectSize)
            {
                // If looping is set, determine the middle section UID
                if (loop) dataIndex = (dataIndex % _data.Count) + _data.Count;

                // Set the scrollbar's scroll value
                scrollbar.value = CalculateScrollPosition(dataIndex);
            }

            // Reset the cached index values
            _lastFirstDataUIDInt = -1;
            _lastLastDataUIDInt = -1;

            // Refresh the UI elements and their data
            Refresh(true);

            // Turn the scrolling back on
            _scrollActive = true;
        }

        /// <summary>
        /// Turns the mask on and off. Having the mask off is useful to
        /// see the recycling at work, but probably won't be used in 
        /// a live project.
        /// </summary>
        /// <param name="active"></param>
        public void ToggleMask(bool active)
        {
            if (_scrollRectMask == null) return;
            _scrollRectMask.enabled = active;
        }

        /// <summary>
        /// Turns the looping on and off. When looping is one, the list item
        /// container size is three times as large to accomodate wrapping around
        /// both ends. If going from not looping to looping, the scroll position
        /// will be maintained. If going from looping to not looping, the list
        /// has to jump back to the start in case the scroll is somewhere near
        /// the wraparound.
        /// </summary>
        /// <param name="active"></param>
        public void ToggleLoop(bool active)
        {
            // Only make modifications if there is a change
            if (loop != active)
            {
                // Set the looping
                loop = active;

                // Set the new list item container size
                SetListItemContainer();

                // Jump to the appropriate spot. If going to looping, the middle set UID is used
                JumpToIndex(loop ? _firstDataUID + _data.Count : _firstDataUID % _data.Count);
            }
        }

        /// <summary>
        /// Turns the scrollbar visibility to one of the enum values
        /// </summary>
        /// <param name="displayMode">The visibility mode of the scrollbar</param>
        public void SetScrollBarDisplayMode(ScrollbarDisplayMode displayMode)
        {
            scrollbarDisplayMode = displayMode;
            SetScrollBarVisibility((_ListSize < _ScrollRectSize) ? scrollbarDisplayMode == ScrollbarDisplayMode.Always : scrollbarDisplayMode != ScrollbarDisplayMode.Never);
        }

        #region Internal

        private const int DATA_INDICES_PER_JUMP_MARKER = 1000;

        /// <summary>
        /// This class speeds up calculating the index of very large data sets
        /// that have non-uniform size elements. A jump marker is set every 1000
        /// data items which narrows down the search and reduces calculation time.
        /// 
        /// 1000 data items is just an arbitrary number to balance between doing
        /// too many quick jump lookups versus going through every element in the 
        /// offset list. To change this, just change the constant value above:
        /// DATA_INDICES_PER_JUMP_MARKER to whatever suits your needs.
        /// </summary>
        private class JumpMarker
        {
            public int dataIndex;
            public float offset;
        }

        /// <summary>
        /// A list of jump markers that helps speed up looking for a particular index
        /// given an offset.
        /// </summary>
        private SmallList<JumpMarker> _jumpMarkers = new SmallList<JumpMarker>();

        /// <summary>
        /// The number of jump markers being used. This value allows the scroller to
        /// recycle the _jumpMarkers list without garbage collection every time the 
        /// data is reloaded.
        /// </summary>
        private int _jumpMarkerCount = 0;

        /// <summary>
        /// The internal reference to the data passed
        /// </summary>
        private List<object> _data = new List<object>();

        /// <summary>
        /// The internal reference to the item sizes passed. If a single size
        /// is passed, this list will be populated all with that size.
        /// </summary>
        private List<float> _listItemSizes = new List<float>();

        /// <summary>
        /// The constant size of the list items. If this is not used, then a value of
        /// -1 is set to turn off the constant calculations.
        /// </summary>
        private float _constantSize = -1f;

        /// <summary>
        /// The internal location of the far side of each item for faster
        /// calculations
        /// </summary>
        private SmallList<float> _listItemOffsets = new SmallList<float>();

        private SmallList<int> _recycleItems = new SmallList<int>();
        private SmallList<int> _inuseItems = new SmallList<int>();

        /// <summary>
        /// The internal list of UI elements created
        /// </summary>
        private SmallList<ListItemBase> _listItems = new SmallList<ListItemBase>();

        /// <summary>
        /// Internal reference to the scroll rect's transform
        /// </summary>
        private RectTransform _scrollRectTransform;

        /// <summary>
        /// The internal reference to the scroll rect's mask. This
        /// is toggled with the public function ToggleMask
        /// </summary>
        private UnityEngine.UI.Mask _scrollRectMask;

        // TODO: Remove
        /// <summary>
        /// The internal reference to the scroll bar's rect transform
        /// </summary>
        //private RectTransform _scrollBarRectTransform;

        /// <summary>
        /// The first data index visible in the list (top for vertical scrollers,
        /// left for horizontal scrollers). Note that this value is a float, so
        /// it could be a fraction of an item. i.e. 5.2 would be the fifth data element,
        /// with 20% of that element partially hidden by the scroll mask.
        /// </summary>
        private float _firstDataUID;

        /// <summary>
        /// Cached value of the last first element used to determine if something
        /// changed during a scroll.
        /// </summary>
        private int _lastFirstDataUIDInt;

        /// <summary>
        /// Cached value of the last final element used to determine if something
        /// changed during a scroll.
        /// </summary>
        private int _lastLastDataUIDInt;

        /// <summary>
        /// If this is true, the scroll event is processed.
        /// </summary>
        private bool _scrollActive = false;

        // TODO: Remove
        /// <summary>
        /// The original size of the scroll bar. This is used to
        /// hide the scrollbar when necessary
        /// </summary>
        //private Vector2 _originalScrollBarSize;

        /// <summary>
        /// The last size of the scroll area. This is used to determine
        /// if more list items need to be created when the area is resized.
        /// </summary>
        private float _lastScrollRectSize = -1;

        /// <summary>
        /// The size of the list item container UI element
        /// </summary>
        private float _ListItemContainerSize { get { return (_VerticalScrolling ? listItemContainer.sizeDelta.y : listItemContainer.sizeDelta.x); } }

        /// <summary>
        /// The size of the scroll area UI element
        /// </summary>
        private float _ScrollRectSize { get { return (_VerticalScrolling ? _scrollRectTransform.rect.height : _scrollRectTransform.rect.width); } }

        /// <summary>
        /// The total size of all the list items (ignoring looping)
        /// </summary>
        private float _ListSize 
        { 
            get 
            {
                if (_constantSize != -1f)
                {
                    return _data.Count * _constantSize;
                }
                else
                {
                    if (_listItemOffsets == null) return 0; return _listItemOffsets[_listItemOffsets.Count - 1];
                }
            } 
        }

        /// <summary>
        /// Whether or not this is a vertical scroller
        /// </summary>
        private bool _VerticalScrolling { get { return (scrollbar.direction == UnityEngine.UI.Scrollbar.Direction.BottomToTop || scrollbar.direction == UnityEngine.UI.Scrollbar.Direction.TopToBottom); } }

        /// <summary>
        /// Cache some values for faster reference later
        /// </summary>
        void Awake()
        {
            _scrollRectTransform = scrollRect.GetComponent<RectTransform>();
            _scrollRectMask = scrollRect.GetComponent<UnityEngine.UI.Mask>();
            // TODO: Remove
            //_scrollBarRectTransform = scrollbar.GetComponent<RectTransform>();
            //_originalScrollBarSize = _scrollBarRectTransform.sizeDelta;
            _lastScrollRectSize = _ScrollRectSize;

            SetScrollBarVisibility(scrollbarDisplayMode == ScrollbarDisplayMode.Always);
        }

        /// <summary>
        /// Checks for size changes in the scroll area. If one is detected,
        /// then the list is refreshed
        /// </summary>
        void Update()
        {
            if (_ScrollRectSize != _lastScrollRectSize)
            {
                Refresh(false);

                _lastScrollRectSize = _ScrollRectSize;
            }
        }

        /// <summary>
        /// This is called whenever the scrollbar is scrolled (which also
        /// happens when the scroll area is swiped.
        /// </summary>
        /// <param name="value">The value of the scrollbar scroll</param>
        public void ScrollbarScrolled(float value)
        {
            // Do some checks to make sure we can scroll to avoid errors
            if (!Application.isPlaying) return;
            if (_data == null) return;
            if (_data.Count == 0) return;
            if (_ScrollRectSize > _ListItemContainerSize) return;
            if (!_scrollActive) return;

            Refresh(false);
        }

        /// <summary>
        /// Updates the UI elements, creating new ones if necessary and setting
        /// the data of any data indices that come in range.
        /// </summary>
        private void Refresh(bool forceSetData)
        {
            // Calculate the first and last data index based on the scroll position
            _firstDataUID = CalculateFirstScrollUID();
            int firstDataUIDInt = Mathf.FloorToInt(_firstDataUID);
            int lastDataUIDInt = Mathf.Min(Mathf.FloorToInt(CalculateLastScrollUID(firstDataUIDInt)), (loop ? _data.Count * 3 : _data.Count) - 1);

            // Only make changes if the first or last index has changed. This can
            // happen if the scroll area is scrolled, or the scroll area's size
            // has changed
            if (firstDataUIDInt != _lastFirstDataUIDInt
                ||
                lastDataUIDInt != _lastLastDataUIDInt
                )
            {
                // Calculate the total number of list items that will be needed
                int totalItems = (lastDataUIDInt - firstDataUIDInt) + 1;

                // Create new list item UI elements if there are not enough
                for (int i = _listItems.Count; i < totalItems; i++)
                {
                    CreateListItem(i);
                }

                // Set the items that can be thrown away for reuse
                // and the items that will be used
                _recycleItems.Clear();
                _inuseItems.Clear();

                if (forceSetData)
                {
                    // if this is a force rebuild of the data, 
                    // then set all items to be recycled
                    for (int i = 0; i < _listItems.Count; i++)
                    {
                        _recycleItems.Add(i);
                    }
                }
                else
                {
                    for (int i = 0; i < _listItems.Count; i++)
                    {
                        if (_listItems[i].UID >= firstDataUIDInt && _listItems[i].UID <= lastDataUIDInt)
                        {
                            // If the item's UID is in the range that is visible, put it in the _inuseItems list
                            // and turn visibility on
                            _listItems[i].IsActive = true;
                            _inuseItems.Add(_listItems[i].UID);
                        }
                        else if (
                                _listItems[i].UID == -1
                                ||
                                _listItems[i].UID < firstDataUIDInt
                                ||
                                _listItems[i].UID > lastDataUIDInt
                            )
                        {
                            // If the item's UID has not been used before or it is outside
                            // the range that is visible, set put the item in the recycle list.
                            _recycleItems.Add(i);
                        }
                    }
                }

                // Update the list items that are newly visible, ignoring those that already have the 
                // correct data set.
                int itemIndex = 0;
                int dataIndexModulus;
                for (int i = firstDataUIDInt; i <= lastDataUIDInt; i++)
                {
                    // The modulus of the UID is the proper data index. This is
                    // only meaningful in loops.
                    dataIndexModulus = i % _data.Count;

                    // If this item was not already visible before
                    if (!_inuseItems.Contains(i))
                    {
                        // Grab a new item from the recycle list
                        itemIndex = _recycleItems.RemoveEnd();

                        // Update the item's properties
                        _listItems[itemIndex].MainRectTransform.anchoredPosition = CreateVector(ItemPosition(i));
                        _listItems[itemIndex].MainRectTransform.sizeDelta = ItemSize(dataIndexModulus);
                        _listItems[itemIndex].UID = i;
                        _listItems[itemIndex].DataIndex = dataIndexModulus;
                        _listItems[itemIndex].IsActive = true;
                        _listItems[itemIndex].SetData(_data[dataIndexModulus]);
                    }
                }

                // Turn off all the leftover recycle items
                for (int i = 0; i < _recycleItems.Count; i++)
                {
                    _listItems[_recycleItems[i]].IsActive = false;
                }

                // Update the cached values of the last indices
                _lastFirstDataUIDInt = firstDataUIDInt;
                _lastLastDataUIDInt = lastDataUIDInt;
            }

            // If looping, make sure the list item container stays within the 
            // scroll area.
            if (loop)
            {
                if (_firstDataUID >= (_data.Count * 2))
                {
                    // The list item container has moved too far forward, so we jump backward the length
                    // of one list, making the jump appear seamless
                    scrollbar.value = CalculateScrollPosition(_firstDataUID - _data.Count);
                }
                else if (_firstDataUID < _data.Count)
                {
                    // The list item container has moved too far backward, so we jump forward the length
                    // of one list, making the jump appear seamless
                    scrollbar.value = CalculateScrollPosition(_firstDataUID + _data.Count);
                }
            }
        }

        /// <summary>
        /// Creates a new UI element for the list
        /// </summary>
        /// <param name="listItemIndex">The index of the element in the list</param>
        private void CreateListItem(int listItemIndex)
        {
            GameObject go;
            ListItemBase listItem;

            // Instantiate and set the parent to the list
            go = (GameObject)Instantiate(listItemPrefab);
            go.name = "Item_" + listItemIndex.ToString();
            go.transform.SetParent(listItemContainer.transform, false);

            // Set some item properties
            listItem = go.GetComponent<ListItemBase>();
            listItem.ItemIndex = listItemIndex;
            listItem.UID = -1;
            listItem.DataIndex = -1;
            listItem.IsActive = true;
            listItem.Scroller = this;

            _listItems.Add(listItem);
        }

        /// <summary>
        /// Sets the size of the list item container based on the total list size 
        /// and whether or not it is looping
        /// </summary>
        private void SetListItemContainer()
        {
            if (_ListSize < _ScrollRectSize)
            {
                // The list size is too small for scrolling, so the size is fixed to the scroll area
                listItemContainer.sizeDelta = (_VerticalScrolling ?
                                                new Vector2(listItemContainer.sizeDelta.x, _ScrollRectSize)
                                               :
                                                new Vector2(_ScrollRectSize, listItemContainer.sizeDelta.y)
                                               );

                listItemContainer.anchoredPosition = CreateVector(0);

                // Only show the scrollbar if it is set to always since the area cannot be scrolled
                SetScrollBarVisibility(scrollbarDisplayMode == ScrollbarDisplayMode.Always);
            }
            else
            {
                // This list is larger than the scroll area, so the size is set to one times the total list size if not
                // looping, otherwise three times the list size if looping
                listItemContainer.sizeDelta = (_VerticalScrolling ?
                                               new Vector2(listItemContainer.sizeDelta.x, _ListSize * (loop ? 3 : 1))
                                               :
                                               new Vector2(_ListSize * (loop ? 3 : 1), listItemContainer.sizeDelta.y)
                                               );

                // Show the scrollbar unless the visibility is set to never
                SetScrollBarVisibility(scrollbarDisplayMode != ScrollbarDisplayMode.Never);
            }
        }

        /// <summary>
        /// Calculates the item's position in the list item container based on it's UID.
        /// UID will be the same as data index in the case of non-looping lists. With looping
        /// lists, there will be three sets of data, so the UID keeps the items unique.
        /// </summary>
        /// <param name="UID">The unique ID of the list item</param>
        /// <returns>Position within the list item container</returns>
        private float ItemPosition(float UID)
        {
            if (_constantSize != -1f)
            {
                // If the list is using constant sized items, then the position
                // is just a simple multiplication.
                return (_VerticalScrolling ? -1 : 1) * (UID * _constantSize);
            }
            else
            {
                // The data index modulus is the actual data index, not matter where in the loop the item is
                int dataIndexModulus = Mathf.FloorToInt(UID) % _data.Count;

                // The remainder is the fractional component that goes against the size of the item
                float dataIndexRemainder = UID - Mathf.FloorToInt(UID);

                // The offset is the how far from the top of the list item container the item is. For loops, this could
                // zero, one, or two times the list size.
                float offset = Mathf.FloorToInt(UID / _data.Count) * _ListSize;

                // The position is the offset of the item, plus the remainder times the item size, plus the list item container offset
                return (_VerticalScrolling ? -1 : 1) * ((dataIndexModulus == 0 ? 0 : _listItemOffsets[dataIndexModulus - 1]) + (_listItemSizes[dataIndexModulus] * dataIndexRemainder) + offset);
            }
        }

        /// <summary>
        /// Gets the size of the item
        /// </summary>
        /// <param name="dataIndexModulus">The actual data index</param>
        /// <returns>Item's size</returns>
        private Vector2 ItemSize(int dataIndexModulus) 
        {
            if (_constantSize != -1f)
            {
                // constant sizes
                return (_VerticalScrolling ? new Vector2(listItemContainer.sizeDelta.x, _constantSize) : new Vector2(_constantSize, listItemContainer.sizeDelta.y));
            }
            else
            {
                // variable sizes
                return (_VerticalScrolling ? new Vector2(listItemContainer.sizeDelta.x, _listItemSizes[dataIndexModulus]) : new Vector2(_listItemSizes[dataIndexModulus], listItemContainer.sizeDelta.y));
            }
        }

        /// <summary>
        /// Calculates the UID of the first element in the scroll area
        /// </summary>
        private float CalculateFirstScrollUID() { return CalculateScrollUID(0); }

        /// <summary>
        /// Calculates the UID of the last element in the scroll area
        /// </summary>
        private float CalculateLastScrollUID(int firstScrollIndex) { return CalculateScrollUID(firstScrollIndex, _ScrollRectSize); }

        /// <summary>
        /// Calculates the UID of the element at the pixel offset in the list item container
        /// </summary>
        /// <param name="startIndex">The index to start looking. This speeds up calculation if we know an index will be after a certain point</param>
        /// <param name="pixelOffset">The number of pixels from the start of the list item container</param>
        private float CalculateScrollUID(int startIndex, float pixelOffset = 0)
        {
            // Get the pixel position based on the scroll value plus the pixel offset
            float pixelPosition = ((_VerticalScrolling ? (1f - scrollbar.value) : scrollbar.value) * (_ListItemContainerSize - _ScrollRectSize)) + pixelOffset;

            if (_constantSize != -1f)
            {
                // If the list is using a constant size for all the elements, then the
                // calculated scroll UID is just a simple division.
                return (pixelPosition / _constantSize);
            }
            else
            {
                // This list is using multiple item sizes, so we have to search for the 
                // index. This part of this function is the bottleneck for processing the 
                // scrolling, so if you are experiencing lags, you might consider using
                // a constant sized list instead.

                int offset = 0;
                if (loop)
                {
                    // Get the offset from the start of the list item container.
                    offset = Mathf.FloorToInt(pixelPosition / _ListSize);

                    // Subtract the offset from the pixel position
                    pixelPosition -= (offset * _ListSize);
                }

                // Look up the pixel position in the jump marker list.
                // This cuts down on looking through every position by a factor of 1000.
                // The jump markers are only used if the calculation is for the first
                // index (pixelOffset = 0), since calculating the last index already 
                // uses the first index as a start.
                if (pixelOffset == 0 && pixelPosition > 0)
                {
                    // Loop through the jump markers, starting at the end
                    // and working backward
                    for (int i = _jumpMarkerCount - 1; i >= 0; i--)
                    {
                        if (pixelPosition >= _jumpMarkers[i].offset)
                        {
                            // Found a jump marker, so the search will start at this index
                            // to speed things up
                            startIndex = _jumpMarkers[i].dataIndex;
                            break;
                        }
                    }
                }

                // Refined search based on the start index. Looks through the list items to find the one at the particular offset
                for (int i = startIndex; i < _listItemOffsets.Count; i++)
                {
                    if (_listItemOffsets[i] >= pixelPosition)
                    {
                        // This item is passed the pixel position, so it is the one it is looking for
                        return Mathf.Min(i + (1f - ((_listItemOffsets[i] - pixelPosition) / _listItemSizes[i])) + (offset * _data.Count), (loop ? _data.Count * 3 : _data.Count) - 1);
                    }
                }

                // The item wasn't found, so just return the last UID
                return (loop ? _data.Count * 3 : _data.Count) - 1;
            }
        }

        /// <summary>
        /// Calculates the scroll position based on the data index supplied
        /// </summary>
        /// <param name="dataIndex">The index of the data element</param>
        /// <returns>The scroll position [0..1]</returns>
        private float CalculateScrollPosition(float dataIndex)
        {
            float rawPosition = (Mathf.Abs(ItemPosition(dataIndex)) / (_ListItemContainerSize - _ScrollRectSize));
            return (_VerticalScrolling ? 1f - rawPosition : rawPosition);
        }

        /// <summary>
        /// Returns a vector based on the direction of scrolling
        /// </summary>
        /// <param name="value">The number to use for the vector</param>
        private Vector2 CreateVector(float value)
        {
            if (_VerticalScrolling) { return new Vector2(0, value); } else { return new Vector2(value, 0); }
        }

        /// <summary>
        /// Shows or hides the scrollbar by setting its size to full or zero
        /// </summary>
        /// <param name="visible">Whether to show the scrollbar</param>
        private void SetScrollBarVisibility(bool visible)
        {
            scrollbar.gameObject.SetActive(visible);
            // TODO: Remove
            //_scrollBarRectTransform.sizeDelta = (visible ? _originalScrollBarSize : Vector2.zero);
        }

        /// <summary>
        /// These functions are used to trap user interaction. The difference between them
        /// being the type of argument (or lack thereof).
        /// </summary>
        public void ItemSelected(ListItemBase listItem)
        {
            if (itemSelected != null) itemSelected(listItem);
        }
        public void ItemSelected(ListItemBase listItem, int arg)
        {
            if (itemSelectedInt != null) itemSelectedInt(listItem, arg);
        }
        public void ItemSelected(ListItemBase listItem, float arg)
        {
            if (itemSelectedFloat != null) itemSelectedFloat(listItem, arg);
        }
        public void ItemSelected(ListItemBase listItem, string arg)
        {
            if (itemSelectedString != null) itemSelectedString(listItem, arg);
        }

        #endregion
    }
}