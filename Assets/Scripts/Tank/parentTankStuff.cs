using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentTankStuff : MonoBehaviour {

	public GameObject opposingTank;

	public bool isBlueTank;
	public bool isRedTank;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//gameobject.find an object called redtank or bluetank, and face it
		if (isBlueTank) {
			opposingTank = GameObject.Find ("RedTank");
		}

		if (isRedTank) {
			opposingTank = GameObject.Find ("BlueTank");
		}

		transform.LookAt (opposingTank.transform);
	}
}
