using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenScript : MonoBehaviour {

	public GameObject CorrespondingTank;

	public float screenNumber;

	public float screenAge;

	public bool explode;

	public LayerMask m_TankMask;
	public ParticleSystem m_ExplosionParticles;       
	public AudioSource m_ExplosionAudio;              
	public float m_MaxDamage = 100f;                  
	public float m_ExplosionForce = 1000f;            
	public float m_MaxLifeTime = 2f;                  
	public float m_ExplosionRadius = 5f;

	// Use this for initialization
	void Start () {

		explode = false;

		screenAge = Time.time;
	}

	public void Endscene()
	{
		Destroy (gameObject);
	}

	// Update is called once per frame
	void Update () {

		if (explode) {

			//THE FOLLOWING FROM HEREAFTER IS BASICALLY JUST THE CODE FROM ShellExplosion, COPIED:
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

			m_ExplosionParticles.transform.parent = null; //we no longer make it have a parent

			m_ExplosionParticles.Play ();

			m_ExplosionAudio.Play ();

			//so that only the explosion particles are destroyed once their duration is elapsed
			Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
			Destroy (gameObject); //and then the entire gameObject is destroyed
		}
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
