using UnityEngine;
using System.Collections;
using EnhancedUI;

public class WordListItem : ListItemBase {

	public UnityEngine.UI.Text wordIdText;
	public UnityEngine.UI.Text wordMeaningText;
	public UnityEngine.UI.Text wordReadingText;
	public UnityEngine.UI.Text wordWritingText;
	public UnityEngine.UI.Text onSoundPath;
	public UnityEngine.UI.Text kunSoundPath;

	public override void SetData(object objectData){
		WordData data = (objectData as WordData);

		wordIdText.text = data.wordId;
		wordMeaningText.text = data.wordMeaning;
		wordReadingText.text = data.wordReading;
		wordWritingText.text = data.wordWriting;
		onSoundPath.text = data.onSound;
		kunSoundPath.text = data.kunSound;

	}

	public override void ItemSelected(){
		WordData passedData = new WordData ();
		passedData.IDSelected = wordIdText.text;
		passedData.MeaningText = wordMeaningText.text;
		passedData.ReadingText = wordReadingText.text;
		passedData.WritingText = wordWritingText.text;
		passedData.OnSoundPath = onSoundPath.text;
		passedData.KunSoundPath = kunSoundPath.text;
		Debug.Log (onSoundPath.text);
		Application.LoadLevel (1);
	}


}
