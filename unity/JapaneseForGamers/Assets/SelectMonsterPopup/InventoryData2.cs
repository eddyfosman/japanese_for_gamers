using UnityEngine;
using System.Collections;

//public delegate void SelectedChangedDelegate(bool val);

public class InventoryData2 {
	public string id;
	public string monsterName;
	public string id2;
	public string monsterName2;

	public SelectedChangedDelegate selectedChanged;
	public SelectedChangedDelegate selectedChanged2;

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

	private bool _selected2;
	public bool Selected2{
		
		get{return _selected2;}
		set{
			if(_selected2 != value){
				_selected2 = value;
				if(selectedChanged2 != null){
					selectedChanged2(_selected2);
				}
			}
		}
		
	}

}
