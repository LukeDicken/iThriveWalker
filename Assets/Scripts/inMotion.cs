using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inMotion : MonoBehaviour {

	private bool motion;
	private Vector3 oldPos;

	// Use this for initialization
	void Start () {
		motion = false;
		oldPos = this.gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = this.gameObject.transform.position;
		if (position == oldPos) {
			motion = false;
		} else {
			motion = true;
		}
	}
}
