using UnityEngine;
using System.Collections;
using LitJson;
using UnityEngine.UI;

public class Main : MonoBehaviour {

    JsonData posJson;
    TextAsset posText;
    public GameObject prefabCube;
    private GameObject prefabGO;
    public GameObject frame;
    public GameObject characterManagerGO;
    public CharacterManager characterManagerScript;

    private bool isRecording = false;

	// Use this for initialization
	void Start ()
    {
        posText = Resources.Load("Mandarin/json-one") as TextAsset;
        //characterManagerScript = characterManagerGO.GetComponent<CharacterManager>();
        Debug.Log(posText.text);
        posJson = JsonMapper.ToObject(posText.text);
        ShowAllPos();
    }

    public void TurnOnOffRecord(Button button)
    {
        if (isRecording)
        {
            isRecording = false;
            button.gameObject.transform.GetComponentInChildren<Text>().text = "StartRecord";
        }
        else
        {
            isRecording = true;
            button.gameObject.transform.GetComponentInChildren<Text>().text = "StopRecord";
        }
    }

    void ShowAllPos()
    {
        float x;
        float y;
        int counter = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Debug.Log(posJson["pos"][counter]["x"]);
                x = ((float.Parse(posJson["pos"][counter]["x"].ToString()) - 300f * j) - 150)/100/4;
                y = ((float.Parse(posJson["pos"][counter]["y"].ToString()) - 300f * i) - 150)/100/4;
                Debug.Log("GIA TRI CUA X VA Y LA: " + x + " , " + y);
                prefabGO = Instantiate(prefabCube) as GameObject;
                prefabGO.transform.SetParent(frame.transform);
                prefabGO.transform.position = new Vector3(x, y, frame.transform.position.z);
                counter++;
            }
        }
    }

    void ShowMousePosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //characterManagerScript.SetHaltAnimation();
            Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10f)));
        }
    }
	
	// Update is called once per frame
	void Update () {
        ShowMousePosition();
    }
}
