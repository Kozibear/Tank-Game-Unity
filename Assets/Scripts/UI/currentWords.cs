using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentWords : MonoBehaviour {

	public Text text;

	public float textSelection;

	public GameObject gameManager;

	public float colorFlash;

	public Image redImage;

	public Image blueImage;

	public Color temp1;
	public Color temp2;

	// Use this for initialization
	void Start () {
		textSelection = 1;
		colorFlash = 0;

		temp1 = blueImage.GetComponent<Image> ().color;
		temp2 = redImage.GetComponent<Image> ().color;
	}
	
	// Update is called once per frame
	void Update () {

		if (GameManager.resetExplosions) {
			textSelection = 1;
		}
		
		blueImage.GetComponent<Image> ().color = temp1;
		redImage.GetComponent<Image> ().color = temp2;

		if (gameManager.GetComponent<MusicManager> ().blueTankControls) {
			text.color = new Color (0.07f, 0.49f, 0.972f, 1);
		} 
		else if (gameManager.GetComponent<MusicManager> ().redTankControls) {
			text.color = new Color (0.654f, 0.086f, 0.086f, 1);
		}

		if (colorFlash != 1 && gameManager.GetComponent<MusicManager> ().blueTankControls) {
			temp1.a = 1;
			colorFlash = 1;
		}
		if (colorFlash != 2 && gameManager.GetComponent<MusicManager> ().redTankControls) {
			temp2.a = 1;
			colorFlash = 2;
		}

		if (temp1.a > 0) {
			temp1.a -= 0.05f;
		}

		if (temp2.a > 0) {
			temp2.a -= 0.05f;
		}

		if (temp1.a <= 0) {
			temp1.a = 0;
		}

		if (temp2.a <= 0) {
			temp2.a = 0;
		}
		
		if (textSelection == 1) {
			text.text = "\"wave the flag\"";
		}

		if (textSelection == 2) {
			text.text = "\"hail to the chief\"";
		}
		if (textSelection == 3) {
			text.text = "\"point the cannon\"";
		}
		if (textSelection == 4) {
			text.text = "\"silver spoon\"";
		}
		if (textSelection == 5) {
			text.text = "\"the taxman comes\"";
		}
		if (textSelection == 6) {
			text.text = "\"a rummage sale\"";
		}
		if (textSelection == 7) {
			text.text = "\"millionaire's son\"";
		}
		if (textSelection == 8) {
			text.text = "\"star spangled eyes\"";
		}
		if (textSelection == 9) {
			text.text = "\"should we give?\"";
		}
		if (textSelection == 10) {
			text.text = "\"More! More! More!\"";
		}
	}

	public void numberSelector ()
	{
		if (textSelection == 1) {
			float[] list = new float[] {2, 3, 4};
			textSelection = list[Random.Range (0, list.Length)];
		}
		else if (textSelection == 2) {
			float[] list = new float[] {1, 3, 4, 5};
			textSelection = list[Random.Range (0, list.Length)];
		}
		else if (textSelection == 3) {
			float[] list = new float[] {1, 2, 4, 5};
			textSelection = list[Random.Range (0, list.Length)];
		}
		else if (textSelection == 4) {
			float[] list = new float[] {2, 3, 5, 6};
			textSelection = list[Random.Range (0, list.Length)];
		}
		else if (textSelection == 5) {
			float[] list = new float[] {3, 4, 6, 7};
			textSelection = list[Random.Range (0, list.Length)];
		}
		else if (textSelection == 6) {
			float[] list = new float[] {4, 5, 7, 8};
			textSelection = list[Random.Range (0, list.Length)];
		}
		else if (textSelection == 7) {
			float[] list = new float[] {5, 6, 8, 9};
			textSelection = list[Random.Range (0, list.Length)];
		}
		else if (textSelection == 8) {
			float[] list = new float[] {6, 7, 9, 10};
			textSelection = list[Random.Range (0, list.Length)];
		}
		else if (textSelection == 9) {
			float[] list = new float[] {6, 7, 8, 10};
			textSelection = list[Random.Range (0, list.Length)];
		}
		else if (textSelection == 10) {
			float[] list = new float[] {7, 8, 9};
			textSelection = list[Random.Range (0, list.Length)];
		}
	}
}
