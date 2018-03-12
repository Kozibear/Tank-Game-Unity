using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRust : MonoBehaviour {

	public Renderer rend;

	public Color colorStart;

	public Color rustColor;

	public Color testColor;

	public float emissionLevel;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		colorStart = rend.material.color;
		rustColor = new Color32 (115, 60, 15, 1);

		emissionLevel = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//we set the emission to 
		testColor = new Color32 ((byte) emissionLevel, (byte) emissionLevel, (byte) emissionLevel, 255);
		rend.material.SetColor ("_EmissionColor", testColor);
		if (emissionLevel > 255) {
			emissionLevel = 255;
		}
		if (emissionLevel < 0) {
			emissionLevel = 0;
		}

		if (this.transform.parent.parent.GetComponent<TankShooting> ().thisTankAge.GetComponent<tankAge> ().age < 20) {
			rend.material.color = colorStart;
		}
		if (this.transform.parent.parent.GetComponent<TankShooting>().thisTankAge.GetComponent<tankAge> ().age >= 20) {
			rend.material.color = Color.Lerp (colorStart, rustColor, ((this.transform.parent.parent.GetComponent<TankShooting> ().thisTankAge.GetComponent<tankAge> ().age - 20)/80) );
		}
	}
}
