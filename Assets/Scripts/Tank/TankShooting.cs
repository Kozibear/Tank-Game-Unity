﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Shell;   
	public Rigidbody m_egg;
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;

	public Rigidbody egg;
	public GameObject thisTankAge;
	public bool mature;
    
    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;                

	public bool isParent;

	public float shellXFactor;
	public float shellYFactor;
	public float shellZFactor;

    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

		shellXFactor = 1;
		shellYFactor = 1;
		shellZFactor = 1;
    }
    

    private void Update()
    {
		if (m_ShootingAudio.pitch <= 0) {
			m_ShootingAudio.pitch = 0.1f;
		}
		if (m_ShootingAudio.pitch > 10) {
			m_ShootingAudio.pitch = 10;
		}

		if (!isParent) {
			// Track the current state of the fire button and make decisions based on the current launch force.
			m_AimSlider.value = m_MinLaunchForce;
	
			if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired) {

				//at max charge, not fired yet
				m_CurrentLaunchForce = m_MaxLaunchForce;

				if (mature) {
					FireEgg ();
				} else {
					Fire ();
				}
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
				Fire ();
			}
		}
	}

	public IEnumerator ShootShellsRepeatedly()
	{
		while (isParent) {
			yield return new WaitForSeconds (1f);
			Fire ();
		}

		yield return null;
	}

    private void Fire()
    {
        // Instantiate and launch the shell.
		if (!isParent && thisTankAge.GetComponent<tankAge> ().age > 21) {
			thisTankAge.GetComponent<tankAge> ().age += 2;
		}

		m_Fired = true;

		Rigidbody shellInstance = Instantiate (m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

		//we alter the shell's size:
		if (shellXFactor != 0) {
			shellInstance.transform.localScale = new Vector3 (shellXFactor, shellYFactor, shellZFactor);
		}

		shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

		m_ShootingAudio.clip = m_FireClip;
		m_ShootingAudio.Play ();

		m_CurrentLaunchForce = m_MinLaunchForce;
    }

	private void FireEgg()
	{
		// Instantiate and launch the shell.
		m_Fired = true;

		Rigidbody shellInstance = Instantiate (m_egg, m_FireTransform.position, m_FireTransform.rotation * Quaternion.Euler(0, -90, 0)) as Rigidbody;

		egg = shellInstance;

		shellInstance.GetComponent<eggExplosion> ().tank = this.gameObject;

		shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

		m_ShootingAudio.clip = m_FireClip;
		m_ShootingAudio.Play ();

		m_CurrentLaunchForce = m_MinLaunchForce;
	}

	public void resetAge()
	{
		thisTankAge.GetComponent<tankAge> ().age = 0;
	}
}