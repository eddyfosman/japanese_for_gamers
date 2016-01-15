using UnityEngine;
using System.Collections;

[System.Serializable]
public class Skil {
	public string skillName = "";
	public Texture2D icon;
	public Texture2D iconDown;
	public string description = "";
	public Transform skillPrefab;
	public AnimationClip skillAnimation;
	public int manaCost = 10;
}

public class SkillDataC : MonoBehaviour {
	public Skil[] skill = new Skil[3];
}
