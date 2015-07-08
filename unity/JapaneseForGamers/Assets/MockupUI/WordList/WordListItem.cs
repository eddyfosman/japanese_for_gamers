using UnityEngine;
using System.Collections;
using EnhancedUI;

public class WordListItem : ListItemBase {

	public UnityEngine.UI.Text wordIdText;
	public UnityEngine.UI.Text wordMeaningText;
	public UnityEngine.UI.Text wordReadingText;
	public UnityEngine.UI.Text wordWritingText;

	public override void SetData(object objectData){
		WordData data = (objectData as WordData);

		wordIdText.text = data.wordId;
		wordMeaningText.text = data.wordMeaning;
		wordReadingText.text = data.wordReading;
		wordWritingText.text = data.wordWriting;
	}

	public override void ItemSelected(){
		WordData passedData = new WordData ();
		passedData.IDSelected = wordIdText.text;
		passedData.MeaningText = wordMeaningText.text;
		passedData.ReadingText = wordReadingText.text;
		passedData.WritingText = wordWritingText.text;
		Debug.Log (wordWritingText);
		Application.LoadLevel (1);
	}


}
