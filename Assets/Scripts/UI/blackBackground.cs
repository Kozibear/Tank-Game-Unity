using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blackBackground : MonoBehaviour {

	public Image blackBackground1;

	public Color blackBackgroundColor;

	// Use this for initialization
	void Start () {
		blackBackground1.gameObject.SetActive (true);

		blackBackgroundColor = new Color (0, 0, 0, 1);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (blackBackgroundColor.a > 0) {
			blackBackground1.color = blackBackgroundColor;
			blackBackgroundColor.a -= 0.015f;
		}
		if (blackBackgroundColor.a <= 0) {
			blackBackgroundColor.a = 0;
			blackBackground1.gameObject.SetActive (false);
		}
	}
}
