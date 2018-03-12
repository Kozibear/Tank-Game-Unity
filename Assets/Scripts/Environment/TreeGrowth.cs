using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGrowth : MonoBehaviour {

	public float recordTime;

	public bool fullyGrown;

	public bool seedPlants;

	public GameObject ground;

	//stuff related to the trees gradually becoming browner
	public Renderer rend;

	public Color colorStart;

	public Color rustColor;

	// Use this for initialization
	void Start () {
		recordTime = Time.timeSinceLevelLoad;
		seedPlants = true;
		fullyGrown = false;

		rend = GetComponent<Renderer> ();
		colorStart = rend.materials[1].color; //we need to do this specific line of code to get the second material 
		rustColor = new Color32 (115, 60, 15, 1);
	}
	
	// Update is called once per frame
	void Update () {
		//we make sure that the trees are always as tall as the ground
		if (ground == null) {
			ground = GameObject.Find ("MovingGround");
		}
			
		if (this.transform.localScale.x < 1 && Time.timeSinceLevelLoad < recordTime + 20 && !fullyGrown) {
			this.transform.localScale += new Vector3 (0.02f, 0.02f, 0.02f);
		}
		else {
			fullyGrown = true;
		}

		//we spawn the two baby trees
		if (Time.timeSinceLevelLoad >= recordTime + 8f && seedPlants) {

			//for the first tree, which always appears, and in a random place close to the original tree
			GameObject babyTree1;
			float randomXAway = Random.Range (-6, 7);
			float randomZAway = Random.Range (-6, 7);
			float randomYRotation = Random.Range (-50, 50);

			babyTree1 = Instantiate (this.gameObject, this.transform.position + new Vector3 (randomXAway, ground.transform.localPosition.y-this.transform.position.y, randomZAway), this.transform.rotation * Quaternion.Euler(0, randomYRotation, 0)) as GameObject;

			babyTree1.transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);

			//for the second tree, which has only a 1/4 chance of happening, and instantiates in the opposite location of the other tree
			float randomNumber = Random.Range (0, 4);
		
			if (randomNumber == 1) {
				
				GameObject babyTree2;
				float randomXAway2 = -randomXAway;
				float randomZAway2 = -randomZAway;
				float randomYRotation2 = Random.Range (-50, 50);

				babyTree2 = Instantiate (this.gameObject, this.transform.position + new Vector3 (randomXAway2, ground.transform.localPosition.y-this.transform.position.y, randomZAway2), this.transform.rotation * Quaternion.Euler(0, randomYRotation2, 0)) as GameObject;
				babyTree2.transform.localScale = new Vector3 (0.01f, 0.01f, 0.01f);

			}

			seedPlants = false;
		}

		//making the tree's color become browner
		if (Time.timeSinceLevelLoad >= recordTime + 10) {
			rend.materials[1].color = Color.Lerp (colorStart, rustColor, (Time.timeSinceLevelLoad-(recordTime+5))/15);
		}

		//we make the tree shrink
		if (Time.timeSinceLevelLoad >= recordTime + 19) {
			transform.localScale -= new Vector3 (0.002f, 0.005f, 0.002f);

			if (transform.localScale.y <= 0) {
				Destroy (gameObject);
			}
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "shell") {
			Destroy (gameObject);
		}
			
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "shell") {
			Destroy (gameObject);
		}
	}
}
