using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;

	public bool canShoot;
	public bool canShoot2;

    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;

	public GameObject lyrics;
	public GameObject gameManager;
	public GameObject time;

    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }
    

    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
		m_AimSlider.value = m_MinLaunchForce;

		if (canShoot) {

			if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired) {

				//at max charge, not fired yet
				m_CurrentLaunchForce = m_MaxLaunchForce;
				Fire ();
			} else if (Input.GetButtonDown (m_FireButton)) {
				//have we pressed fire for the first time?
				m_Fired = false;
				m_CurrentLaunchForce = m_MinLaunchForce;

				m_ShootingAudio.clip = m_ChargingClip;
				m_ShootingAudio.Play ();
			} else if (Input.GetButton (m_FireButton) && !m_Fired) {

				//holding the fire button, not yet fired
				m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

				m_AimSlider.value = m_CurrentLaunchForce;
			} else if (Input.GetButtonUp (m_FireButton) && !m_Fired) {

				//we release the button, having not yet fired
				lyrics.GetComponent<currentWords>().numberSelector();
				Fire ();
			}

		}
	}

    private void Fire()
    {
		canShoot = false;
		if (gameManager.GetComponent<MusicManager> ().blueTankControls) {
			gameManager.GetComponent<MusicManager> ().blueTankControls = false;
			gameManager.GetComponent<MusicManager> ().redTankControls = true;

			gameManager.GetComponent<MusicManager> ().PauseNormalSong ();
			gameManager.GetComponent<MusicManager> ().exceptionHoldDown = true;

			time.GetComponent<TimeCountdown>().resetNumber();
		}
		else if (gameManager.GetComponent<MusicManager> ().redTankControls) {
			gameManager.GetComponent<MusicManager> ().redTankControls = false;
			gameManager.GetComponent<MusicManager> ().blueTankControls = true;

			gameManager.GetComponent<MusicManager> ().PauseNormalSong ();
			gameManager.GetComponent<MusicManager> ().exceptionHoldDown = true;

			time.GetComponent<TimeCountdown>().resetNumber();
		}

		canShoot = true;

        // Instantiate and launch the shell.
		m_Fired = true;

		Rigidbody shellInstance = Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

		shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

		m_ShootingAudio.clip = m_FireClip;
		m_ShootingAudio.Play ();

		m_CurrentLaunchForce = m_MinLaunchForce;
    }
}