using UnityEngine;
using UnityEngine.UI;
using EnhancedUI;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using LitJson;

public delegate void CellSelectedDelegate(bool val);


public class MonsterInventory2 : MonoBehaviour, IEnhancedScrollerDelegate{

	public static void DoSomeThing(){

	}

	TextAsset jsonMonsterFile;
	JsonData jsonMonsters;

	public bool isFirstCell;

	static int monstersEquipped = 0;

	static Stack<int> stack = new Stack<int>();

	public Hashtable hashtableMonster = new Hashtable ();

	public List<string> listMonsterSelected = new List<string> ();

	private SmallList<InventoryData2> _data;

	public EnhancedScroller vScroller;

	private List<int> listCellIndex;

	private bool isFirstPos;

	public int StackCount{
		get{
			return stack.Count;
		}
	}

	public EnhancedScrollerCellView vCellViewPrefab;

	void Awake(){
		CellViewSelected (null);
	}

	void Start(){
		listCellIndex = new List<int> ();
		vScroller.Delegate = this;
		Reload (true);

	}

	public void WhichPosition(bool val){
		isFirstPos = val;
	}

	public void ResetListMonsterSelected(){
		listMonsterSelected.Clear ();
	}

	public void Reload(bool val){

		jsonMonsterFile = Resources.Load ("monsters") as TextAsset;
		jsonMonsters = JsonMapper.ToObject (jsonMonsterFile.text);

		if(_data != null){
			for(var i = 0; i < _data.Count; i++){
				_data[i].selectedChanged = null;
			}
		}

		_data = new SmallList<InventoryData2>();

		for(int i = 0; i < jsonMonsters["monsters"].Count; i+=2){
			if(i + 1 < jsonMonsters["monsters"].Count){
				_data.Add(new InventoryData2(){
					id = jsonMonsters["monsters"][i]["id"].ToString(),
					monsterName = jsonMonsters["monsters"][i]["image"].ToString(),
					id2 = jsonMonsters["monsters"][i + 1]["id"].ToString(),
					monsterName2 = jsonMonsters["monsters"][i+1]["image"].ToString()
						
				});
			}

		}

		vScroller.ReloadData();
		if(listMonsterSelected.Count != 0 && val){
			ReloadMonsterSelected ();
		}

	}

	private void ReloadMonsterSelected(){
		InventoryCellView2 inventory = new InventoryCellView2 ();
		for(int i = 0; i < _data.Count; i++){
			for(int j = 0; j < listMonsterSelected.Count; j++){
				if(_data[i].monsterName == listMonsterSelected[j]){
					inventory.ReturnStack.Push(_data[i].id);
//					stack.Push(i);
					_data[i].Selected = true;
					inventory.ReturnHashTable.Add(_data[i].id,_data[i].monsterName);
//					hashtableMonster.Add(i,_data[i].monsterName);


				}
				if(_data[i].monsterName2 == listMonsterSelected[j]){
					inventory.ReturnStack.Push(_data[i].id2);
					//					stack.Push(i);
					_data[i].Selected2 = true;
					inventory.ReturnHashTable.Add(_data[i].id2,_data[i].monsterName2);
					//					hashtableMonster.Add(i,_data[i].monsterName);
					

				}
			}
		}
	}

	private void CellViewSelected(EnhancedScrollerCellView cellView){
		if(cellView != null){
			var selectedDataIndex = (cellView as InventoryCellView2).DataIndex;

//			for(var i = 0; i < _data.Count; i++){
//				if(selectedDataIndex == i){
//					if(_data[i].Selected && !_data[i].Selected2 ){
//						if(){
//
//						}
//					}
//				}
//
//			}

			// loop through each item in the data list and turn
			// on or off the selection state. This is done so that
			// any previous selection states are removed and new
			// ones are added.
//			for (var i = 0; i < _data.Count; i++)
//			{
//
//				if(selectedDataIndex == i){
//					if(!_data[i].Selected ){
//						if(stack.Count < 4){
//							stack.Push(selectedDataIndex);
//							hashtableMonster.Add(selectedDataIndex,_data[i].monsterName);
//							_data[i].Selected = (selectedDataIndex == i);
//
//							break;
//						}
//						else if(4 == stack.Count){
//							hashtableMonster.Remove(stack.Peek());
//							_data[stack.Pop()].Selected = false;
//							_data[i].Selected = true;
//							stack.Push(i);
//							hashtableMonster.Add(i,_data[i].monsterName);
//							break;
//						}
//
//					}
//					else {
//						if(stack.Peek() == i){
//							hashtableMonster.Remove(stack.Peek());
//							_data[stack.Pop()].Selected = false;
//							break;
//						}
//						else{
//							Queue tempQueue = new Queue();
//							while(stack.Peek() != i){
//								tempQueue.Enqueue(stack.Pop());
//							}
//							hashtableMonster.Remove(stack.Peek());
//							_data[stack.Pop()].Selected = false;
//							while(tempQueue.Count > 0){
//								stack.Push((int)tempQueue.Dequeue());
//							}
//							break;
//						}
//					}
//	
//				}
//			}

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
		InventoryCellView2 cellView = scroller.GetCellView (vCellViewPrefab) as InventoryCellView2;
		cellView.name = "Vertical " + _data [dataIndex].monsterName;
		cellView.selected = CellViewSelected;
		cellView.SetData (dataIndex, _data[dataIndex], true);
		return cellView;
	}
	#endregion
}
