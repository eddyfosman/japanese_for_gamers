using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour {

	public GameObject questionPanel;
	private QuestionFadeScript questionFadeScript;

	public GameObject monsterPanel;
	private MonsterFadeScript monsterFadeScript;

	public GameObject playerHealthBar;
	private PlayerHealthBar playerHealthBarScript;

	public GameObject quesManager;
	private QuestionManager questionManagerScript;

	public GameObject statusDialog;
	private StatusDialogScript statusDialogScript;

	public GameObject buttonController;
	private ButtonController buttonControllerScript;

	public float speed;
	public Image visualTime;
	public RectTransform timeBarTransform;
	public int maxTime;
	private int currentTime;
	private int CurrentTime{
		get{return currentTime;}
		set{
			currentTime = value;
			HandleTime();
		}
	}
	private bool onCD = false;
	public float coolDown;
	private float cachedY;
	private float minXValue;
	private float maxXValue;
	private Vector3 oldPos;
	public float smooth;

	private float MapValues(float x, float inMin, float inMax, float outMin, float outMax){
		return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
	}

	private void HandleTime(){
		float currentXValue = MapValues (currentTime, 0, maxTime, minXValue, maxXValue);
//		timeBarTransform.position = new Vector3 (currentXValue, cachedY);
//		timeBarTransform.GetComponent<RectTransform>().position = new Vector3 (currentXValue, cachedY);
		timeBarTransform.GetComponent<RectTransform> ().offsetMax = new Vector2 (currentXValue, GetComponent<RectTransform> ().offsetMax.y);
		timeBarTransform.GetComponent<RectTransform> ().offsetMin = new Vector2 (currentXValue, GetComponent<RectTransform> ().offsetMin.y);
//		Debug.Log ("currentXValue la : "  + currentXValue);
//		Debug.Log ("currentTime la : "  + currentTime);
//		Debug.Log ("maxTime la : " + maxTime);
//		Debug.Log ("minXValue la : " + minXValue);
//		Debug.Log ("maxXValue la : " + maxXValue);
//		Debug.Log ("VI TRI HIEN TAI CUA THANH THOI GIAN: " + timeBarTransform.position);

	}

	public void ResetTimeBar(){
		questionManagerScript.SetQuestion();
		currentTime = maxTime;
		timeBarTransform.position = new Vector3 (maxXValue, cachedY);

	}

	IEnumerator CoolDownTime(){
		onCD = true;
		yield return new WaitForSeconds (coolDown);
		onCD = false;
	}

	void ShowParticleWhenNotAnswer(){
		questionManagerScript.ShowParticleWhenNotAnswer (questionManagerScript.deadGO);
		SetButtonInteractibleFalse ();
		ShowOnlyRightAnswerButton ();
	}

	void SetDeadParticleFalse(){
		questionManagerScript.deadGO.SetActive (false);
	}

	void ShowQuestion(){
		questionFadeScript.ShowQuestion ();
	}

	void SetButtonInteractibleFalse(){
		questionFadeScript.SetButtonInteractiableFalse();
	}

	void ShowOnlyRightAnswerButton(){
		questionManagerScript.ShowOnlyRightAnswerButton ();
	}

	void ShowAllAnswerButton(){
		questionManagerScript.ShowAllAnswerButton ();
	}

	// Use this for initialization
	void Start () {
//		Debug.Log ("VI TRI HIEN TAI CUA THANH THOI GIAN: " + timeBarTransform.position);
		questionFadeScript = questionPanel.GetComponent<QuestionFadeScript>();
		monsterFadeScript = monsterPanel.GetComponent<MonsterFadeScript>();
		playerHealthBarScript = playerHealthBar.GetComponent<PlayerHealthBar> ();
		questionManagerScript = quesManager.GetComponent<QuestionManager>();
		statusDialogScript = statusDialog.GetComponent<StatusDialogScript> ();
		buttonControllerScript = buttonController.GetComponent<ButtonController> ();
		cachedY = timeBarTransform.position.y;
		maxXValue = timeBarTransform.position.x;
		minXValue = timeBarTransform.position.x - timeBarTransform.rect.width;
		currentTime = maxTime;
		oldPos = timeBarTransform.position;
	}



	// Update is called once per frame
	void Update () {
		if(!onCD && currentTime > 0 && !questionFadeScript.onCD && !monsterFadeScript.onCD && !statusDialogScript.onCD && !buttonControllerScript.onCD){
			StartCoroutine(CoolDownTime());
			CurrentTime -= 1;
//			Debug.Log("Thoi GIAN CON LAI LA: " + CurrentTime);
			if(currentTime == 0){

				ShowParticleWhenNotAnswer();
				Invoke("SetDeadParticleFalse", questionManagerScript.CompareParticleDuration());
				Invoke("ResetTimeBar", questionManagerScript.CompareParticleDuration());
				Invoke("ShowQuestion", questionManagerScript.CompareParticleDuration());
				Invoke("ShowAllAnswerButton", questionManagerScript.CompareParticleDuration());
				playerHealthBarScript.Damage();
			}
		}
		oldPos = timeBarTransform.position;
	}
}
