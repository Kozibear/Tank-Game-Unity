using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackBGFadeIn : MonoBehaviour {

	public Image blackBackground;

	public Color blackBackgroundColor;

	public bool darkenScreen;

	void Awake () {
		blackBackground.gameObject.SetActive (true);

		blackBackgroundColor = new Color (0, 0, 0, 1);

		darkenScreen = false;
	}

	// Use this for initialization
	void Start () {
		blackBackground.gameObject.SetActive (true);

		blackBackgroundColor = new Color (0, 0, 0, 1);

		darkenScreen = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//for the black background fading out 
		if (!darkenScreen && blackBackgroundColor.a > 0) {
			blackBackground.color = blackBackgroundColor;
			blackBackgroundColor.a -= 0.02f;
		}
		if (!darkenScreen && blackBackgroundColor.a <= 0) {
			blackBackgroundColor.a = 0;
			blackBackground.gameObject.SetActive (false);
		}
	}
}
