using UnityEngine;
using UnityEngine.UI;
using EnhancedUI;
using System.Collections.Generic;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using LitJson;

public class MonsterInventory : MonoBehaviour, IEnhancedScrollerDelegate{

	TextAsset jsonMonsterFile;
	JsonData jsonMonsters;

	static int monstersEquipped = 0;

	static Stack<int> stack = new Stack<int>();

	public Hashtable hashtableMonster = new Hashtable ();

	private SmallList<InventoryData> _data;

	public EnhancedScroller vScroller;

	private List<int> listCellIndex;

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
		Reload ();
	}

	private void Reload(){

		jsonMonsterFile = Resources.Load ("monsters") as TextAsset;
		jsonMonsters = JsonMapper.ToObject (jsonMonsterFile.text);

		if(_data != null){
			for(var i = 0; i < _data.Count; i++){
				_data[i].selectedChanged = null;
			}
		}

		_data = new SmallList<InventoryData>();

		for(int i = 0; i < jsonMonsters["monsters"].Count; i++){
			_data.Add(new InventoryData(){
				
				monsterName = jsonMonsters["monsters"][i]["image"].ToString()
					
			});
		}

		vScroller.ReloadData();
	}



	private void CellViewSelected(EnhancedScrollerCellView cellView){
		if(cellView != null){
			var selectedDataIndex = (cellView as InventoryCellView).DataIndex;
//			if(listCellIndex.Count < 4){
//				listCellIndex.Add(selectedDataIndex);
//			}
			
			// loop through each item in the data list and turn
			// on or off the selection state. This is done so that
			// any previous selection states are removed and new
			// ones are added.
			for (var i = 0; i < _data.Count; i++)
			{
//				for(var j = 0; j < listCellIndex.Count; j++){
//					_data[i].Selected = (selectedDataIndex == i && selectedDataIndex == j);
//				}
				if(selectedDataIndex == i){
					if(!_data[i].Selected ){
						if(stack.Count < 4){
							stack.Push(selectedDataIndex);
							hashtableMonster.Add(selectedDataIndex,_data[i].monsterName);
							_data[i].Selected = (selectedDataIndex == i);

							break;
						}
						else if(4 == stack.Count){
							hashtableMonster.Remove(stack.Peek());
							_data[stack.Pop()].Selected = false;
							_data[i].Selected = true;
							stack.Push(i);
							hashtableMonster.Add(i,_data[i].monsterName);
							break;
						}

					}
					else {
						if(stack.Peek() == i){
							hashtableMonster.Remove(stack.Peek());
							_data[stack.Pop()].Selected = false;
							break;
						}
						else{
							Queue tempQueue = new Queue();
							while(stack.Peek() != i){
								tempQueue.Enqueue(stack.Pop());
							}
							hashtableMonster.Remove(stack.Peek());
							_data[stack.Pop()].Selected = false;
							while(tempQueue.Count > 0){
								stack.Push((int)tempQueue.Dequeue());
							}
							break;
						}
					}
				
//				else if(_data[i].Selected && monstersEquipped > 0){
//						if(stack.Peek() != null){
//							if(stack.Peek() != i){
//								for(int j = 0; j < _data.Count; j++){
//									if(stack.Peek() == j){
//										_data[stack.Pop()].Selected = false;
//										monstersEquipped--;
//										break;
//									}
//								}
//							}
////							while(stack.Peek() != i && stack.Count > 0){
////								_data[stack.Pop()].Selected = false;
////								monstersEquipped--;
////							}
////							_data[i].Selected = false;
//
//							break;
//						}
//
//					}
//				}
//				if(!_data[i].Selected && monstersEquipped < 4){
//
//					if(selectedDataIndex == i){
//						Debug.Log(_data[i].monsterName);
//						stack.Push(selectedDataIndex);
//						monstersEquipped++;
//						_data[i].Selected = (selectedDataIndex == i);
//						break;
//					}
//
//				}
//				else if(_data[i].Selected && monstersEquipped > 0){
//					if(stack.Peek() != null){
//
//
//						if(selectedDataIndex == i){
//							if(stack.Peek() == i){
//								_data[i].Selected = false;
//								monstersEquipped--;
//								stack.Pop();
//								break;
//							}
//							else if(stack.Peek() != i){
//								_data[stack.Peek()].Selected = false;
//								monstersEquipped--;
//								stack.Pop();
//								break;
//							}
//						}
//						
//						
//					}
//
//
//
//
//				}
//				_data[i].Selected = (selectedDataIndex == i);
			}
			}
//			for (var i = 0; i < _data.Count; i++){
//				if(_data[i].Selected && monstersEquipped > 0){
//					if(stack.Peek() != null){
//						if(stack.Peek() != i){
//							for(int j = 0; j < _data.Count; j++){
//								if(stack.Peek() == j){
//									_data[stack.Pop()].Selected = false;
//									monstersEquipped--;
//									break;
//								}
//							}
//						}
//						//							while(stack.Peek() != i && stack.Count > 0){
//						//								_data[stack.Pop()].Selected = false;
//						//								monstersEquipped--;
//						//							}
//						//							_data[i].Selected = false;
//						
//						break;
//					}
//					
//				}
//			
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
		InventoryCellView cellView = scroller.GetCellView (vCellViewPrefab) as InventoryCellView;
		cellView.name = "Vertical " + _data [dataIndex].monsterName;
		cellView.selected = CellViewSelected;
		cellView.SetData (dataIndex, _data[dataIndex], true);
		return cellView;
	}
	#endregion
}
