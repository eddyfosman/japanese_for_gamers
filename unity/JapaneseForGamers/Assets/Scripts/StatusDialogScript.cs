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

	public Button backButton;
	private Selectable backButtonSelectable;

	public bool onCD = false;

	public void RetrievPlayerData(){
		gameObject.SetActive (true);
		onCD = true;


		Debug.Log (player.Level);
		level.text = "Level \t" + player.Level.ToString();
		hp.text = "HP \t" + player.Hp.ToString();
		atk.text = "ATK \t\t" + player.Atk.ToString();
		def.text = "DEF \t" + player.Def.ToString();

	}

	// Use this for initialization
	void Start () {
		backButtonSelectable = backButton.GetComponent<Selectable> ();
		questionManagerScript = questionManager.GetComponent<QuestionManager> ();
		backButtonSelectable.interactable = false;
		player = questionManagerScript.GetPlayerData ();
		RetrievPlayerData ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
