using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using LitJson;

public class ButtonController : MonoBehaviour {

	public GameObject selectMonsterPopupGO;
	public Button proceedBtn;
	public GameObject monsterSelectPopup;
	private MonsterInventory2 monsterInventoryScript;
	public bool onCD;
	private bool once = false;
	private bool shuffeld = false;
	public Image[] visualMonsterEquipped;
	private List<string> visualMonsterNameList = new List<string>();
	private InventoryCellView2 inventory;
	private Hashtable hashtable;
	public GameObject selectTeamPopup;
	public GameObject selectMonsterPopup;
	public GameObject inventoryGO;
	private TextAsset jsonMonsterFile;
	private JsonData jsonMonsters;

	// Use this for initialization
	void Start () {
		jsonMonsterFile = Resources.Load ("monstersequip") as TextAsset;
		jsonMonsters = JsonMapper.ToObject (jsonMonsterFile.text);
		inventoryGO = GameObject.FindGameObjectWithTag ("Inventory");
//		inventory = new InventoryCellView2 ();
		inventory = inventoryGO.AddComponent<InventoryCellView2>();
//		inventory = inventoryGO.AddComponent(
		hashtable = inventory.ReturnHashTable;
		
		monsterInventoryScript = monsterSelectPopup.GetComponent<MonsterInventory2> ();
		onCD = true;
		shuffeld = false;
	}

	public void BackToSelectTeam(){
		selectTeamPopup.GetComponent<CanvasGroup>().alpha = 1;
		selectTeamPopup.GetComponent<CanvasGroup>().interactable = true;
		selectTeamPopup.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		selectMonsterPopup.GetComponent<CanvasGroup>().alpha = 0;
		selectMonsterPopup.GetComponent<CanvasGroup>().interactable = false;
		selectMonsterPopup.GetComponent<CanvasGroup>().blocksRaycasts = false;
		inventory.ResetStackAndHashtable ();
		monsterInventoryScript.ResetListMonsterSelected ();
		monsterInventoryScript.Reload (false);

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


		foreach(string key in hashtable.Keys){
			visualMonsterNameList.Add ((string)hashtable[key]);
			Debug.Log((string)hashtable[key]);
		}
		Shuffle(visualMonsterNameList);
		
		for(int i = 0 ; i < visualMonsterEquipped.Length; i++){
			visualMonsterEquipped[i].sprite = Resources.Load("Sprites/" + visualMonsterNameList[i], typeof(Sprite)) as Sprite;
			for(int j = 0; j < jsonMonsters["monsters"].Count; j++){
				if(jsonMonsters["monsters"][j]["image"].ToString() == visualMonsterNameList[i]){
					MonsterEquip mq = visualMonsterEquipped[i].gameObject.AddComponent<MonsterEquip>();
					mq.ID = jsonMonsters["monsters"][j]["id"].ToString();
					mq.Image = jsonMonsters["monsters"][j]["image"].ToString();
					mq.PosX = float.Parse(jsonMonsters["monsters"][j]["posx"].ToString());
					mq.PosY = float.Parse(jsonMonsters["monsters"][j]["posy"].ToString());
					mq.AddValue("atk",jsonMonsters["monsters"][j]["bonus"][0]["atk"].ToString());
					mq.AddValue("def",jsonMonsters["monsters"][j]["bonus"][0]["def"].ToString());
					mq.AddValue("hp",jsonMonsters["monsters"][j]["bonus"][0]["hp"].ToString());
					mq.EffectProperty.Turn = int.Parse(jsonMonsters["monsters"][j]["effect"][0]["turn"].ToString());
					mq.EffectProperty.Apply = jsonMonsters["monsters"][j]["effect"][0]["apply"].ToString();
					mq.EffectProperty.Value = int.Parse(jsonMonsters["monsters"][j]["effect"][0]["value"].ToString());
					mq.EffectProperty.Type = jsonMonsters["monsters"][j]["effect"][0]["type"].ToString();
				}

			}
			Debug.Log(visualMonsterNameList[i]);
		}
		selectMonsterPopupGO.SetActive (false);
		onCD = false;

	}



	// Update is called once per frame
	void Update () {

		if(4 == inventory.StackCount){

			proceedBtn.GetComponent<Selectable>().interactable = true;


		}
		else{
			proceedBtn.GetComponent<Selectable>().interactable = false;
		}
	}
}
