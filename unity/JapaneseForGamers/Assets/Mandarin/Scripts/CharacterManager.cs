using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;


public class CharacterManager : MonoBehaviour {

    public enum EventType {
		StartAnimation, FinishAnimation, TextureLoaded
	}

    public class EventInfo {
		public EventType eventType;
		public CharacterManager sender;
	}

    public string Character;
    public float FramesPerSecond = 10f;
    public bool AutoStart = false;
    public bool LoopAnimation = false;
    public float waitOnEnd = 0.3f;
    public GameObject prefabCube;
    private GameObject prefabGO;

    public delegate void CallbackEventHandler(EventInfo eventInfo);
    public CallbackEventHandler CallBackFunction;

    public Texture textureChar;

    private CharacterDatabase.Character cs=null;
    private Material chrMat;
    private Texture chrText;
    private int Columns=1;
    private int Rows=1;
    private int NumImages;
    private bool started = false;
    private bool haltAnimation = false;
    private bool breakUpdateTiling = false;
    private bool reverseUpdateTilingOne = false;
    private bool updateTilingOne = false;
    private bool breakOriUpdateTiling = false;
    private bool breakReverseUpdateTiling = false;
    private int currentStroke = 0;

    private List<Transform> linesList = new List<Transform>();
    private List<Vector2> posList = new List<Vector2>();
    private List<Vector2> offsetList = new List<Vector2>();
    private List<Vector2> displayedStroke = new List<Vector2>();
    private Dictionary<Vector2, Vector3> displayedStrokeVsValue = new Dictionary<Vector2, Vector3>();

    public void AddPosIntoList(Vector2 v2)
    { 
        if (v2 != null)
        {
            displayedStroke.Add(v2);
        }
    }

    private int cachedRow;
    public int CachedRow
    {
        get { return cachedRow; }
        set { cachedRow = value; }
    }

    private int cachedColumn;
    public int CachedColumn
    {
        get { return cachedColumn; }
        set { cachedColumn = value; }
    }

    private int cachedTotal;
    public int CachedTotal
    {
        get { return cachedTotal; }
        set { cachedTotal = value; }
    }

    //If you are compiling for Web set USE_WEB to true and put the apropriate web path
    //If you are compiling for descktop or mobile ser web to false
    //Remember to copy all textures to the Textures directory and pinyin sounds to the PinyinSounds directory
    //These two directories must be located in the correspondent webPath or hdPath 
    //The hdPath must start where the App is located
    public static readonly bool USE_WEB = false;
    public static readonly string webPath = "put_you_server/and_path_here/";
    public static readonly string hdPath = "ChineseCharacters/";
    string pathTexture;

    

	// Use this for initialization
	void Start () {
        chrMat = new Material(Shader.Find("Transparent/Diffuse"));
        GetComponent<Renderer>().sharedMaterial = chrMat;
        GetComponent<Renderer>().material.mainTexture = (Texture2D)Resources.Load("trans", typeof(Texture2D));
        //PrepareTexture();
        if (AutoStart)
        {
            StartAnimation();
        }
	}

    //public void StartAnimation()
    //{
    //    if (started)
    //        return;
    //    if (CallBackFunction != null)
    //    {
    //        EventInfo einfo = new EventInfo();
    //        einfo.sender = this;
    //        einfo.eventType = EventType.StartAnimation;
    //        CallBackFunction(einfo);
    //    }
    //    started = true;
    //    Debug.Log("CO VAO DAY KHONG??");
    //    StartCoroutine(UpdateTiling());
    //    LoadPositionCoroutine();
    //    SetFalseReverseUpdateTiling();
    //}

    public void StartAnimation()
    {
        if (started)
            return;
        if (CallBackFunction != null)
        {
            EventInfo einfo = new EventInfo();
            einfo.sender = this;
            einfo.eventType = EventType.StartAnimation;
            CallBackFunction(einfo);
        }
        started = true;
        Debug.Log("CO VAO DAY KHONG??");
        //StartCoroutine(UpdateTiling());
        offsetList.Clear();
        LoadAllOffsetValue();
        LoadPositionCoroutine();
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offsetList[1]);
        //SetFalseReverseUpdateTiling();

    }

    private void LoadAllOffsetValue()
    {
        float x = 0f;
        float y = 0f;
        Vector2 offset = Vector2.zero;
        int total = 0;
        for (int i = Rows - 1; i >= 0; i--)
        {
            y = (float)i / (float)Rows;

           
            for (int j = 0; j <= Columns - 1; j++)
            {
                total++;
                if (total > NumImages)
                    break;
                Debug.Log("TONG SO TOTAL LA: " + total);
                x = (float)j / (float)Columns;
                offset.Set(x, y);
                offsetList.Add(offset);
            }
            

        }

    }

    public void StopAnimation() {
        started = false;
        StopAllCoroutines();
    }
	
	public void SetCharacter(string chr, Texture2D text)
    {
        cs = CharacterDatabase.instance.FindCharacter(chr);
        if (cs == null)
        {
            Debug.Log("Character "+chr+" not found in database!");
            return;
        }
        Character = chr;
        textureChar = text;
        PrepareTexture();
    }

    public void ShowNextStroke()
    {
        //SetFalseReverseUpdateTiling();
        linesList[currentStroke].gameObject.SetActive(false);
        currentStroke++;
        if (currentStroke < linesList.Count)
        {
            linesList[currentStroke].gameObject.SetActive(true);
            if (linesList.Count - 1 != currentStroke)
                GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offsetList[currentStroke + 1]);
            else
                GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offsetList[currentStroke + 2]);

            Debug.Log("SO PHAN TU TRONG OFFSET LIST: " + offsetList.Count);
            Debug.Log("SO PHAN TU TRONG LINE LIST: " + linesList.Count);
            Debug.Log("CURRENT STROKE LA:  " + currentStroke);
        }
    }

    private void LoadPositionCoroutine()
    {
       
        currentStroke = 0;
        linesList.Clear();
        foreach (Transform t in transform)
        {
            if (t != null)
            {
                Destroy(t.gameObject);
            }
        }
        
        TextReader tr = new StreamReader(Application.persistentDataPath + "/" + cs.Unicode + ".dat");
        List<string> strList = new List<string>();
        string strA;
        strA = tr.ReadLine();
        while (strA != null)
        {
            Debug.Log(strA);
            strList.Add(strA);
            strA = tr.ReadLine();
        }
        Debug.Log(tr.ReadToEnd());
        char[] lineDelim = new char[] { '#' };
        Debug.Log(lineDelim[0]);
        char[] posDelim = new char[] { ':' };
        string str = tr.ReadToEnd();
        string[] strArr = str.Split(new[] { "#" }, StringSplitOptions.RemoveEmptyEntries);
        if (strArr == null)
        {
            Debug.Log("NULL MAT ROI KO BIET LAM SAO");
        }
        Debug.Log(strArr.Length);
        for (int i = 1; i < strList.Count; i++)
        {
            string[] strPosArr = strList[i].Split(posDelim, StringSplitOptions.RemoveEmptyEntries);
            Debug.Log(strPosArr[0]);
            Vector2 v2 = new Vector2(float.Parse(strPosArr[0]), float.Parse(strPosArr[1]));
            posList.Add(v2);
            prefabGO = Instantiate(prefabCube) as GameObject;
            prefabGO.GetComponent<Renderer>().enabled = false;
            Vector3 cachedScale = prefabGO.transform.localScale;
            prefabGO.transform.SetParent(transform);
            linesList.Add(prefabGO.transform);
            prefabGO.transform.localScale = cachedScale;
            prefabGO.transform.localPosition = new Vector3(-v2.x / (10f / 1.69f), -v2.y / (10f / 1.69f), -0.33f);
            if (1 != i)
            {
                prefabGO.SetActive(false) ;
            }
        }
        tr.Close();
    }

    public void SaveAllPos()
    {
        string str = cs.Unicode + "#";
        //if (!File.Exists(Application.persistentDataPath + "/" + "pos.txt"))
        //{
        //    File.Create(Application.persistentDataPath + "/" + "pos.txt");
        //}
        //else if ()
        //{

        //}
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/" + cs.Unicode + ".dat"); 
        for (int i = 0; i < displayedStroke.Count; i++)
        {
            if (i == displayedStroke.Count - 1)
            {
                str = str + displayedStroke[i].x + ":" + displayedStroke[i].y;
                
            }
            else
            {
                str = str + displayedStroke[i].x + ":" + displayedStroke[i].y + "#";

            }
        }
        //File.WriteAllText(Application.persistentDataPath + "/" + "pos.txt", str);
        sw.WriteLine(str);
        sw.Close();
        sw = null;
        foreach (Transform t in transform)
        {
            if (t != null)
            {
                Destroy(t.gameObject);
            }
        }
    }

    public int NumChild()
    {
        return transform.childCount;
    }

    public void ClearList()
    {
        if (transform.childCount > 0)
        {
            return;
        }
        displayedStroke.Clear();
        
    }

    //Overloaded method, will load the char dynamically
    public void SetCharacter(string chr)
    {
        cs = CharacterDatabase.instance.FindCharacter(chr);
        if (cs == null)
        {
            Debug.Log("Character " + chr + " not found in database!");
            return;
        }
        Character = chr;
        LoadTextureFromFile(cs.Unicode);
    }

    public void LoadTextureFromFile(string unicode)
    {
        if (USE_WEB)
        {
            pathTexture = "http://" + webPath + "Textures/" + cs.Unicode + ".png";
            StartCoroutine(LoadTextureCoroutine());
        }
        else
        {
            DirectoryInfo info = new DirectoryInfo("./");
            pathTexture = "file://" + info.FullName.Replace("\\", "/") + hdPath + "Textures/" + cs.Unicode + ".png";
            StartCoroutine(LoadTextureCoroutine());
        }
    }

    public void SetFalseReverseUpdateTiling()
    {
        
        if (reverseUpdateTilingOne)
        {
            breakReverseUpdateTiling = true;
        }

        if (!updateTilingOne)
        {
            breakUpdateTiling = false;
            breakOriUpdateTiling = true;
            StartCoroutine(UpdateTiling(cachedRow, cachedColumn, cachedTotal));
            updateTilingOne = true;
            Debug.Log("SetFalseReverseUpdateTiling!IF TRUE");
        }
        else
        {
            haltAnimation = false;
            reverseUpdateTilingOne = false;

            Debug.Log("SetFalseReverseUpdateTiling!IF FALSE");
        }
        
    }

    public void SetFalseUpdateTiling()
    {
        if (updateTilingOne)
        {
            breakUpdateTiling = true;
        }

        if (!reverseUpdateTilingOne)
        {
            breakReverseUpdateTiling = false;

            StartCoroutine(ReverseUpdateTiling(cachedRow, cachedColumn, cachedTotal));
            reverseUpdateTilingOne = true;
            Debug.Log("SetFalseUpdateTiling!IF TRUE");
        }
        else
        {
            haltAnimation = false;
            updateTilingOne = false;

            Debug.Log("SetFalseUpdateTiling!IF FALSE");
        }
    }

    IEnumerator LoadTextureCoroutine()
    {
        WWW www = new WWW(pathTexture);
        yield return www;

        textureChar = www.texture;
        PrepareTexture();
        if (CallBackFunction != null)
        {
            EventInfo einfo = new EventInfo();
            einfo.sender = this;
            einfo.eventType = EventType.TextureLoaded;
            CallBackFunction(einfo);
        }
    }

    private void DeleteCube()
    {
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
        displayedStroke.Remove(displayedStroke[displayedStroke.Count - 1]);
    }

    public void PrepareTexture() {
        if (cs == null)
            return;
        chrText = textureChar;
        Columns = cs.Width;
        Rows = cs.Height;
        NumImages = cs.Images;
        Vector2 size = new Vector2(1f / Columns, 1f / Rows);
        GetComponent<Renderer>().material.mainTexture = chrText;
        GetComponent<Renderer>().sharedMaterial.SetTextureScale("_MainTex", size);
        Vector2 offset = Vector2.zero;
        offset.Set(0, (float)(Rows - 1) / (float)Rows);
        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }

    public void ContinueUpdateTiling()
    {
        StartCoroutine(UpdateTiling(cachedRow, cachedColumn, cachedTotal));
    }

    public void SetHaltAnimation()
    {
        haltAnimation = false;
        Debug.Log("CO VAO DAY KHONG");
    }

    public void StartReverseUpdateTiling()
    {
        breakUpdateTiling = true;
    }

    private IEnumerator UpdateTiling()
    {
        float x = 0f;
        float y = 0f;
        Vector2 offset = Vector2.zero;
        
        while (true)
        {
            int total = 0;
            for (int i = Rows - 1; i >= 0; i--) // y
            {



                y = (float)i / (float)Rows;

                for (int j = 0; j <= Columns - 1; j++) // x
                {
                    total++;
                    Debug.Log("SO COT : " + j + " SO HANG: " + i + " TONG SO : " + total);
                    if (total > NumImages)
                        break;
                    x = (float)j / (float)Columns;

                    offset.Set(x, y);
                    if (!displayedStrokeVsValue.ContainsKey(new Vector2(x, y)))
                    {
                        displayedStrokeVsValue.Add(new Vector2(x, y), new Vector3(j, i, total));
                    }
                    CachedTotal = total;
                    CachedColumn = j;
                    CachedRow = i;
                    Debug.Log("CACHED ROW LA : " + CachedRow);
                    GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);


                    haltAnimation = true;
                    //haltAnimation = false;
                    //Invoke("SetHaltAnimation", 10f);
                    if (!haltAnimation)
                    {
                        Debug.Log("DA VAO DAY ROI NHE");
                    }
                    else if (haltAnimation)
                    {
                        Debug.Log("STOP");
                        while (haltAnimation)
                        {
                            if (breakOriUpdateTiling)
                            {
                                breakOriUpdateTiling = false;
                                Debug.Log("TRUOC KHI BREAK NE");
                                yield break;
                            }
                            Debug.Log("NGAY SAU KHI KHI BREAK NE");
                            yield return new WaitForSeconds(1f / FramesPerSecond);
                        }
                        //haltAnimation = false;
                        //yield break;
                    }
                    Debug.Log("NGAY SAU KHI KHI BREAK NE");
                    yield return new WaitForSeconds(1f / FramesPerSecond);
                }
            }

            if (!LoopAnimation)
            {
                if (CallBackFunction != null)
                {
                    EventInfo einfo = new EventInfo();
                    einfo.sender = this;
                    einfo.eventType = EventType.FinishAnimation;
                    CallBackFunction(einfo);
                }
                started = false;
                yield break;
            }
            yield return new WaitForSeconds(waitOnEnd);
        }
    }

    private IEnumerator UpdateTiling(int cachedRow, int cachedColumn, int cachedTotal)
    {
        float x = 0f;
        float y = 0f;
        Vector2 offset = Vector2.zero;
        //reverseUpdateTilingOne = false;
        bool isFirstTime = false;
        while (true)
        {
            int total = cachedTotal - 1;
            for (int i = cachedRow; i >= 0; i--) // y
            {


                
                y = (float)i / (float)Rows;
                if (!isFirstTime)
                {
                    for (int j = cachedColumn; j <= Columns - 1; j++) // x
                    {
                        total++;
                        Debug.Log("SO COT : " + j + " SO HANG: " + i + " TONG SO : " + total);
                        if (total > NumImages)
                            break;
                        x = (float)j / (float)Columns;

                        offset.Set(x, y);
                        if (!displayedStrokeVsValue.ContainsKey(new Vector2(x, y)))
                        {
                            displayedStrokeVsValue.Add(new Vector2(x, y), new Vector3(j, i, total));
                        }
                        CachedTotal = total;
                        CachedColumn = j;
                        CachedRow = i;
                        Debug.Log("CACHED ROW TRONG OVERLOADED LA : " + CachedRow);
                        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);


                        haltAnimation = true;
                        //haltAnimation = false;
                        //Invoke("SetHaltAnimation", 10f);
                        if (!haltAnimation)
                        {
                            Debug.Log("DA VAO DAY ROI NHE");
                        }
                        else if (haltAnimation)
                        {
                            Debug.Log("STOP");
                            while (haltAnimation)
                            {
                                if (breakUpdateTiling)
                                {
                                    breakUpdateTiling = false;
                                    yield break;
                                }
                                yield return new WaitForSeconds(1f / FramesPerSecond);
                            }
                            //haltAnimation = false;
                            //yield break;
                        }
                        yield return new WaitForSeconds(1f / FramesPerSecond);
                    }
                    isFirstTime = true;
                }
                else
                {
                    for (int j = 0; j <= Columns - 1; j++) // x
                    {
                        total++;
                        Debug.Log("SO COT : " + j + " SO HANG: " + i + " TONG SO : " + total);
                        if (total > NumImages)
                            break;
                        x = (float)j / (float)Columns;

                        offset.Set(x, y);
                        if (!displayedStrokeVsValue.ContainsKey(new Vector2(x, y)))
                        {
                            displayedStrokeVsValue.Add(new Vector2(x, y), new Vector3(j, i, total));
                        }
                        CachedTotal = total;
                        CachedColumn = j;
                        CachedRow = i;
                        Debug.Log("CACHED ROW TRONG OVERLOADED LA : " + CachedRow);
                        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);


                        haltAnimation = true;
                        //haltAnimation = false;
                        //Invoke("SetHaltAnimation", 10f);
                        if (!haltAnimation)
                        {
                            Debug.Log("DA VAO DAY ROI NHE");
                        }
                        else if (haltAnimation)
                        {
                            Debug.Log("STOP");
                            while (haltAnimation)
                            {
                                if (breakUpdateTiling)
                                {
                                    breakUpdateTiling = false;
                                    yield break;
                                }
                                yield return new WaitForSeconds(1f / FramesPerSecond);
                            }
                            //haltAnimation = false;
                            //yield break;
                        }
                        yield return new WaitForSeconds(1f / FramesPerSecond);
                    }
                }
                
            }

            if (!LoopAnimation)
            {
                if (CallBackFunction != null)
                {
                    EventInfo einfo = new EventInfo();
                    einfo.sender = this;
                    einfo.eventType = EventType.FinishAnimation;
                    CallBackFunction(einfo);
                }
                started = false;
                yield break;
            }
            yield return new WaitForSeconds(waitOnEnd);
        }
    }

    private IEnumerator ReverseUpdateTiling(int cachedRow, int cachedColumn, int cachedTotal)
    {
        float x = 0f;
        float y = 0f;
        Vector2 offset = Vector2.zero;
        //updateTilingOne = false;
        bool isFirstTime = false;
        while (true)
        {
            int total = cachedTotal;
            for (int i = cachedRow; i <= Rows - 1; i++) // y
            {



                y = (float)i / (float)Rows;
                if (!isFirstTime)
                {
                    for (int j = cachedColumn - 1; j >= 0; j--) // x
                    {
                        total--;
                        Debug.Log("SO COT : " + j + " SO HANG: " + i + " TONG SO : " + total);
                        if (total < 0)
                            break;
                        x = (float)j / (float)Columns;

                        offset.Set(x, y);
                        if (!displayedStrokeVsValue.ContainsKey(new Vector2(x, y)))
                        {
                            displayedStrokeVsValue.Add(new Vector2(x, y), new Vector3(j, i, total));
                        }
                        CachedTotal = total;
                        CachedColumn = j;
                        CachedRow = i;
                        Debug.Log("CACHED ROW TRONG OVERLOADED REVERSE LA : " + CachedRow);
                        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);


                        haltAnimation = true;
                        //haltAnimation = false;
                        //Invoke("SetHaltAnimation", 10f);
                        if (!haltAnimation)
                        {
                            Debug.Log("DA VAO DAY ROI NHE");
                        }
                        else if (haltAnimation)
                        {
                            Debug.Log("STOP");
                            while (haltAnimation)
                            {

                                if (breakReverseUpdateTiling)
                                {
                                    breakReverseUpdateTiling = false;
                                    yield break;
                                }
                                yield return new WaitForSeconds(1f / FramesPerSecond);
                            }
                            //haltAnimation = false;
                            //yield break;
                        }
                        yield return new WaitForSeconds(1f / FramesPerSecond);
                    }
                    isFirstTime = true;
                }
                else
                {
                    for (int j = Columns - 1; j >= 0; j--) // x
                    {
                        total--;
                        Debug.Log("SO COT : " + j + " SO HANG: " + i + " TONG SO : " + total);
                        if (total > NumImages)
                            break;
                        x = (float)j / (float)Columns;

                        offset.Set(x, y);
                        if (!displayedStrokeVsValue.ContainsKey(new Vector2(x, y)))
                        {
                            displayedStrokeVsValue.Add(new Vector2(x, y), new Vector3(j, i, total));
                        }
                        CachedTotal = total;
                        CachedColumn = j;
                        CachedRow = i;
                        Debug.Log("CACHED ROW TRONG OVERLOADED REVERSE LA : " + CachedRow);
                        GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);


                        haltAnimation = true;
                        //haltAnimation = false;
                        //Invoke("SetHaltAnimation", 10f);
                        if (!haltAnimation)
                        {
                            Debug.Log("DA VAO DAY ROI NHE");
                        }
                        else if (haltAnimation)
                        {
                            Debug.Log("STOP");
                            while (haltAnimation)
                            {

                                if (breakReverseUpdateTiling)
                                {
                                    breakReverseUpdateTiling = false;
                                    yield break;
                                }
                                yield return new WaitForSeconds(1f / FramesPerSecond);
                            }
                            //haltAnimation = false;
                            //yield break;
                        }
                        yield return new WaitForSeconds(1f / FramesPerSecond);
                    }
                }

            }

            if (!LoopAnimation)
            {
                if (CallBackFunction != null)
                {
                    EventInfo einfo = new EventInfo();
                    einfo.sender = this;
                    einfo.eventType = EventType.FinishAnimation;
                    CallBackFunction(einfo);
                }
                started = false;
                yield break;
            }
            yield return new WaitForSeconds(waitOnEnd);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetFalseUpdateTiling();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetFalseReverseUpdateTiling();
        }
        else if (Input.GetKeyDown(KeyCode.Delete))
        {
            DeleteCube();
        }
    }

}
