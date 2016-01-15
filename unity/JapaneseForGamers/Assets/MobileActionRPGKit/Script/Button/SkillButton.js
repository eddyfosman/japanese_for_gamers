#pragma strict
var skillID : int = 0;
var player : GameObject;
var posX : float = 20.0f;
var posY : float = 20.0f;
var size : float = 120.0f;

// code them vao

var drawBoard : GameObject;
private var finishedDraw : boolean = false;

//

var downTexture : Texture2D;
@HideInInspector
var originalTexture : Texture;
private var onMobile : boolean = false;

function Start () {

    if (!drawBoard) 
    {
        Debug.Log("DANG TIM KIEM DRAWBOARD");
        drawBoard = GameObject.FindWithTag("DrawBoard");
    }
    if (!drawBoard)
    {
        Debug.Log("VAN DANG LA NULL DAY");
    }
    //drawBoard.SetActive(false);
    drawBoard.transform.localPosition = Vector3(20, 0, 1.78);
	if(!player){
		player = GameObject.FindWithTag("Player");
	}
	DontDestroyOnLoad (transform.gameObject);
	originalTexture = GetComponent.<GUITexture>().texture;
	GetComponent.<GUITexture>().pixelInset = Rect (Screen.width -size - posX, -Screen.height +posY, size, size);
	this.transform.parent = null;
	this.transform.position = Vector3(0,1,0);
	CheckPlatform();
}

function CheckPlatform(){
	if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer){
		onMobile = true;
	}else{
		onMobile = false;
	}
					
}

function Update () {
	if(!player){
		Destroy(gameObject);
	}
	
	var count : int = Input.touchCount;
       
    for(var i: int = 0;i < count; i++){
    	var touch : Touch = Input.GetTouch(i);
    	//if(guiTexture.HitTest(touch.position) && touch.phase == TouchPhase.Began){
    	if(GetComponent.<GUITexture>().HitTest(touch.position)){
        	Activate();
     	}
    }
}

function OnMouseDown () {
	if(onMobile){
		return;
	}
	//Push Attack Button
	Activate();
		
}

//code goc
//function Activate(){
//	//Push Attack Button
//	player.GetComponent(AttackTrigger).TriggerSkill(skillID);
//	if(downTexture){
//		GetComponent.<GUITexture>().texture = downTexture;
//		yield WaitForSeconds(0.1);
//		GetComponent.<GUITexture>().texture = originalTexture;
//	}

//}

function Activate(){
    //Push Attack Button
    //drawBoard.SetActive(true);
    if (drawBoard != null)
    {
        drawBoard.transform.position = Vector3(0, 0, 1.78);
    }
    
    while (!finishedDraw)
    {
        yield null;        
    }
    player.GetComponent(AttackTrigger).TriggerSkill(skillID);
    if(downTexture){
        GetComponent.<GUITexture>().texture = downTexture;
        yield WaitForSeconds(0.1);
        GetComponent.<GUITexture>().texture = originalTexture;
    }

}


