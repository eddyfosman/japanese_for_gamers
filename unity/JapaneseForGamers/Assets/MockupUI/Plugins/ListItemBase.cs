using UnityEngine;

namespace EnhancedUI
{
    /// <summary>
    /// This is the base class for list items. You will need your list item classes to inherit from this 
    /// class. Usually, you will only need to override the SetData function unless your list items have
    /// an unusual structure. In that case, you will need to override the IsActive and/or MainRectTransform
    /// parameters.
    /// </summary>
    public class ListItemBase : MonoBehaviour
    {
        private int _dataIndex = -1;

        /// <summary>
        /// The index of the list item, starting at zero.
        /// </summary>
        public int ItemIndex { get; set; }

        /// <summary>
        /// The index of data for this item, starting at zero.
        /// </summary>
        public int DataIndex { get { return _dataIndex; } set { _dataIndex = value; } }

        /// <summary>
        /// This is the unique ID, which for none looping will be the same as the data index.
        /// For looping there will be three sets of the same data, so this value is needed
        /// to distinguish between the three matching DataIndices.
        /// </summary>
        public int UID { get; set; }

        /// <summary>
        /// This is the link to the scroller object. Typically,
        /// this will not need to be overwritten.
        /// </summary>
        public virtual EnhancedScroller Scroller { get; set; }

        /// <summary>
        /// Gets or sets the active state of a list item
        /// </summary>
        public virtual bool IsActive { get { return gameObject.activeSelf; } set { gameObject.SetActive(value); } }

        /// <summary>
        /// Returns the primary Rect Transform of your list item. By default,
        /// this pulls the Rect Transform on the root gameobject.
        /// </summary>
        public virtual RectTransform MainRectTransform { get { return gameObject.GetComponent<RectTransform>(); } }

        /// <summary>
        /// Sets the data of the list item. You will want to cast the object
        /// to the data type class in your project, then set the list item
        /// components accordingly
        /// </summary>
        /// <param name="objectData">The generic object passed to the list item. This should be cast to your data type</param>
        public virtual void SetData(object objectData) { }

        public virtual void CopyItem(ListItemBase listItem) { }

        /// <summary>
        /// This function notifies the scroller that the item was selected. You can link to this
        /// function via the event system in Unity UI to handle the selected callback for you.
        /// </summary>
        public virtual void ItemSelected() { if (Scroller != null) Scroller.ItemSelected(this); }
        public virtual void ItemSelected(int arg) { if (Scroller != null) Scroller.ItemSelected(this, arg); }
        public virtual void ItemSelected(float arg) { if (Scroller != null) Scroller.ItemSelected(this, arg); }
        public virtual void ItemSelected(string arg) { if (Scroller != null) Scroller.ItemSelected(this, arg); }
    }
}