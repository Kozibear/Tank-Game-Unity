using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCountdown : MonoBehaviour {

	public Text timeText;

	public float currentTime;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		timeText.text = "" + currentTime;
	}

	public IEnumerator TimeLoop()
	{
		yield return new WaitForSeconds (1);
		currentTime -= 1;

		if (currentTime != 0) {
			StartCoroutine (TimeLoop ());
		} 
		else {
			yield return null;
		}

	}
}
