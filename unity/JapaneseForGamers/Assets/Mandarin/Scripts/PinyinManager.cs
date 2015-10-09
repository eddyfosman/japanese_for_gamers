using UnityEngine;
using System.Collections;
using System.IO;

public class PinyinManager : MonoBehaviour {

    //If you are compiling for Web set USE_WEB to true and put the apropriate web path
    //If you are compiling for descktop or mobile ser web to false
    //Remember to copy all textures to the Textures directory and pinyin sounds to the PinyinSounds directory
    //These two directories must be located in the correspondent webPath or hdPath 
    //The hdPath must start where the App is located
    public static readonly bool USE_WEB = false;
    public static readonly string webPath = "put_you_server/and_path_here/";
    public static readonly string hdPath = "ChineseCharacters/";
    string pathTexture;

    AudioSource source;

    public void PlayPinyin(string pinyin, AudioSource src)
    {
        source = src;
        if (USE_WEB)
        {
            pathTexture = "http://" + webPath + "PinyinSounds/" + pinyin + ".ogg";
            StartCoroutine(LoadPinyinCoroutine());
        }
        else
        {
            DirectoryInfo info = new DirectoryInfo("./");
            pathTexture = "file://" + info.FullName.Replace("\\", "/") + hdPath + "PinyinSounds/" + pinyin + ".ogg";
            StartCoroutine(LoadPinyinCoroutine());
        }
    }

    IEnumerator LoadPinyinCoroutine()
    {
        WWW www = new WWW(pathTexture);
        yield return www;

        source.clip = www.GetAudioClip(false, false);
        source.Play();
    }
}
