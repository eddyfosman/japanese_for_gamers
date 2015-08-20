using UnityEngine;
using System.Collections;

public delegate void SelectedChangedDelegate(bool val);

public class InventoryData {

	public string monsterName;

	public SelectedChangedDelegate selectedChanged;

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

}
