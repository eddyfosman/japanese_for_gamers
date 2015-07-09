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
		wordMeaing.text = data.wordMeaning;
		wordReading.text = data.wordReading;
	}

	public override void ItemSelected ()
	{
		SceneTransitionData sceneData = new SceneTransitionData ();
		sceneData.IsFromWordListScene = false;

		WordData selectedWord = new WordData ();
		selectedWord.IDSelected = wordID.text;
		selectedWord.MeaningText = wordMeaing.text;
		selectedWord.ReadingText = wordReading.text;
		selectedWord.WritingText = wordText.text;

		Application.LoadLevel (1);
	}
}
