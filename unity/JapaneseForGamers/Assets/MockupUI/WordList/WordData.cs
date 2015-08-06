

public class WordData {

	static string idSelected;
	public string IDSelected {
		get{return idSelected;}
		set{idSelected = value;}
	}

	static string writingText;
	public string WritingText{
		get{return writingText;}
		set{writingText = value;}
	}

	static string readingText;
	public string ReadingText {
		get{return readingText;}
		set{readingText = value;}
	}

	static string meaningText;
	public string MeaningText {
		get{return meaningText;}
		set{meaningText = value;}
	}

	static string onSoundPath;
	public string OnSoundPath{
		get{
			return onSoundPath;
		}
		set{
			onSoundPath = value;
		}
	}

	static string kunSoundPath;
	public string KunSoundPath{
		get{
			return kunSoundPath;
		}
		set{
			kunSoundPath = value;
		}
	}

	public string wordId;
	public string wordWriting;
	public string wordReading;
	public string wordMeaning;
	public string onSound;
	public string kunSound;
	
}

