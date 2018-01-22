using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;              

	public GameObject renderscreen1;
	public GameObject renderscreen2;

	public float shellNumber;

    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
		//for the lights, camera intervention:
		if (other.gameObject.tag == "floor") {

			if (shellNumber == 1) {
				Instantiate (renderscreen1, transform.position + new Vector3 (0, 1.5f, 0), Quaternion.Euler (90, this.transform.rotation.eulerAngles.y, 180) /*this.transform.rotation*/);
			}
			if (shellNumber == 2) {
				Instantiate (renderscreen2, transform.position + new Vector3 (0, 1.5f, 0), Quaternion.Euler (90, this.transform.rotation.eulerAngles.y, 180) /*this.transform.rotation*/);
			}
		}

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