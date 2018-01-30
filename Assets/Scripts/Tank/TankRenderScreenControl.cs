using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRenderScreenControl : MonoBehaviour {

	public GameObject Screen1GameObject;
	public GameObject Screen2GameObject;
	public GameObject Screen3GameObject;
	public GameObject Screen4GameObject;
	public GameObject Screen5GameObject;

	public float lowestNumber;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void DeleteOldestScreen (){

		lowestNumber = Mathf.Min (Screen1GameObject.GetComponent<screenScript> ().screenAge, Screen2GameObject.GetComponent<screenScript> ().screenAge, Screen3GameObject.GetComponent<screenScript> ().screenAge, Screen4GameObject.GetComponent<screenScript> ().screenAge, Screen5GameObject.GetComponent<screenScript> ().screenAge);

		if (lowestNumber == Screen1GameObject.GetComponent<screenScript> ().screenAge) {
			Screen1GameObject.GetComponent<screenScript> ().explode = true;
			Screen1GameObject = null;
		}
		else if (lowestNumber == Screen2GameObject.GetComponent<screenScript> ().screenAge) {
			Screen2GameObject.GetComponent<screenScript> ().explode = true;
			Screen2GameObject = null;
		}
		else if (lowestNumber == Screen3GameObject.GetComponent<screenScript> ().screenAge) {
			Screen3GameObject.GetComponent<screenScript> ().explode = true;
			Screen3GameObject = null;
		}
		else if (lowestNumber == Screen4GameObject.GetComponent<screenScript> ().screenAge) {
			Screen4GameObject.GetComponent<screenScript> ().explode = true;
			Screen4GameObject = null;
		}
		else if (lowestNumber == Screen5GameObject.GetComponent<screenScript> ().screenAge) {
			Screen5GameObject.GetComponent<screenScript> ().explode = true;
			Screen5GameObject = null;
		}
	}

	public void ResetScreens () {
		if (Screen1GameObject != null) {
			Screen1GameObject.GetComponent<screenScript> ().Endscene();
			Screen1GameObject = null;
		}

		if (Screen2GameObject != null) {
			Screen2GameObject.GetComponent<screenScript> ().Endscene();
			Screen2GameObject = null;
		}

		if (Screen3GameObject != null) {
			Screen3GameObject.GetComponent<screenScript> ().Endscene();
			Screen3GameObject = null;
		}

		if (Screen4GameObject != null) {
			Screen4GameObject.GetComponent<screenScript> ().Endscene();
			Screen4GameObject = null;
		}

		if (Screen5GameObject != null) {
			Screen5GameObject.GetComponent<screenScript> ().Endscene();
			Screen5GameObject = null;
		}
	}
}
