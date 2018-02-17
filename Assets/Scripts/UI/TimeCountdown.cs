using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountdown : MonoBehaviour {

	public Text timeText;

	public float currentTime;
	public float recordedTime;

	// Use this for initialization
	void Start () {
		recordedTime = currentTime;
	}
	
	// Update is called once per frame
	void Update () {
		timeText.text = "" + currentTime;
	}

	public IEnumerator TimeLoop()
	{
		//NEED TO USE WHILE!!! OTHERWHILE IT "LAYERS" ON MULTIPLE COROUTINES THE SECOND TIME!
		while (currentTime != 0) { 
			yield return new WaitForSeconds (1);
			currentTime -= 1;
		}

		yield return null; //this part needs to be free!
	}

	public void resetNumber()
	{
		recordedTime -= 5;
		currentTime = recordedTime;
	}
}
