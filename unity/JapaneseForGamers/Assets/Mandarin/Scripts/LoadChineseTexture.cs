using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class LoadChineseTexture : MonoBehaviour {

    public int padding = 10;                //Global padding of app elements
    public Texture prevButton;              //Texture for previous button
    public Texture nextButton;              //Texture for next button
    public Texture reloadButton;            //Texture for reload button
    public Texture scrollText;              //Texture for text and data
    public Font titleFont;                  //Main title font
    public Color titleColor = Color.white;  //Main title color
    public int titleHeigth = 25;              //Main title height
    //GUI data
    GUIStyle titleStyle;                //Style for title
    GUIStyle contentStyle;              //Style for content

    int current = 0;
    private CharacterDatabase.Character cs = null;
    string pathSound, pathTexture;


    public Text inputText;
    public Text inputUnicode;


	// Use this for initialization
	void Start () {

        transform.Find("Canvas01/Front").gameObject.GetComponent<CharacterManager>().CallBackFunction = EventManager;

        //Defines title style
        titleStyle = new GUIStyle();
        titleStyle.normal.textColor = titleColor;
        titleStyle.alignment = TextAnchor.MiddleCenter;
        titleStyle.wordWrap = true;
        titleStyle.fontSize = titleHeigth;
        if (titleFont != null)
            titleStyle.font = titleFont;
        else
            titleStyle.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        //Defines content style
        contentStyle = new GUIStyle();
        contentStyle.normal.textColor = Color.black;
        contentStyle.alignment = TextAnchor.MiddleCenter;
        contentStyle.wordWrap = true;
        contentStyle.fontStyle = FontStyle.Bold;
        contentStyle.fontSize = 17;
        contentStyle.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

        SetCurrentChar(current);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {

        DrawOutline(new Rect(padding, padding, Screen.width - (2 * padding), titleHeigth), "Word " + (current + 1) + " of " + CharacterDatabase.instance.CharactersDB.Count, titleStyle, Color.black, titleColor);

        float size = Mathf.Min(512, Mathf.Min(Screen.height / 7, Screen.width / 7));
        Color ant = GUI.backgroundColor;
        GUI.backgroundColor = Color.clear;

        float deltatx = (Screen.width - (3 * size)) / 4;

        if (current > 0)
        {
            GUI.DrawTexture(new Rect(deltatx, Screen.height - (padding + size), size, size), prevButton);
            if (GUI.Button(new Rect(deltatx, Screen.height - (padding + size), size, size), ""))
            {
                current--;
                SetCurrentChar(current);
            }
        }

        GUI.DrawTexture(new Rect((Screen.width - size) / 2, Screen.height - (padding + size), size, size), reloadButton);
        if (GUI.Button(new Rect((Screen.width - size) / 2, Screen.height - (padding + size), size, size), ""))
            SetCurrentChar(current);

        if (current < CharacterDatabase.instance.CharactersDB.Count-1)
        {
            GUI.DrawTexture(new Rect(Screen.width - (deltatx + size), Screen.height - (padding + size), size, size), nextButton);
            if (GUI.Button(new Rect(Screen.width - (deltatx + size), Screen.height - (padding + size), size, size), ""))
            {
                current++;
                SetCurrentChar(current);
            }
        }

        GUI.DrawTexture(new Rect(2 * padding, Screen.height / 4, Screen.width / 4, Screen.height / 2), scrollText);
        GUI.Label(new Rect((2 * padding) + 10, Screen.height / 4, (Screen.width / 4) - 20, Screen.height / 2), "Meaning\n\n" + cs.english, contentStyle);
        //GUI.DrawTexture(new Rect(2 * padding + (Screen.width * 2), Screen.height / 4, Screen.width / 4, Screen.height / 2), scrollText);
        //GUI.Label(new Rect((2 * padding) + 10 + (Screen.width * 2), Screen.height / 4, (Screen.width / 4) - 20, Screen.height / 2), "Meaning\n\n" + cs.english, contentStyle);

        string typec ="Simplified";
        switch(cs.Type) {
            case 1:
                typec="Simplified";
                break;
            case 2:
                typec="Traditional";
                break;
            case 3:
                typec = "Simplified and Traditional";
                break;
        }

        string textdesc="Pinyin: "+cs.pinyin+"\nStrokes: "+cs.Strokes+"\n"+typec+"\nUnicode: "+cs.Unicode;
        //GUI.DrawTexture(new Rect(Screen.width - ((2 * padding) + (Screen.width * 2)), Screen.height / 4, Screen.width / 4, Screen.height / 2), scrollText);
        //GUI.Label(new Rect(10 + Screen.width - ((2 * padding) + (Screen.width * 2)), Screen.height / 4, (Screen.width / 4) - 20, Screen.height / 2), "Data\n\n" + textdesc, contentStyle);
        GUI.DrawTexture(new Rect(Screen.width - ((2 * padding) + (Screen.width / 4)), Screen.height / 4, Screen.width / 4, Screen.height / 2), scrollText);
        GUI.Label(new Rect(10 + Screen.width - ((2 * padding) + (Screen.width / 4)), Screen.height / 4, (Screen.width / 4) - 20, Screen.height / 2), "Data\n\n" + textdesc, contentStyle);

        GUI.Label(new Rect(padding, Screen.height-(padding+15), Screen.width, 25), "This App uses CC-CEDICT as dictionary");

        GUI.backgroundColor = ant;

    }

    public void SetCurrentChar(int numChar)
    {
        

        current = numChar;
        cs = CharacterDatabase.instance.GetChatAtPos(current);
        transform.Find("Canvas01/Front").gameObject.GetComponent<CharacterManager>().StopAnimation();
        transform.Find("Canvas01/Front").gameObject.GetComponent<CharacterManager>().SetCharacter(cs.Ideogram);

        GetComponent<PinyinManager>().PlayPinyin(cs.pinyin, Camera.main.GetComponent<AudioSource>());
    }

    public void SetCurrentChar()
    {
        if (transform.Find("Canvas01/Front").gameObject.GetComponent<CharacterManager>().NumChild() > 0)
        {
            return;
        }
        int numChar;
        if (inputText.text != null && int.TryParse(inputText.text, out numChar))
        {
            current = numChar - 1;
        }
        else
        {
            return;
        }

        
        cs = CharacterDatabase.instance.GetChatAtPos(current);
        transform.Find("Canvas01/Front").gameObject.GetComponent<CharacterManager>().ClearList();
        transform.Find("Canvas01/Front").gameObject.GetComponent<CharacterManager>().StopAnimation();
        transform.Find("Canvas01/Front").gameObject.GetComponent<CharacterManager>().SetCharacter(cs.Ideogram);

        GetComponent<PinyinManager>().PlayPinyin(cs.pinyin, Camera.main.GetComponent<AudioSource>());
    }

    public void SetCurrentCharByUnicode()
    {
        if (transform.Find("Canvas01/Front").gameObject.GetComponent<CharacterManager>().NumChild() > 0)
        {
            return;
        }
        if (inputUnicode.text != null && inputUnicode.text != "")
        {
            object[] objArr = CharacterDatabase.instance.FindUnicode2(inputUnicode.text);
            if (objArr != null)
            {
                cs = (CharacterDatabase.Character)objArr[0];
                current = (int)objArr[1];
            }
            else
            {
                return;
            }
            transform.Find("Canvas01/Front").gameObject.GetComponent<CharacterManager>().ClearList();
            transform.Find("Canvas01/Front").gameObject.GetComponent<CharacterManager>().StopAnimation();
            transform.Find("Canvas01/Front").gameObject.GetComponent<CharacterManager>().SetCharacter(cs.Ideogram);

            GetComponent<PinyinManager>().PlayPinyin(cs.pinyin, Camera.main.GetComponent<AudioSource>());
        }
        else
        {
            return;
        }
    }

    //Receives the events from CharacterManager (start and end animation)
    void EventManager(CharacterManager.EventInfo eventInfo)
    {
        //Debug.Log("EVENT " + eventInfo.eventType.ToString());
        if (eventInfo.eventType == CharacterManager.EventType.FinishAnimation)
        {
        }

        if (eventInfo.eventType == CharacterManager.EventType.StartAnimation)
        {
        }

        if (eventInfo.eventType == CharacterManager.EventType.TextureLoaded)
        {
            transform.Find("Canvas01/Front").gameObject.GetComponent<CharacterManager>().StartAnimation();
        }
    }

    //draw text of a specified color, with a specified outline color
    void DrawOutline(Rect position, string text, GUIStyle theStyle, Color outColor, Color inColor)
    {
        theStyle.normal.textColor = outColor;
        position.x--;
        GUI.Label(position, text, theStyle);
        position.x += 2;
        GUI.Label(position, text, theStyle);
        position.x--;
        position.y--;
        GUI.Label(position, text, theStyle);
        position.y += 2;
        GUI.Label(position, text, theStyle);
        position.y--;
        theStyle.normal.textColor = inColor;
        GUI.Label(position, text, theStyle);
        
    }
}
