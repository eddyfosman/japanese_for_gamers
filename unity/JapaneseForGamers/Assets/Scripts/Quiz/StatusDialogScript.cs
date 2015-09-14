using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusDialogScript : MonoBehaviour {

	public GameObject questionManager;
	private QuestionManager questionManagerScript;
	PlayerData player;

	public Text level;
	public Text hp;
	public Text atk;
	public Text def;
	public Text agi;
	public Text luk;
	public Text bonusPoints;

	PlayerData tempPlayer;

	public Button backButton;
	private Selectable backButtonSelectable;

	public Button applyButton;
	private Selectable applyButtonSelectable;

	public Button cancelButton;
	private Selectable cancelButtonSelectable;

	public Button addHpButton;
	private Selectable addHpButtonSelectable;

	public Button addAtkButton;
	private Selectable addAtkButtonSelectable;

	public Button addDefButton;
	private Selectable addDefButtonSelectable;

	public Button addAgiButton;
	private Selectable addAgiButtonSelectable;

	public Button addLukButton;
	private Selectable addLukButtonSelectable;



	public bool onCD = false;
	public bool onChange = false;

	public void ShowStatusDialog(){
		onCD = true;
		gameObject.SetActive (true);
	}

	public void HideStatusDialog(){
		if(onChange){
			CancelStatusChange();
		}
		onCD = false;
		gameObject.SetActive (false);
	}

	public void AddPointHp(){
		tempPlayer.Hp++;
		hp.text = "HP \t" + tempPlayer.Hp;
		tempPlayer.BonusPoint--;
		bonusPoints.text = "Bonus Points\t" + tempPlayer.BonusPoint;
		onChange = true;

	}

	public void AddPointAtk(){
		tempPlayer.Atk++;
		atk.text = "ATK \t\t" + tempPlayer.Atk;
		tempPlayer.BonusPoint--;
		bonusPoints.text = "Bonus Points\t" + tempPlayer.BonusPoint;
		onChange = true;
	}

	public void AddPointDef(){
		tempPlayer.Def++;
		def.text = "DEF \t" + tempPlayer.Def;
		tempPlayer.BonusPoint--;
		bonusPoints.text = "Bonus Points\t" + tempPlayer.BonusPoint;
		onChange = true;
	}

	public void AddPointAgi(){
		tempPlayer.Agi++;
		agi.text = "AGI \t" + tempPlayer.Agi;
		tempPlayer.BonusPoint--;
		bonusPoints.text = "Bonus Points\t" + tempPlayer.BonusPoint;
		onChange = true;
	}

	public void AddPointLuk(){
		tempPlayer.Luk++;
		luk.text = "LUK \t" + tempPlayer.Luk;
		tempPlayer.BonusPoint--;
		bonusPoints.text = "Bonus Points\t" + tempPlayer.BonusPoint;
		onChange = true;
	}

	public void ApplyStatusChange(){
		player.Hp = tempPlayer.Hp;
		player.Atk = tempPlayer.Atk;
		player.Def = tempPlayer.Def;
		player.Agi = tempPlayer.Agi;
		player.Luk = tempPlayer.Luk;
		player.BonusPoint = tempPlayer.BonusPoint;
		onChange = false;
	}

	public void CancelStatusChange(){
		Debug.Log ("-------player" + player.BonusPoint);
		Debug.Log ("-------tempplayer" + tempPlayer.BonusPoint);
		Debug.Log ("---tempplayerlevel" + tempPlayer.Level);
		hp.text = "HP \t" + player.Hp;
		atk.text = "ATK \t\t" + player.Atk;
		def.text = "DEF \t" + player.Def;
		agi.text = "AGI \t" + player.Agi;
		luk.text = "LUK \t" + player.Luk;
		bonusPoints.text = "Bonus Points\t" + player.BonusPoint;
		onChange = false;
	}

	public void RetrievPlayerData(){


		player = questionManagerScript.GetPlayerData ();
//		Debug.Log (player.Level);
		tempPlayer = new PlayerData ();
		tempPlayer.Atk = player.Atk;
		tempPlayer.Def = player.Def;
		tempPlayer.Hp = player.Hp;
		tempPlayer.Agi = player.Agi;
		tempPlayer.Luk = player.Luk;
		tempPlayer.BonusPoint = player.BonusPoint;

		level.text = "Level \t" + player.Level;
		hp.text = "HP \t" + player.Hp;
		atk.text = "ATK \t\t" + player.Atk;
		def.text = "DEF \t" + player.Def;
		agi.text = "AGI \t" + player.Agi;
		luk.text = "LUK \t" + player.Luk;
		bonusPoints.text = "Bonus Points\t" + player.BonusPoint.ToString ();

	}

	// Use this for initialization
	void Start () {
		Debug.Log ("Chay start statusdialogscript");
		backButtonSelectable = backButton.GetComponent<Selectable> ();
		applyButtonSelectable = applyButton.GetComponent<Selectable> ();
		cancelButtonSelectable = cancelButton.GetComponent<Selectable> ();
		addHpButtonSelectable = addHpButton.GetComponent<Selectable>();
		addAtkButtonSelectable = addAtkButton.GetComponent<Selectable>();
		addDefButtonSelectable = addDefButton.GetComponent<Selectable>();
		addAgiButtonSelectable = addAgiButton.GetComponent<Selectable>();
		addLukButtonSelectable = addLukButton.GetComponent<Selectable>();
		questionManagerScript = questionManager.GetComponent<QuestionManager> ();

		applyButtonSelectable.interactable = false;


	}
	
	// Update is called once per frame
	void Update () {
		onCD = true;
		return;
		if(onCD){
			if(!onChange){
				RetrievPlayerData ();
			}

			if(tempPlayer.BonusPoint == 0){
				addHpButtonSelectable.interactable = false;
				addAtkButtonSelectable.interactable = false;
				addDefButtonSelectable.interactable = false;
				addAgiButtonSelectable.interactable = false;
				addLukButtonSelectable.interactable = false;
			}
			else{
				addHpButtonSelectable.interactable = true;
				addAtkButtonSelectable.interactable = true;
				addDefButtonSelectable.interactable = true;
				addAgiButtonSelectable.interactable = true;
				addLukButtonSelectable.interactable = true;
			}

			if(onChange){
				applyButtonSelectable.interactable = true;
				cancelButtonSelectable.interactable = true;
			}
			else{
				applyButtonSelectable.interactable = false;
				cancelButtonSelectable.interactable = false;
			}
		}
	}
}
