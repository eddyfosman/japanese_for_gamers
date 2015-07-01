using UnityEngine;
using System.Collections;

public class DeviceManager : MonoBehaviour {

	public Canvas canvas1;
	public Canvas canvas2;

	


	// Use this for initialization
	void Start () {
#if UNITY_WEBGL
		Debug.Log("WEBPLAYER");
		canvas1.gameObject.SetActive(true);
		canvas2.gameObject.SetActive(false);
		Screen.orientation = ScreenOrientation.Landscape;
#endif
#if UNITY_ANDROID

		canvas1.gameObject.SetActive(true);
		canvas2.gameObject.SetActive(false);
		Screen.orientation = ScreenOrientation.Portrait;
		
#endif


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
