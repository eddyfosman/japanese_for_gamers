using UnityEngine;
using System.Collections;
using EnhancedUI;

public class TextListItem : ListItemBase {

	public UnityEngine.UI.Text wordText;
	public UnityEngine.UI.Text wordID;
	public UnityEngine.UI.Text wordMeaing;
	public UnityEngine.UI.Text wordReading;

	public override void SetData (object objectData)
	{
		TextData data = (objectData as TextData);
		wordText.text = data.wordWriting;
		wordID.text = data.wordID;
	}

	public override void ItemSelected ()
	{

		Application.LoadLevel (1);
	}
}
