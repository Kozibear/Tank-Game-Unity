using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class maturity : MonoBehaviour {

	public GameObject ageText;

	public Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (ageText.GetComponent<tankAge> ().age > 20) {
			text.text = "Sexually Mature";
		} 
		else {
			text.text = "Immature";
		}
	}
}
