using UnityEngine;
using UnityEngine.UI;
using EnhancedUI.EnhancedScroller;
using System.Collections;

//public delegate void SelectedDelegate(EnhancedScrollerCellView cellView);

public class InventoryCellView2 : EnhancedScrollerCellView {

	private InventoryData2 _data;

	public Image image;
	public Image image2;

	public Image bgrImage1;
	public Image bgrImage2;

	public Color selectedColor;
	public Color unSelectedColor;

	public int DataIndex{ get; private set;}

	public SelectedDelegate selected;

	void OnDestroy(){
		if(_data != null){
			_data.selectedChanged -= SelectedChanged;
		}
	}

	public void SetData(int dataIndex, InventoryData2 data, bool isVertical){
		if(_data != null){
			_data.selectedChanged -= SelectedChanged;
			_data.selectedChanged2 -= SelectedChanged2;
		}

		DataIndex = dataIndex;
		_data = data;
		image.sprite = Resources.Load ("Sprites/" + data.monsterName, typeof(Sprite)) as Sprite;
		image2.sprite = Resources.Load ("Sprites/" + data.monsterName2, typeof(Sprite)) as Sprite;

		_data.selectedChanged -= SelectedChanged;
		_data.selectedChanged += SelectedChanged;

		_data.selectedChanged2 -= SelectedChanged2;
		_data.selectedChanged2 += SelectedChanged2;

		SelectedChanged (data.Selected);
		SelectedChanged2 (data.Selected2);

	}

	private void SelectedChanged(bool selected){
		bgrImage1.color = (selected ? selectedColor : unSelectedColor);

	}

	private void SelectedChanged2(bool selected){

		bgrImage2.color = (selected ? selectedColor : unSelectedColor);
	}

	public static Stack stack = new Stack();
//	public static bool first = false;

	public void OnSelected(){
//		if(!first){
//			stack = new Stack ();
//			first = true;
//		}

		if (selected != null) {
			selected(this);	
			if(!_data.Selected){
				if(stack.Count < 4){
					Debug.Log(stack.Count);
					_data.Selected = true;
					stack.Push(_data.monsterName);
					Debug.Log(stack.Count);
				}
			}
			else{
				if(stack.Count == 0){
					Debug.Log("STACK BANG KHOONG");
					return;
				}
				if(stack.Peek() == _data.monsterName){
					stack.Pop();
					_data.Selected = false;
				}
				else{
					Queue tempQueue = new Queue();
					while(stack.Peek() != _data.monsterName){
						tempQueue.Enqueue(stack.Pop());
					}
					stack.Pop();
					_data.Selected = false;
					while(tempQueue.Count > 0){
						stack.Push((string)tempQueue.Dequeue());
					}
				}

			}

		} 
	}

	public void OnSelected2(){
//		if(!first){
//			stack = new Stack ();
//			first = true;
//		}

		if (selected != null) {
			selected(this);		
			if(!_data.Selected2){

				if(stack.Count < 4){
					Debug.Log(stack.Count);
					_data.Selected2 = true;
					stack.Push(_data.monsterName2);
					Debug.Log(stack.Count);
				}
			}
			else{
				if(stack.Count == 0){
					Debug.Log("STACK BANG KHOONG");
					return;
				}
				if(stack.Peek() == _data.monsterName2){
					stack.Pop();
					_data.Selected2 = false;
				}
				else{
					Queue tempQueue = new Queue();
					while(stack.Peek() != _data.monsterName2){
						tempQueue.Enqueue(stack.Pop());
					}
					stack.Pop();
					_data.Selected2 = false;
					while(tempQueue.Count > 0){
						stack.Push((string)tempQueue.Dequeue());
					}
				}

			}

		} 
	}

}
