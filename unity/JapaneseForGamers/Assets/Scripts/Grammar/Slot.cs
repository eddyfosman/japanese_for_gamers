using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {
	GameObject wordPanel;
	static int cellStaticId = 0;
	int cellId;
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


	}

	void Start(){
		wordPanel = GameObject.FindGameObjectWithTag ("WordPanel");
		cellStaticId++;
		CellId = cellStaticId;
	}

	Transform NextCell(){
		Transform trans;
		foreach(Transform t in wordPanel.transform){
			if(t.gameObject.GetComponent<Slot>().CellId == CellId + 1){
				Debug.Log("O dang bi tranh cho la: " + t.gameObject.GetComponent<Slot>().CellId);
				trans = t;
				break;

			}

		}
		return transform;
	}

	#region IDropHandler implementation
	public void OnDrop (PointerEventData eventData)
	{
		if(!item){
			DragHandler.itemBeingDragged.transform.SetParent(transform);
			Debug.Log(CellId);
		}
		else{
			item.transform.SetParent(NextCell());
			DragHandler.itemBeingDragged.transform.SetParent(transform);
			Debug.Log(CellId);

		}

	}
	#endregion
}
