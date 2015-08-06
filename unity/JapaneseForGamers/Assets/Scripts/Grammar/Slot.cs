using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {

	public GameObject cellPrefab;
	GameObject cellGo;
	GameObject wordPanel;
	DragHandler itemDragHandler;
	DragHandler itemBeingDraggedDragHandler;
	Transform trans;
	static int cellStaticId = 0;
	int cellId;
	public int ID;
	public int CellId{
		get{return cellId;}
		set{cellId = value;}
		}
	public GameObject item{
		get{
			if(transform.childCount > 0){
				return transform.GetChild(0).gameObject;
			}
			return null;
		}
		set{

		}

	}

	public void Check(){

	}

	void Start(){
		wordPanel = GameObject.FindGameObjectWithTag ("WordPanel");
		cellStaticId++;
		CellId = cellStaticId;
		ID = cellStaticId;
	}

	void Update(){

	}

	void AddWordLoop(Transform trans, GameObject passedItem){
		if(!item){
			passedItem.transform.SetParent(trans, false);
		}
		else{
			passedItem.transform.SetParent(trans, false);
			foreach(Transform t in wordPanel.transform){
				if(t.gameObject.GetComponent<Slot>().CellId == CellId + 1){
					Debug.Log("O dang bi tranh cho la: " + t.gameObject.GetComponent<Slot>().CellId);
					trans = t;
					break;
					
				}
			}
			trans.GetComponent<Slot>().AddWordLoop(trans,item);
		}

	}


			public void AddWordIntoCell ()
		{
		itemBeingDraggedDragHandler = DragHandler.itemBeingDragged.GetComponent<DragHandler> ();
		if(!item){
			
//					Debug.Log(eventData.position);
			DragHandler.itemBeingDragged.transform.SetParent(transform);
			itemBeingDraggedDragHandler.previousParent = itemBeingDraggedDragHandler.currentParent;
			itemBeingDraggedDragHandler.currentParent = transform;
			Debug.Log(CellId);
		}
		else{
			DragHandler.itemBeingDragged.transform.SetParent(transform);
			itemBeingDraggedDragHandler.previousParent = itemBeingDraggedDragHandler.currentParent;
			itemBeingDraggedDragHandler.currentParent = transform;
			foreach(Transform t in wordPanel.transform){
				if(t.gameObject.GetComponent<Slot>().CellId == CellId + 1){
					Debug.Log("O dang bi tranh cho la: " + t.gameObject.GetComponent<Slot>().CellId);
					trans = t;
					break;
					
				}
			}
			trans.GetComponent<Slot>().AddWordLoop(trans,item);
			

		}

//				if (itemDragHandler.currentParent.gameObject.GetComponent<Slot> ().CellId
//						> itemBeingDraggedDragHandler.currentParent.gameObject.GetComponent<Slot> ().CellId) {
//						trans = NextCell (1);
//						if (!trans.gameObject.GetComponent<Slot> ().item) {
//								item.transform.SetParent (trans);
//								DragHandler.itemBeingDragged.transform.SetParent (transform);
//								Debug.Log (CellId);
//						} else {
//								trans.gameObject.GetComponent<Slot> ().item.transform.SetParent (NextCell (2));
//								DragHandler.itemBeingDragged.transform.SetParent (transform);
//								Debug.Log (CellId);
//						}
//				} else if (itemDragHandler.currentParent.gameObject.GetComponent<Slot> ().CellId
//						< itemBeingDraggedDragHandler.currentParent.gameObject.GetComponent<Slot> ().CellId) {
//						trans = PreviousCell (1);
//						if (!trans.gameObject.GetComponent<Slot> ().item) {
//								item.transform.SetParent (trans);
//								DragHandler.itemBeingDragged.transform.SetParent (transform);
//								Debug.Log (CellId);
//						} else {
//								trans.gameObject.GetComponent<Slot> ().item.transform.SetParent (PreviousCell (2));
//								DragHandler.itemBeingDragged.transform.SetParent (transform);
//								Debug.Log (CellId);
//						}
//				}
		}

	Transform NextCell(int numCell){

		foreach(Transform t in wordPanel.transform){
			if(t.gameObject.GetComponent<Slot>().CellId == CellId + numCell){
				Debug.Log("O dang bi tranh cho la: " + t.gameObject.GetComponent<Slot>().CellId);
				trans = t;
				break;

			}

				


		}
		if(trans == null){
			cellGo = null;
			cellGo = Instantiate(cellPrefab) as GameObject;
			if(cellGo.transform.childCount > 0){
				Destroy(cellGo.transform.GetChild(0).gameObject);
			}
			cellGo.transform.SetParent(wordPanel.transform, false);
			trans = cellGo.transform;
			Debug.Log("O dang bi tranh cho la: " + trans.gameObject.GetComponent<Slot>().CellId);
		}
		return trans;
	}

	Transform PreviousCell(int numCell){
		
		foreach(Transform t in wordPanel.transform){
			if(t.gameObject.GetComponent<Slot>().CellId == CellId - numCell){
				Debug.Log("O dang bi tranh cho la: " + t.gameObject.GetComponent<Slot>().CellId);
				trans = t;
				break;
				
			}
			
			
			
			
		}
		if(trans == null){
			cellGo = null;
			cellGo = Instantiate(cellPrefab) as GameObject;
			if(cellGo.transform.childCount > 0){
				Destroy(cellGo.transform.GetChild(0).gameObject);
			}
			cellGo.transform.SetParent(wordPanel.transform, false);
			trans = cellGo.transform;
			Debug.Log("O dang bi tranh cho la: " + trans.gameObject.GetComponent<Slot>().CellId);
		}
		return trans;
	}

	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
		itemBeingDraggedDragHandler = DragHandler.itemBeingDragged.GetComponent<DragHandler> ();
		if(!item){

			Debug.Log(eventData.position);
			DragHandler.itemBeingDragged.transform.SetParent(transform);
			itemBeingDraggedDragHandler.previousParent = itemBeingDraggedDragHandler.currentParent;
			itemBeingDraggedDragHandler.currentParent = transform;
			Debug.Log(CellId);
		}
		else{
			itemDragHandler = item.GetComponent<DragHandler>();
			if(itemBeingDraggedDragHandler.currentParent.gameObject.tag == itemDragHandler.currentParent.gameObject.tag){
				DragHandler.itemBeingDragged.transform.SetParent(item.transform.parent, false);	
				itemBeingDraggedDragHandler.previousParent = itemBeingDraggedDragHandler.currentParent;
				itemBeingDraggedDragHandler.currentParent = item.transform.parent;
				itemDragHandler.previousParent = itemDragHandler.currentParent;
				itemDragHandler.currentParent = itemBeingDraggedDragHandler.previousParent;
				item.transform.SetParent(itemDragHandler.currentParent, false);

//				AddWordIntoCell();

//				if(itemDragHandler.currentParent.gameObject.GetComponent<Slot>().CellId
//				   > itemBeingDraggedDragHandler.currentParent.gameObject.GetComponent<Slot>().CellId){
//					trans = NextCell(1);
//					if(!trans.gameObject.GetComponent<Slot>().item){
//						item.transform.SetParent(trans);
//						DragHandler.itemBeingDragged.transform.SetParent(transform);
//						Debug.Log(CellId);
//					}
//					else{
//						trans.gameObject.GetComponent<Slot>().item.transform.SetParent(NextCell(2));
//						DragHandler.itemBeingDragged.transform.SetParent(transform);
//						Debug.Log(CellId);
//					}
//				}
//				else if(itemDragHandler.currentParent.gameObject.GetComponent<Slot>().CellId
//				        < itemBeingDraggedDragHandler.currentParent.gameObject.GetComponent<Slot>().CellId){
//					trans = PreviousCell(1);
//					if(!trans.gameObject.GetComponent<Slot>().item){
//						item.transform.SetParent(trans);
//						DragHandler.itemBeingDragged.transform.SetParent(transform);
//						Debug.Log(CellId);
//					}
//					else{
//						trans.gameObject.GetComponent<Slot>().item.transform.SetParent(PreviousCell(2));
//						DragHandler.itemBeingDragged.transform.SetParent(transform);
//						Debug.Log(CellId);
//					}
//				}
			}
			else {
				DragHandler.itemBeingDragged.transform.SetParent(item.transform.parent, false);	
				itemBeingDraggedDragHandler.previousParent = itemBeingDraggedDragHandler.currentParent;
				itemBeingDraggedDragHandler.currentParent = item.transform.parent;
				itemDragHandler.previousParent = itemDragHandler.currentParent;
				itemDragHandler.currentParent = itemBeingDraggedDragHandler.previousParent;
				item.transform.SetParent(itemDragHandler.currentParent, false);
			}


		}

	}
	#endregion
}
