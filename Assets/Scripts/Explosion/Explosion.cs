using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public Vector3 originalScale;

	// Use this for initialization
	void Start () {
		originalScale = this.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.right, 7);

		//transform.localScale = new Vector3 (MusicManager.currentSample, MusicManager.currentSample, MusicManager.currentSample);

		if (transform.localScale.x > MusicManager.currentSample) {
			transform.localScale -= new Vector3 (0.7f, 0.7f, 0.7f);

		}

		if (transform.localScale.x < MusicManager.currentSample) {
			transform.localScale += new Vector3 (0.7f, 0.7f, 0.7f);

		}

		if (GameManager.resetExplosions) {

			Destroy (gameObject);
		}

	}
}
