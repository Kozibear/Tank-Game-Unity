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

	public GameObject Tank1RenderCanvas1;
	public GameObject Tank1RenderCanvas2;
	public GameObject Tank1RenderCanvas3;
	public GameObject Tank1RenderCanvas4;
	public GameObject Tank1RenderCanvas5;

	public GameObject Tank2RenderCanvas1;
	public GameObject Tank2RenderCanvas2;
	public GameObject Tank2RenderCanvas3;
	public GameObject Tank2RenderCanvas4;
	public GameObject Tank2RenderCanvas5;

	public GameObject shooter;

	public float shellNumber;

    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
	}


    private void OnTriggerEnter(Collider other)
	{
		//for the lights, camera intervention:
		if (other.gameObject.tag == "floor") {
			
			if(shellNumber == 1) {

				//first, we check to see if there are too many render textures onscreen, and we delete the oldest one
				if (shooter.GetComponent<TankRenderScreenControl> ().Screen1GameObject != null && shooter.GetComponent<TankRenderScreenControl> ().Screen2GameObject != null && shooter.GetComponent<TankRenderScreenControl> ().Screen3GameObject != null && shooter.GetComponent<TankRenderScreenControl> ().Screen4GameObject != null && shooter.GetComponent<TankRenderScreenControl> ().Screen5GameObject != null) {
					shooter.GetComponent<TankRenderScreenControl> ().DeleteOldestScreen();
				}

				if (shooter.GetComponent<TankRenderScreenControl> ().Screen1GameObject == null) {
					GameObject screen = Instantiate (Tank1RenderCanvas1, transform.position + new Vector3 (0, 1.5f, 0), Quaternion.Euler (0, this.transform.rotation.eulerAngles.y - 180, 0));
					screen.GetComponent<screenScript> ().CorrespondingTank = shooter;
					shooter.GetComponent<TankRenderScreenControl> ().Screen1GameObject = screen;

				} else if (shooter.GetComponent<TankRenderScreenControl> ().Screen2GameObject == null) {
					GameObject screen = Instantiate (Tank1RenderCanvas2, transform.position + new Vector3 (0, 1.5f, 0), Quaternion.Euler (0, this.transform.rotation.eulerAngles.y - 180, 0));
					screen.GetComponent<screenScript> ().CorrespondingTank = shooter;
					shooter.GetComponent<TankRenderScreenControl> ().Screen2GameObject = screen;

				} else if (shooter.GetComponent<TankRenderScreenControl> ().Screen3GameObject == null) {
					GameObject screen = Instantiate (Tank1RenderCanvas3, transform.position + new Vector3 (0, 1.5f, 0), Quaternion.Euler (0, this.transform.rotation.eulerAngles.y - 180, 0));
					screen.GetComponent<screenScript> ().CorrespondingTank = shooter;
					shooter.GetComponent<TankRenderScreenControl> ().Screen3GameObject = screen;

				} else if (shooter.GetComponent<TankRenderScreenControl> ().Screen4GameObject == null) {
					GameObject screen = Instantiate (Tank1RenderCanvas4, transform.position + new Vector3 (0, 1.5f, 0), Quaternion.Euler (0, this.transform.rotation.eulerAngles.y - 180, 0));
					screen.GetComponent<screenScript> ().CorrespondingTank = shooter;
					shooter.GetComponent<TankRenderScreenControl> ().Screen4GameObject = screen;

				} else if (shooter.GetComponent<TankRenderScreenControl> ().Screen5GameObject == null) {
					GameObject screen = Instantiate (Tank1RenderCanvas5, transform.position + new Vector3 (0, 1.5f, 0), Quaternion.Euler (0, this.transform.rotation.eulerAngles.y - 180, 0));
					screen.GetComponent<screenScript> ().CorrespondingTank = shooter;
					shooter.GetComponent<TankRenderScreenControl> ().Screen5GameObject = screen;
				}
			}
				
			if (shellNumber == 2) {

				//first, we check to see if there are too many render textures onscreen, and we delete the oldest one
				if (shooter.GetComponent<TankRenderScreenControl> ().Screen1GameObject != null && shooter.GetComponent<TankRenderScreenControl> ().Screen2GameObject != null && shooter.GetComponent<TankRenderScreenControl> ().Screen3GameObject != null && shooter.GetComponent<TankRenderScreenControl> ().Screen4GameObject != null && shooter.GetComponent<TankRenderScreenControl> ().Screen5GameObject != null) {
					shooter.GetComponent<TankRenderScreenControl> ().DeleteOldestScreen();
				}

				if (shooter.GetComponent<TankRenderScreenControl> ().Screen1GameObject == null) {
					GameObject screen = Instantiate (Tank2RenderCanvas1, transform.position + new Vector3 (0, 1.5f, 0), Quaternion.Euler (0, this.transform.rotation.eulerAngles.y - 180, 0));
					screen.GetComponent<screenScript> ().CorrespondingTank = shooter;
					shooter.GetComponent<TankRenderScreenControl> ().Screen1GameObject = screen;

				} else if (shooter.GetComponent<TankRenderScreenControl> ().Screen2GameObject == null) {
					GameObject screen = Instantiate (Tank2RenderCanvas2, transform.position + new Vector3 (0, 1.5f, 0), Quaternion.Euler (0, this.transform.rotation.eulerAngles.y - 180, 0));
					screen.GetComponent<screenScript> ().CorrespondingTank = shooter;
					shooter.GetComponent<TankRenderScreenControl> ().Screen2GameObject = screen;

				} else if (shooter.GetComponent<TankRenderScreenControl> ().Screen3GameObject == null) {
					GameObject screen = Instantiate (Tank2RenderCanvas3, transform.position + new Vector3 (0, 1.5f, 0), Quaternion.Euler (0, this.transform.rotation.eulerAngles.y - 180, 0));
					screen.GetComponent<screenScript> ().CorrespondingTank = shooter;
					shooter.GetComponent<TankRenderScreenControl> ().Screen3GameObject = screen;

				} else if (shooter.GetComponent<TankRenderScreenControl> ().Screen4GameObject == null) {
					GameObject screen = Instantiate (Tank2RenderCanvas4, transform.position + new Vector3 (0, 1.5f, 0), Quaternion.Euler (0, this.transform.rotation.eulerAngles.y - 180, 0));
					screen.GetComponent<screenScript> ().CorrespondingTank = shooter;
					shooter.GetComponent<TankRenderScreenControl> ().Screen4GameObject = screen;

				} else if (shooter.GetComponent<TankRenderScreenControl> ().Screen5GameObject == null) {
					GameObject screen = Instantiate (Tank2RenderCanvas5, transform.position + new Vector3 (0, 1.5f, 0), Quaternion.Euler (0, this.transform.rotation.eulerAngles.y - 180, 0));
					screen.GetComponent<screenScript> ().CorrespondingTank = shooter;
					shooter.GetComponent<TankRenderScreenControl> ().Screen5GameObject = screen;
				} 
			}
		}

		if (other.gameObject.tag == "redscreen" && shellNumber == 1) {

			other.GetComponent<screenScript> ().explode = true;
			Destroy (gameObject); //we destroy the shell so that there aren't two explosions
		}

		if (other.gameObject.tag == "bluescreen" && shellNumber == 2) {

			other.GetComponent<screenScript> ().explode = true;
			Destroy (gameObject); //we destroy the shell so that there aren't two explosions
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