using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eggExplosion : MonoBehaviour {

	public LayerMask m_TankMask;
	public ParticleSystem m_ExplosionParticles;       
	public AudioSource m_ExplosionAudio;              
	public float m_MaxDamage = 100f;                  
	public float m_ExplosionForce = 1000f;            
	public float m_MaxLifeTime = 2f;                  
	public float m_ExplosionRadius = 5f;

	public GameObject tank;

	private void Start()
	{
		Destroy(gameObject, m_MaxLifeTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		//we instantiate a clone of the current tank and change certain settings to make it function a specific way
		GameObject originalParentTank;
		originalParentTank = Instantiate(tank, tank.transform.position, tank.transform.rotation) as GameObject;
		originalParentTank.GetComponent<TankMovement> ().enabled = false;

		originalParentTank.GetComponent<TankShooting> ().isParent = true;
		originalParentTank.GetComponent<TankShooting> ().StartCoroutine ("ShootShellsRepeatedly");

		originalParentTank.GetComponent<TankHealth> ().isParent = true;
		originalParentTank.GetComponent<TankHealth> ().StartCoroutine ("DieEventually");

		originalParentTank.GetComponent<parentTankStuff> ().enabled = true;
		if (tank.name == "BlueTank") {
			originalParentTank.GetComponent<parentTankStuff> ().isBlueTank = true;
		}
		if (tank.name == "RedTank") {
			originalParentTank.GetComponent<parentTankStuff> ().isRedTank = true;
		}

		//we move the tank to the new position and reset its values
		tank.transform.localScale = new Vector3 (tank.transform.localScale.x*0.2f, tank.transform.localScale.y*0.2f, tank.transform.localScale.z*0.2f);
		tank.GetComponent<TankHealth> ().m_CurrentHealth = 100;
		tank.GetComponent<TankShooting> ().resetAge ();
		tank.transform.position = new Vector3(this.transform.position.x, this.transform.position.y+1, this.transform.position.z);

		//we give it new dimensions:
		float randX;
		float randY;
		float randZ;

		//for the chassis:
		randX = Random.Range (0.7f, 1.4f);
		randY = Random.Range (0.7f, 1.4f);
		randZ = Random.Range (0.7f, 1.4f);
		tank.transform.GetChild(0).GetChild(0).localScale = new Vector3 (tank.transform.GetChild(0).GetChild(0).localScale.x * randX, tank.transform.GetChild(0).GetChild(0).localScale.y * randY, tank.transform.GetChild(0).GetChild(0).localScale.z * randZ);

		//for the wheels:
		randX = Random.Range (0.7f, 1.4f);
		randY = Random.Range (0.7f, 1.4f);
		randZ = Random.Range (0.7f, 1.4f);
		tank.transform.GetChild(0).GetChild(1).localScale = new Vector3 (tank.transform.GetChild(0).GetChild(1).localScale.x * randX, tank.transform.GetChild(0).GetChild(1).localScale.y * randY, tank.transform.GetChild(0).GetChild(1).localScale.z * randZ);
		tank.transform.GetChild(0).GetChild(2).localScale = new Vector3 (tank.transform.GetChild(0).GetChild(2).localScale.x * randX, tank.transform.GetChild(0).GetChild(2).localScale.y * randY, tank.transform.GetChild(0).GetChild(2).localScale.z * randZ);

		//for the turret:
		randX = Random.Range (0.7f, 1.4f);
		randY = Random.Range (0.7f, 1.4f);
		randZ = Random.Range (0.7f, 1.4f);
		tank.transform.GetChild(0).GetChild(3).localScale = new Vector3 (tank.transform.GetChild(0).GetChild(3).localScale.x * randX, tank.transform.GetChild(0).GetChild(3).localScale.y * randY, tank.transform.GetChild(0).GetChild(3).localScale.z * randZ);

		//for the missile size:
		randX = Random.Range (0.7F, 1.4f);
		randY = Random.Range (0.7F, 1.4f);
		randZ = Random.Range (0.7F, 1.4f);
		tank.transform.GetComponent<TankShooting> ().shellXFactor *= randX;
		tank.transform.GetComponent<TankShooting> ().shellYFactor *= randY;
		tank.transform.GetComponent<TankShooting> ().shellZFactor *= randZ;

		//we change the emission value (brightness) for the tank's material:
		randX = Random.Range(-30, 31);
		if (tank.transform.GetChild (0).GetChild (0).GetComponent<TankRust> ().emissionLevel + randX < 0) {
			tank.transform.GetChild (0).GetChild (0).GetComponent<TankRust> ().emissionLevel = 0;
			tank.transform.GetChild (0).GetChild (1).GetComponent<TankRust> ().emissionLevel = 0;
			tank.transform.GetChild (0).GetChild (2).GetComponent<TankRust> ().emissionLevel = 0;
			tank.transform.GetChild (0).GetChild (3).GetComponent<TankRust> ().emissionLevel = 0;
		}
		if (tank.transform.GetChild (0).GetChild (0).GetComponent<TankRust> ().emissionLevel + randX > 255) {
			tank.transform.GetChild (0).GetChild (0).GetComponent<TankRust> ().emissionLevel = 255;
			tank.transform.GetChild (0).GetChild (1).GetComponent<TankRust> ().emissionLevel = 255;
			tank.transform.GetChild (0).GetChild (2).GetComponent<TankRust> ().emissionLevel = 255;
			tank.transform.GetChild (0).GetChild (3).GetComponent<TankRust> ().emissionLevel = 255;
		}
		else {
			tank.transform.GetChild (0).GetChild (0).GetComponent<TankRust> ().emissionLevel += randX;
			tank.transform.GetChild (0).GetChild (1).GetComponent<TankRust> ().emissionLevel += randX;
			tank.transform.GetChild (0).GetChild (2).GetComponent<TankRust> ().emissionLevel += randX;
			tank.transform.GetChild (0).GetChild (3).GetComponent<TankRust> ().emissionLevel += randX;
		}

		//we change the sound effects emitted by the tank:
		randX = Random.Range(-0.5f, 0.5f);
		tank.transform.GetComponent<TankShooting> ().m_ShootingAudio.pitch += randX;

		/*
		// Find all the tanks in an area around the shell and damage them.
		Collider[] colliders = Physics.OverlapSphere (transform.position, m_ExplosionRadius, m_TankMask);

		for (int i = 0; i < colliders.Length; i++) {

			Rigidbody targetRigidbody = colliders [i].GetComponent<Rigidbody> ();

			//if the previous part doesn't exist, continue on with the rest of the code anyways
			if (!targetRigidbody)
				continue;

			targetRigidbody.AddExplosionForce (m_ExplosionForce, transform.position, m_ExplosionRadius);

			TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth> ();

			if (!targetHealth)
				continue;

			float damage = CalculateDamage (targetRigidbody.position);

			targetHealth.TakeDamage (damage);
		}
		*/

		m_ExplosionParticles.transform.parent = null; //we no longer make it have a parent

		m_ExplosionParticles.Play ();

		m_ExplosionAudio.Play ();

		//so that only the explosion particles are destroyed once their duration is elapsed
		Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

		Destroy (gameObject); //and then the entire gameObject is destroyed
	}


	private float CalculateDamage(Vector3 targetPosition)
	{
		// Calculate the amount of damage a target should take based on it's position.
		Vector3 explosionToTarget = targetPosition - transform.position;

		float explosionDistance = explosionToTarget.magnitude;

		float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

		float damage = relativeDistance * m_MaxDamage;

		damage = Mathf.Max (0f, damage); //we make sure that it's not negative

		return damage;
	}
}
