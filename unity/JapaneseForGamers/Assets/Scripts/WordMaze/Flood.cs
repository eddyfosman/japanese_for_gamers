using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class PostitionBean1{

//	int idX;
//	public int IdX{ set; get;}
//
//	int idY;
//	public int IdY{ set; get;}

	float posX;
	public float PosX{ set; get;}

	float posY;
	public float PosY{ set; get;}

//	float posZ;
//	public float PosZ{ set; get;}



}

public class Flood : MonoBehaviour {
	public Camera camera;
	public GameObject Cube;
	GameObject loveGO;
	GameObject cube;
	public bool isCollided = false;
	bool isDestroyed = false;
	bool isFirst = false;
	bool firstCube = true;
	public bool isWordMaze2 = false;
	PostitionBean1 posObject;


	List<Vector3> listPos2 = new List<Vector3>();

	IEnumerator WaitForChecking(){
		while(!cube.GetComponent<CheckCollision>().getFlood){
			Debug.Log("VAO DAY");
			yield return null;

		}
	}

	int N = 10;
	int[][] init;
	List<Vector2> check = new List<Vector2>();
	List<GameObject> listCube = new List<GameObject>();
	List<Vector3> listPos = new List<Vector3>();

	void GetData(){
		TextAsset jsonFile = Resources.Load ("wordmazedata") as TextAsset;
		JsonData jsonPos = JsonMapper.ToObject (jsonFile.text);
		for(int i = 0;i < jsonPos["pos"].Count; i ++){
			posObject = new PostitionBean1();
			posObject.PosX = float.Parse(jsonPos["pos"][i]["posx"].ToString());
			posObject.PosY = float.Parse(jsonPos["pos"][i]["posy"].ToString());
			Vector3 posv3 = new Vector3(posObject.PosX, posObject.PosY, 0f);
			listPos2.Add(posv3);
		}
	}


	void CheckFlood(int i, int j){

		if(i < 0 || i >= N){
			return;
		}
		if(j < 0 || j >= N){
			return;
		}



		for(int e = 0; e < check.Count ; e++){
			if(check[e].x == (float)i && check[e].y == (float)j){
				return;
			}

		}
		check.Add (new Vector2((float)i,(float)j));

		cube = listCube[i*N + j];


		if(cube.GetComponent<Cube>().isCollided){

			cube.GetComponent<Renderer>().enabled = false;
//			Destroy(cube);
			Debug.Log("XOA CUBE");

			return;
			


		}


			
				cube.GetComponent<Renderer>().enabled = true;
				listPos.Add(cube.transform.position);
//				Destroy(cube);
			
			
			Debug.Log("TRUE CUBE");
			firstCube = false;
			CheckFlood(i-1,j);
			CheckFlood(i,j+1);
			CheckFlood(i+1,j);
			CheckFlood(i,j-1);


	}

	GameObject AddCube(int i, int j){
//		float x =	-4f +	j*0.8f;
//		float y =  2.4f -	i*0.8f;

		float x =	-3.13f +	j*3.15f;
		float y =  	11f	 -	i*2.2f;

//		float x =	-7f +	j*2.8f;
//		float y =  21.3f -	i*2.8f;

		GameObject newGO = Instantiate (Cube) as GameObject;
		newGO.GetComponent<Renderer>().enabled = false;
		newGO.transform.position = new Vector3(x, y, 0f);
		Vector3 temp = newGO.transform.localScale;
		newGO.transform.localScale = new Vector3(temp.x * 7f, temp.y * 7f, temp.z * 7f);
		if(!isWordMaze2){
			newGO.AddComponent<BoxCollider>();
			newGO.AddComponent<Rigidbody>();
			
			newGO.GetComponent<Rigidbody>().useGravity = false;
			newGO.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
		}
		else{
			newGO.AddComponent<BoxCollider2D>();
			newGO.AddComponent<Rigidbody2D>();
			
			newGO.GetComponent<Rigidbody2D>().gravityScale = 0f;

		}

		newGO.AddComponent<Cube>();
		newGO.AddComponent<CheckCollision>();

		return newGO;
	}

	public void CreateFood(){
		Debug.Log("VAO DAY 6");
		GameObject newGO = Instantiate (Cube) as GameObject;
		Debug.Log("VAO DAY 7");
		//dong nay de dung cho AI flood
//		newGO.transform.position = listPos[Random.Range(0, listPos.Count)];
		newGO.transform.position = listPos2[Random.Range(0, listPos2.Count)];
		Debug.Log("VAO DAY 8");
		Vector3 temp = newGO.transform.localScale;
		Debug.Log("VAO DAY 9");
		newGO.transform.localScale = new Vector3(temp.x * 7f, temp.y * 7f, temp.z * 7f);
		if(!isWordMaze2){
			newGO.AddComponent<BoxCollider>();
			newGO.GetComponent<BoxCollider>().isTrigger = true;
		}
		else{
			newGO.AddComponent<BoxCollider2D>();
			newGO.GetComponent<BoxCollider2D>().isTrigger = true;
		}
		newGO.AddComponent<GetF>();
	}

	void StartCheckFlood(){
		CheckFlood(0, 0);
		for(int d = 0; d < listCube.Count; d++){
			Destroy(listCube[d]);
		}


		if(!isWordMaze2){
			foreach(Transform transform in loveGO.transform){
				
				
				transform.gameObject.GetComponent<MeshCollider>().isTrigger = false;
				
			}
		}
		else{
			foreach(Transform transform in loveGO.transform){
				
				transform.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
				transform.gameObject.GetComponent<EdgeCollider2D>().isTrigger = false;
				
			}
		}

		CreateFood ();
	}

	// Use this for initialization
	void Start () {
		GetData ();
		if(!isWordMaze2){
			loveGO = GameObject.Find ("love");
			foreach(Transform transform in loveGO.transform){
				
				transform.gameObject.AddComponent<MeshCollider>();
				transform.gameObject.GetComponent<MeshCollider>().isTrigger = true;
				
			}
		}
		else{
			loveGO = GameObject.Find("AutoTileSet");
			foreach(Transform transform in loveGO.transform){

				transform.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
				transform.gameObject.GetComponent<EdgeCollider2D>().isTrigger = true;

			}
		}


		for(int a = 0; a < N; a++){
			for(int b = 0; b < N; b++){
				GameObject go = AddCube(a, b);
				listCube.Add(go);
			}
		}
		
				

	}
	
	// Update is called once per frame
	void Update () {
		if(!isFirst){
			isFirst = true;

			Invoke("StartCheckFlood", 5f);
		}
	}
}
