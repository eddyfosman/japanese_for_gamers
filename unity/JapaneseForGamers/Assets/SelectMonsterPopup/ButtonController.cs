using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonController : MonoBehaviour {

	public GameObject selectMonsterPopupGO;
	public Button proceedBtn;
	public GameObject monsterSelectPopup;
	private MonsterInventory monsterInventoryScript;
	public bool onCD;
	private bool once = false;
	private bool shuffeld = false;
	public Image[] visualMonsterEquipped;
	private List<string> visualMonsterNameList = new List<string>();


	// Use this for initialization
	void Start () {
		monsterInventoryScript = monsterSelectPopup.GetComponent<MonsterInventory> ();
		onCD = true;
		shuffeld = false;
	}

	private void Shuffle<T> (List<T> list)
	{
		int count = list.Count;
		for (int i = count - 1; i > 0; i--) {
			int randIndex = Random.Range (0, i);
			T temp = list [i];
			list [i] = list [randIndex];
			list [randIndex] = temp;
			
		}
		Debug.Log ("SHUFFLED");
		shuffeld = true;
	}

	public void HideSelectMonsterPopup(){
		foreach(int key in monsterInventoryScript.hashtableMonster.Keys){
			visualMonsterNameList.Add ((string)monsterInventoryScript.hashtableMonster[key]);
			Debug.Log((string)monsterInventoryScript.hashtableMonster[key]);
		}
		Shuffle(visualMonsterNameList);
		
		for(int i = 0 ; i < visualMonsterEquipped.Length; i++){
			visualMonsterEquipped[i].sprite = Resources.Load("Sprites/" + visualMonsterNameList[i], typeof(Sprite)) as Sprite;
			Debug.Log(visualMonsterNameList[i]);
		}
		selectMonsterPopupGO.SetActive (false);
		onCD = false;

	}



	// Update is called once per frame
	void Update () {

		if(4 == monsterInventoryScript.StackCount){

			proceedBtn.GetComponent<Selectable>().interactable = true;


		}
		else{
			proceedBtn.GetComponent<Selectable>().interactable = false;
		}
	}
}
