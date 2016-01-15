﻿using UnityEngine;
using UnityEngine.UI;
using EnhancedUI;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using LitJson;


public class TeamSelect : MonoBehaviour,IEnhancedScrollerDelegate {

	TextAsset jsonTeamFile;
	JsonData jsonTeam;

	private SmallList<TeamData> _data;
	
	public EnhancedScroller vScroller;

	public EnhancedScrollerCellView vCellViewPrefab;

	public GameObject selectTeamPopup;
	public GameObject selectMonsterPopup;
	public GameObject selectMonsterPopupGO;
	private MonsterInventory2 monsterInventoryScript2;

	public Button editBtn;

	void Awake(){
		CellViewSelected (null);
	}

	void Start(){
		monsterInventoryScript2 = selectMonsterPopupGO.GetComponent<MonsterInventory2> ();
		vScroller.Delegate = this;
		Reload ();
		editBtn.GetComponent<Selectable>().interactable = false;
	}

	private void Reload(){
		jsonTeamFile = Resources.Load ("teams") as TextAsset;
		jsonTeam = JsonMapper.ToObject (jsonTeamFile.text);
	
		if(_data != null){
			for(var i = 0; i < _data.Count; i++){
				_data[i].selectedChanged = null;
			}
		}
		
		_data = new SmallList<TeamData>();
		
		for(int i = 0; i < jsonTeam["teams"].Count; i++){

				_data.Add(new TeamData(){
					Selected = System.Convert.ToBoolean(jsonTeam["teams"][i]["selected"].ToString()),
					Created = System.Convert.ToBoolean(jsonTeam["teams"][i]["created"].ToString()),
					monsterName1 = jsonTeam["teams"][i]["monsters"][0]["name"].ToString(),
					monsterName2 = jsonTeam["teams"][i]["monsters"][1]["name"].ToString(),
					monsterName3 = jsonTeam["teams"][i]["monsters"][2]["name"].ToString(),
					monsterName4 = jsonTeam["teams"][i]["monsters"][3]["name"].ToString()
						
				});


		}
		
		vScroller.ReloadData();
	}

	public void HideSelectTeamPopup(){
		for(var i = 0; i < _data.Count; i++){
			if(_data[i].Selected){
				monsterInventoryScript2.listMonsterSelected.Add(_data[i].monsterName1);
				monsterInventoryScript2.listMonsterSelected.Add(_data[i].monsterName2);
				monsterInventoryScript2.listMonsterSelected.Add(_data[i].monsterName3);
				monsterInventoryScript2.listMonsterSelected.Add(_data[i].monsterName4);
				break;

			}
		}
		selectTeamPopup.GetComponent<CanvasGroup>().alpha = 0;
		selectTeamPopup.GetComponent<CanvasGroup>().interactable = false;
		selectTeamPopup.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		selectMonsterPopup.GetComponent<CanvasGroup>().alpha = 1;
		selectMonsterPopup.GetComponent<CanvasGroup>().interactable = true;
		selectMonsterPopup.GetComponent<CanvasGroup>().blocksRaycasts = true;
		monsterInventoryScript2.Reload (true);
	}

	private void CellViewSelected(EnhancedScrollerCellView cellView){
		if(cellView != null){
			var selectedDataIndex = (cellView as TeamCellView).DataIndex;
			
			for (var i = 0; i < _data.Count; i++)
			{
				if(i == selectedDataIndex){
					if(!_data[i].Created){
						selectTeamPopup.GetComponent<CanvasGroup>().alpha = 0;
						selectTeamPopup.GetComponent<CanvasGroup>().interactable = false;
						selectTeamPopup.GetComponent<CanvasGroup> ().blocksRaycasts = false;
						selectMonsterPopup.GetComponent<CanvasGroup>().alpha = 1;
						selectMonsterPopup.GetComponent<CanvasGroup>().interactable = true;
						selectMonsterPopup.GetComponent<CanvasGroup>().blocksRaycasts = true;
						editBtn.GetComponent<Selectable>().interactable = false;
					}
					else if(_data[i].Created){
						editBtn.GetComponent<Selectable>().interactable = true;
					}
				}

				_data[i].Selected = (selectedDataIndex == i);
			}


			
		}
	}

	#region IEnhancedScrollerDelegate implementation

	public int GetNumberOfCells (EnhancedScroller scroller)
	{
		return _data.Count;
	}

	public float GetCellViewSize (EnhancedScroller scroller, int dataIndex)
	{
		return 120f;
	}

	public EnhancedScrollerCellView GetCellView (EnhancedScroller scroller, int dataIndex, int cellIndex)
	{
		TeamCellView cellView = scroller.GetCellView (vCellViewPrefab) as TeamCellView;
		cellView.name = "Vertical " + _data [dataIndex].monsterName1;
		cellView.selected = CellViewSelected;
		cellView.SetData (dataIndex, _data[dataIndex], true);
		return cellView;
	}

	#endregion
}