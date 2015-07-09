using UnityEngine;
using System.Collections;
using EnhancedUI;
using UnityEngine.UI;

public class ImageListItem : ListItemBase {

	public UnityEngine.UI.Image imageMonster;
	public UnityEngine.UI.Text monsterName;
	public UnityEngine.UI.Text monsterID;

	public override void SetData(object objectData){
		ImageData data = (objectData as ImageData);
		monsterName.text = data.monsterName;
		monsterID.text = data.monsterID;
		Sprite sprite = Resources.Load ("Sprites/" + data.monsterName,  typeof(Sprite)) as Sprite;

		imageMonster.sprite = sprite;

	}

	public override void ItemSelected ()
	{
		SceneTransitionData sceneData = new SceneTransitionData ();
		sceneData.IsFromMonsterListScene = false;
		MonsterData selectedMonster = new MonsterData ();
		selectedMonster.SelectedID = monsterID.text;
		selectedMonster.SelectedMonsterName = monsterName.text;
		Application.LoadLevel (3);
	}

}
