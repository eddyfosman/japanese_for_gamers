using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnswerPanel : MonoBehaviour, IDropHandler,IDragHandler {

	GameObject[] slotList;
	RectTransform answerPanelRectTrans;
	GridLayoutGroup gridLayout;
	bool moreThanOneRow = false;
	int objectsInOneRow = 0;
	int numberOfColumn = 0;


	// Use this for initialization
	void Start () {
		gridLayout = gameObject.GetComponent<GridLayoutGroup> ();
		answerPanelRectTrans = transform.GetComponent<RectTransform>();
		slotList = GameObject.FindGameObjectsWithTag("Slot");
		Debug.Log (slotList[0].GetComponent<RectTransform>().rect.width);
		if(slotList != null){
			RectTransform slotRectTransform = slotList[0].GetComponent<RectTransform>();
			if(((slotList.Length - 1) * gridLayout.spacing.x + (slotList.Length * gridLayout.cellSize.x)) > answerPanelRectTrans.rect.width){
				moreThanOneRow = true;
				objectsInOneRow = (int)((answerPanelRectTrans.rect.width + gridLayout.spacing.x)/(gridLayout.spacing.x + gridLayout.cellSize.x));
				numberOfColumn = slotList.Length/objectsInOneRow;
				Debug.Log("SỐ PHẦN TỬ MỘT DÒNG LÀ " + objectsInOneRow);
				Debug.Log("SỐ PHẦN TỬ MỘT DÒNG LÀ :" + (answerPanelRectTrans.rect.width + gridLayout.spacing.x)/(gridLayout.spacing.x + gridLayout.cellSize.x));

			}
			else{
				moreThanOneRow = false;
				objectsInOneRow = (int)((answerPanelRectTrans.rect.width + gridLayout.spacing.x)/(gridLayout.spacing.x + gridLayout.cellSize.x));
			}
		}
	}
	
	void Update(){
//		Debug.Log(slotList[0].GetComponent<RectTransform>().position);
	}
		


	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{

		for (int i = 0; i < slotList.Length; i++) {
			RectTransform slotRectTransform = slotList[i].GetComponent<RectTransform>();
			float slotWidth = gridLayout.cellSize.x;
			Debug.Log("VI TRI CUA CHUOT LA: " + eventData.position.x);
			Debug.Log("VI TRI CUA SlOT LA: " + slotRectTransform.position.x);
			if((eventData.position.x) > (slotRectTransform.position.x + slotRectTransform.rect.width/2)){
				if(Mathf.Abs(eventData.position.x - slotRectTransform.position.x - gridLayout.spacing.x/2) < slotRectTransform.rect.width/2){
					Debug.Log("Ben phai slot : " + i);
					if(moreThanOneRow){
						if(Mathf.Abs(eventData.position.y - slotRectTransform.position.y) < slotRectTransform.rect.height/2){
							Debug.Log("CUNG HANG VOI CA SLOT: " + i);
							slotList[i].GetComponent<Slot>().AddWordIntoCell();
							break;
						}
						else{
							continue;
						}
			
					}
					else{
						slotList[i].GetComponent<Slot>().AddWordIntoCell();
						break;
					}
				}
//				else if((Mathf.Abs(eventData.position.x - slotRectTransform.position.x - gridLayout.spacing.x/2) > slotRectTransform.rect.width/2) && (i % objectsInOneRow) == 0){
//					slotList[i].GetComponent<Slot>().AddWordIntoCell();
//					break;
//				}

			}
			else if((eventData.position.x) < (slotRectTransform.position.x - slotRectTransform.rect.width/2) ){
				if(((Mathf.Abs(eventData.position.x - slotRectTransform.position.x) - gridLayout.spacing.x/2) < slotRectTransform.rect.width/2) && ((i % objectsInOneRow) != 0)){
					Debug.Log("Ben trai slot : " + i);
					if(moreThanOneRow){
						if(Mathf.Abs(eventData.position.y - slotRectTransform.position.y) < slotRectTransform.rect.height/2){
							Debug.Log("CUNG HANG VOI CA SLOT: " + i);
							slotList[i - 1].GetComponent<Slot>().AddWordIntoCell();
							break;
						}
						else{
							continue;
						}
									
					}
					else{
						slotList[i - 1].GetComponent<Slot>().AddWordIntoCell();
						break;
					}
				}
				else if(((Mathf.Abs(eventData.position.x - slotRectTransform.position.x) - gridLayout.spacing.x/2) < slotRectTransform.rect.width/2) && ((i % objectsInOneRow) == 0)){
					if(moreThanOneRow){
						if(Mathf.Abs(eventData.position.y - slotRectTransform.position.y) < slotRectTransform.rect.height/2){
							Debug.Log("CUNG HANG VOI CA SLOT: " + i);
							slotList[i].GetComponent<Slot>().AddWordIntoCell();
							break;
						}
						else{
							continue;
						}
						
					}
					else{
						slotList[i].GetComponent<Slot>().AddWordIntoCell();
						break;
					}
				}
//				else if(((Mathf.Abs(eventData.position.x - slotRectTransform.position.x) - gridLayout.spacing.x/2) < slotRectTransform.rect.width/2) && (i == 0)){
//					if(Mathf.Abs(eventData.position.y - slotRectTransform.position.y) < slotRectTransform.rect.height/2){
//						Debug.Log("CUNG HANG VOI CA SLOT: " + i);
//						slotList[i].GetComponent<Slot>().AddWordIntoCell();
//						break;
//					}
//					else{
//						continue;
//					}
//				}
			}
			else if(eventData.position.x == (slotRectTransform.position.x + slotRectTransform.rect.width/2) || eventData.position.x == (slotRectTransform.position.x - slotRectTransform.rect.width/2)){
				Debug.Log("O GIUA SLOT: " + i + " VA : " + (i + 1));
				if(moreThanOneRow){
					if(Mathf.Abs(eventData.position.y - slotRectTransform.position.y) < slotRectTransform.rect.height/2){
						Debug.Log("CUNG HANG VOI CA SLOT: " + i);
						slotList[i].GetComponent<Slot>().AddWordIntoCell();
						break;
					}
					else{
						continue;
					}
							
				}
				else{
					slotList[i].GetComponent<Slot>().AddWordIntoCell();
					break;
				}
			}
		}
		Debug.Log (eventData.position);
//		if(eventData.position.x > 120.5f && eventData.position.x < 130.5f){
//			foreach(Transform t in transform){
//				if(t.gameObject.GetComponent<Slot>().CellId == 9){
//					t.gameObject.GetComponent<Slot>().AddWordIntoCell();
//				}
//			}
//		}
//		else if(eventData.position.x > 180.5f && eventData.position.x < 190.5f){
//			foreach(Transform t in transform){
//				if(t.gameObject.GetComponent<Slot>().CellId == 10){
//					t.gameObject.GetComponent<Slot>().AddWordIntoCell();
//				}
//			}
//		}
//		else if(eventData.position.x > 240.5f && eventData.position.x < 250.5f){
//			foreach(Transform t in transform){
//				if(t.gameObject.GetComponent<Slot>().CellId == 11){
//					t.gameObject.GetComponent<Slot>().AddWordIntoCell();
//				}
//			}
//		}
//		else if(eventData.position.x > 300.5f && eventData.position.x < 310.5f){
//			foreach(Transform t in transform){
//				if(t.gameObject.GetComponent<Slot>().CellId == 12){
//					t.gameObject.GetComponent<Slot>().AddWordIntoCell();
//				}
//			}
//		}
//		if(eventData.hovered.Count > 0){
//			Debug.Log (eventData.hovered [0]);
////			Debug.Log (transform.GetComponent<RectTransform>().rect.width);
//
//		}


	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{

	}

	#endregion
}
