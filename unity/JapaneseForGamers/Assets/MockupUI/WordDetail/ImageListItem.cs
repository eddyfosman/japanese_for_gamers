using UnityEngine;
using System.Collections;
using EnhancedUI;
using UnityEngine.UI;

public class ImageListItem : ListItemBase {

	public UnityEngine.UI.Image imageMonster;

	public override void SetData(object objectData){
		ImageData data = (objectData as ImageData);
		Sprite sprite = Resources.Load ("Sprites/" + data.monsterID,  typeof(Sprite)) as Sprite;

		imageMonster.sprite = sprite;

	}
}
