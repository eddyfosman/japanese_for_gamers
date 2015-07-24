using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnswerPanel : MonoBehaviour, IDropHandler,IDragHandler {

	GameObject[] slotList;
	RectTransform answerPanelRectTrans;
	GridLayoutGroup gridLayout;


	// Use this for initialization
	void Start () {
		gridLayout = gameObject.GetComponent<GridLayoutGroup> ();
		answerPanelRectTrans = transform.GetComponent<RectTransform>();
		slotList = GameObject.FindGameObjectsWithTag("Slot");
		Debug.Log (slotList[0].GetComponent<RectTransform>().rect.width);
		if(slotList != null){
			RectTransform slotRectTransform = slotList[0].GetComponent<RectTransform>();
			if(((slotList.Length - 1) * gridLayout.spacing.x + (slotList.Length * gridLayout.cellSize.x)) > answerPanelRectTrans.rect.width){
				Debug.Log("Vuot qua kich co cua panel");
			}
		}
	}
	
	void Update(){
		Debug.Log(slotList[0].GetComponent<RectTransform>().position);
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
				}

			}
			else if((eventData.position.x) < (slotRectTransform.position.x - slotRectTransform.rect.width/2)){
				if((Mathf.Abs(eventData.position.x - slotRectTransform.position.x) - gridLayout.spacing.x/2) < slotRectTransform.rect.width/2){
					Debug.Log("Ben trai slot : " + i);
				}
			}
			else if(eventData.position.x == (slotRectTransform.position.x + slotRectTransform.rect.width/2) || eventData.position.x == (slotRectTransform.position.x - slotRectTransform.rect.width/2)){
				Debug.Log("O GIUA SLOT: " + i + " VA : " + (i + 1));
			}
		}
		Debug.Log (eventData.position);
		if(eventData.position.x > 120.5f && eventData.position.x < 130.5f){
			foreach(Transform t in transform){
				if(t.gameObject.GetComponent<Slot>().CellId == 9){
					t.gameObject.GetComponent<Slot>().AddWordIntoCell();
				}
			}
		}
		else if(eventData.position.x > 180.5f && eventData.position.x < 190.5f){
			foreach(Transform t in transform){
				if(t.gameObject.GetComponent<Slot>().CellId == 10){
					t.gameObject.GetComponent<Slot>().AddWordIntoCell();
				}
			}
		}
		else if(eventData.position.x > 240.5f && eventData.position.x < 250.5f){
			foreach(Transform t in transform){
				if(t.gameObject.GetComponent<Slot>().CellId == 11){
					t.gameObject.GetComponent<Slot>().AddWordIntoCell();
				}
			}
		}
		else if(eventData.position.x > 300.5f && eventData.position.x < 310.5f){
			foreach(Transform t in transform){
				if(t.gameObject.GetComponent<Slot>().CellId == 12){
					t.gameObject.GetComponent<Slot>().AddWordIntoCell();
				}
			}
		}
		if(eventData.hovered.Count > 0){
			Debug.Log (eventData.hovered [0]);
//			Debug.Log (transform.GetComponent<RectTransform>().rect.width);

		}


	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{

	}

	#endregion
}
