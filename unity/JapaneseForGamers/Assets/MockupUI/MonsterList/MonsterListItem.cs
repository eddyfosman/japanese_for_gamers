using UnityEngine;
using System.Collections;
using EnhancedUI;

public class MonsterListItem : ListItemBase {

	public UnityEngine.UI.Text monsterID;
	public UnityEngine.UI.Text monsterName;
	public UnityEngine.UI.Image monsterImage;

	public override void SetData(object objectData){
		MonsterData data = (objectData as MonsterData);
		monsterID.text = data.monsterID;
		monsterName.text = data.monsterName;
//		monsterImage.sprite = Resources.Load ("MockupUI/Resources/Sprites/" + data.monsterName ) as Sprite;
		monsterImage.sprite = Resources.Load ("Sprites/" + data.monsterName ,typeof (Sprite) ) as Sprite;

	
	}
	
	public override void ItemSelected(){
		MonsterData data = new MonsterData ();
		data.SelectedID = monsterID.text;
		data.SelectedMonsterName = monsterName.text;
		Debug.Log (monsterID.text);
		Debug.Log (monsterName.text);
		Application.LoadLevel (3);
	}
}
