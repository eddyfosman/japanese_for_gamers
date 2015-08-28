using UnityEngine;
using System.Collections;

public delegate void CreatedChangedDelegate(bool val);

public class TeamData{

	public string monsterName1;
	public string monsterName2;
	public string monsterName3;
	public string monsterName4;
	
	public SelectedChangedDelegate selectedChanged;
	public CreatedChangedDelegate createdChanged;
	
	private bool _selected;
	public bool Selected{
		
		get{return _selected;}
		set{
			if(_selected != value){
				_selected = value;
				if(selectedChanged != null){
					selectedChanged(_selected);
				}
			}
		}
		
	}

	private bool _created;
	public bool Created{
		get{return _created;}
		set{
			if(_created != value){
				_created = value;
				if(createdChanged != null){
					createdChanged(_created);
				}
			}

		}
	}

}
