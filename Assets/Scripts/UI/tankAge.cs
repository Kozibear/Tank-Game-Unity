using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tankAge : MonoBehaviour {

	public GameObject tank;

	public float age;

	public Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		text.text = "Age: " + age;

		if (age >= 20) {
			tank.GetComponent<TankShooting> ().mature = true;
		} 
		else {
			tank.GetComponent<TankShooting> ().mature = false;
		}

		if (age >= 100) {
			tank.GetComponent<TankHealth> ().OnDeath ();
			age = 0;
		}
	}

	public IEnumerator TimeLoop()
	{

		while (age != 100) {
			yield return new WaitForSeconds (0.5f);
			age += 1;
		}

		yield return null;
	}
}
