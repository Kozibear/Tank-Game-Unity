using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {

	public GameObject parentTank;

	public Camera cam;

	// Use this for initialization
	void Start () {
		if (parentTank.GetComponent<TankMovement> ().m_PlayerNumber == 1) {

			cam.rect = new Rect (-0.5f, 0f, 1f, 1f);
		} 
		else {
			cam.rect = new Rect (0.5f, 0f, 1f, 1f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
