using UnityEngine;
using System.Collections;
using EnhancedUI;
using System.Collections.Generic;

public class MonsterEquippableListItem {

	public UnityEngine.UI.Image monsterImage;
	public UnityEngine.UI.Text monsterName;

	public static List<string> monsterSelected = new List<string>();

//	public override void SetData(object objectData){
//		MonsterEquippableData data = (objectData as MonsterEquippableData);
//		Debug.Log (data.monsterName);
//		if(monsterSelected.Count > 0){
//			for(int i = 0; i < monsterSelected.Count; i++){
//				if(monsterSelected[i] == monsterName.text && transform.gameObject.GetComponentInChildren<UnityEngine.UI.Selectable> ().interactable == true){
//					Debug.Log("--------"+monsterSelected[i]);
//					transform.gameObject.GetComponentInChildren<UnityEngine.UI.Selectable> ().interactable = false;
//				}
////				else if(transform.gameObject.GetComponentInChildren<UnityEngine.UI.Selectable> ().interactable == false && monsterSelected[i] != monsterName.text){
////
////					transform.gameObject.GetComponentInChildren<UnityEngine.UI.Selectable> ().interactable = true;
////				}
//			}
//		}
//
//		monsterImage.sprite = Resources.Load ("Sprites/" + data.monsterName ,typeof (Sprite) ) as Sprite;
//		monsterName.text = data.monsterName;
//		
//		
//	}

//	public override void ItemSelected(){
////		MonsterData data = new MonsterData ();
////		data.SelectedID = monsterID.text;
////		data.SelectedMonsterName = monsterName.text;
////		Application.LoadLevel (3);
//		if(monsterSelected.Count < 4){
//			monsterSelected.Add(monsterName.text);
//		}
//		transform.gameObject.GetComponentInChildren<UnityEngine.UI.Selectable> ().interactable = false;
//	}
}
