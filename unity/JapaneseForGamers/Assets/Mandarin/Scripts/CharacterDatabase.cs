using UnityEngine;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;


//Use this class to select the unicode file name dor a specific chinese character
//The characters textures are stored in a file with the name of the correspondent unicode 


//Persistent singleton character database
public class CharacterDatabase : MonoBehaviour {

    private static CharacterDatabase _instance;

    public static CharacterDatabase instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CharacterDatabase>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    public class Character
    {
        public string Unicode, Ideogram, pinyin, english;
        public int Images, Strokes, Width, Height, Type;
    }

    public List<Character> CharactersDB = new List<Character>();

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }

        LoadDatabase();
	}

    //Load the character database into a sorted list of characters
    void LoadDatabase()
    {
        char[] archDelim = new char[] { '\r', '\n' };
        char[] lineDelim = new char[] { '#' };

        TextAsset txt = (TextAsset)Resources.Load("Mandarin/characters", typeof(TextAsset));
        string[] records = txt.text.Split(archDelim, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < records.Length; i++)
        {
            string[] fields = records[i].Split(lineDelim, StringSplitOptions.RemoveEmptyEntries);
            Character record = new Character();
            record.Unicode = fields[0];
            Int32.TryParse(fields[1], out record.Images);
            Int32.TryParse(fields[2], out record.Strokes);
            Int32.TryParse(fields[3], out record.Width);
            Int32.TryParse(fields[4], out record.Height);
            Int32.TryParse(fields[5], out record.Type);
            record.Ideogram = fields[6];
            record.pinyin = fields[7];
            record.english = fields[8];
            CharactersDB.Add(record);
        }
    }

    public Character FindUnicode(string Unicode)
    {
        return CharactersDB.Find(x => x.Unicode == Unicode);
        
    }

    public object[] FindUnicode2(string Unicode)
    {
        object[] obj = new object[2];
        obj[0] = CharactersDB.Find(x => x.Unicode == Unicode);
        if (obj[0] == null)
        {
            return null;
        }
        obj[1] = CharactersDB.FindIndex(x => x.Unicode == Unicode);
        return obj;
    }

    public Character FindCharacter(string Ideogram)
    {
        return CharactersDB.Find(x => x.Ideogram == Ideogram);
    }

    public Character GetChatAtPos(int numChar)
    {
        if(numChar>=0 && numChar<CharactersDB.Count)
         return CharactersDB[numChar];
        return null;
    }
}
