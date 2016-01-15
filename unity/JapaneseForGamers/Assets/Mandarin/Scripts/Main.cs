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
    public GameObject framePar;
    public GameObject frameParPar;
    public GameObject frameParParPar;
    public GameObject characterManagerGO;
    private CharacterManager characterManagerScript;

    private bool isRecording = false;

	// Use this for initialization
	void Start ()
    {
        posText = Resources.Load("Mandarin/json-one") as TextAsset;
        characterManagerScript = characterManagerGO.GetComponent<CharacterManager>();
        Debug.Log(posText.text);
        posJson = JsonMapper.ToObject(posText.text);
        //ShowAllPos();
    }

    private void CheckPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.name == "Front")
                    {
                        if (isRecording)
                        {
                            Vector3 v3 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10f));
                            characterManagerScript.AddPosIntoList(new Vector2(v3.x, v3.y));
                            prefabGO = Instantiate(prefabCube) as GameObject;
                            prefabGO.transform.SetParent(frame.transform);
                            prefabGO.transform.localPosition = new Vector3(-v3.x * (1.69f / 10f), -v3.y * (1.69f / 10f), 0f);
                            //prefabGO.transform.localPosition = frame.transform.InverseTransformPoint(new Vector3(v3.x * (1.69f / 10f), v3.y * (1.69f / 10f), 0f));
                            //prefabGO.transform.position = new Vector3(-v3.x * (1.69f / 10f), -v3.y * (1.69f / 10f), 0f);
                            //2 code nay deu duoc quan trong la cho 1.69f/10f vi cai bang la con cua camera
                            //prefabGO.transform.localPosition = frame.transform.TransformVector(new Vector3(-v3.x*(1.69f/10f), -v3.y * (1.69f /10f), 0f));
                            //prefabGO.transform.localPosition = new Vector3(-v3.x * (1.69f / 10f), -v3.y * (1.69f / 10f), 0f);
                            //code vo dung    
                            //prefabGO.transform.localPosition = frame.transform.InverseTransformPoint(new Vector3(v3.x * (1.69f / 10f), v3.y * (1.69f / 10f), 0f));
                        }
                        Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10f)));
                        Debug.DrawRay(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -21f)), Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10f)), Color.yellow, 5.0f);
                    }
                }
            
        }

    }
    
    private void CheckMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.08f, out hit))
        {
            Debug.Log("DANG CLICK CHUOT NE1111");
            if (Input.GetMouseButton(0))
            {
                Debug.Log("DANG CLICK CHUOT NE");
                if (hit.collider.name == "Cube(Clone)")
                {
                    Debug.Log("DANG CLICK CHUOTTRUNG NEE");
                    characterManagerScript.ShowNextStroke();
                }
            }
        }
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
                Debug.Log(prefabGO.transform.position);
                Debug.Log(prefabGO.transform.localPosition);
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
            Debug.DrawRay(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10f)), Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -21f)), Color.yellow, 5.0f);

        }
    }
	
	// Update is called once per frame
	void Update () {
        //ShowMousePosition();
        //CheckPosition();
        CheckMousePosition();
    }
}
