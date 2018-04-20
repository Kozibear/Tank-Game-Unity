using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSink : MonoBehaviour {

	public float currentTime;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad >= 4) {
			transform.position += new Vector3 (0, 0.0013f, 0);
		} 
		else {
		}
	}
}