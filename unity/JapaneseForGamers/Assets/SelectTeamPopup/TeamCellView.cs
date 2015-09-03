using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;

public class TeamCellView : EnhancedScrollerCellView {

	private TeamData _data;

	public GameObject teamContainer;
	public Image notCreatedImage;
	public Image image;
	public Image image1;
	public Image image2;
	public Image image3;
	public Image image4;

	public GameObject selectMonsterPopupGO;


	public Color selectedColor;
	public Color unSelectedColor;
	
	public int DataIndex{ get; private set;}
	
	public SelectedDelegate selected;
	
	void OnDestroy(){
		if(_data != null){
			_data.selectedChanged -= SelectedChanged;
		}
	}
	
	public void SetData(int dataIndex, TeamData data, bool isVertical){
		if(_data != null){
			_data.selectedChanged -= SelectedChanged;
		}
		
		DataIndex = dataIndex;
		_data = data;
		image1.sprite = Resources.Load ("Sprites/" + data.monsterName1, typeof(Sprite)) as Sprite;
		image2.sprite = Resources.Load ("Sprites/" + data.monsterName2, typeof(Sprite)) as Sprite;
		image3.sprite = Resources.Load ("Sprites/" + data.monsterName3, typeof(Sprite)) as Sprite;
		image4.sprite = Resources.Load ("Sprites/" + data.monsterName4, typeof(Sprite)) as Sprite;

		if(!_data.Created){
			notCreatedImage.gameObject.SetActive(true);
			teamContainer.SetActive(false);
//			selectMonsterPopupGO.GetComponent<MonsterInventory>().Reload();
		}
		
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
