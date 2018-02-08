using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentWords : MonoBehaviour {

	public Text text;

	public float textSelection;

	// Use this for initialization
	void Start () {
		textSelection = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (textSelection == 1) {
			text.text = "ever was";
		}
		if (textSelection == 2) {
			text.text = "train them";
		}
		if (textSelection == 3) {
			text.text = "the land";
		}
		if (textSelection == 4) {
			text.text = "the power";
		}
		if (textSelection == 5) {
			text.text = "and me";
		}
		if (textSelection == 6) {
			text.text = "my destiny";
		}
		if (textSelection == 7) {
			text.text = "best friend";
		}
		if (textSelection == 8) {
			text.text = "so true";
		}
		if (textSelection == 9) {
			text.text = "pull us";
		}
		if (textSelection == 10) {
			text.text = "teach you";
		}
	}

	public void numberSelector ()
	{
		textSelection = Random.Range (0, 11);
	}
}
