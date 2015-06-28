using UnityEngine;
using System.Collections;

public class Gizmos2 : MonoBehaviour {

	public float gizmoSize = 0.25f;
	public Color gizmoColor = Color.yellow;

	void OnDrawGizmos(){

		Gizmos.color = gizmoColor;
		Gizmos.DrawWireSphere (transform.position, gizmoSize);

	}
}
