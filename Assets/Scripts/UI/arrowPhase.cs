using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrowPhase : MonoBehaviour {

	public Image arrow;

	public Color arrowColor;

	public bool fading;

	public bool permanentFade;

	// Use this for initialization
	void Start () {
		arrowColor = arrow.color;
		fading = true;
		permanentFade = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		arrow.color = arrowColor;

		if (!permanentFade) {

			if (fading && arrowColor.a > 0.1f) {
				arrowColor.a -= 0.03f;
			}
			if (fading && arrowColor.a <= 0.1f) {
				fading = false;
			}

			if (!fading && arrowColor.a < 1f) {
				arrowColor.a += 0.03f;
			}
			if (!fading && arrowColor.a >= 1f) {
				fading = true;
			}
		}

		if (permanentFade) {
			
			if (arrowColor.a > 0) {
				arrowColor.a -= 0.03f;
			}
			if (arrowColor.a <= 0) {
				arrowColor.a = 0;
			}
		}
	}
}
