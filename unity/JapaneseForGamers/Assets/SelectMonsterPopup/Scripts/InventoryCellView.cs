using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;

public delegate void SelectedDelegate(EnhancedScrollerCellView cellView);

public class InventoryCellView : EnhancedScrollerCellView {

	private InventoryData _data;

	public Image image;

	public Color selectedColor;
	public Color unSelectedColor;

	public int DataIndex{ get; private set;}

	public SelectedDelegate selected;

	void OnDestroy(){
		if(_data != null){
			_data.selectedChanged -= SelectedChanged;
		}
	}

	public void SetData(int dataIndex, InventoryData data, bool isVertical){
		if(_data != null){
			_data.selectedChanged -= SelectedChanged;
		}

		DataIndex = dataIndex;
		_data = data;
		image.sprite = Resources.Load ("Sprites/" + data.monsterName, typeof(Sprite)) as Sprite;

		_data.selectedChanged -= SelectedChanged;
		_data.selectedChanged += SelectedChanged;

		SelectedChanged (data.Selected);

	}

	private void SelectedChanged(bool selected){
		image.color = (selected ? selectedColor : unSelectedColor);
	}

	public void OnSelected(){
		if (selected != null) {
			selected(this);		
		} 
	}

}
