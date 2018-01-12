using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int m_NumRoundsToWin = 5;        
    public float m_StartDelay = 3f;         
    public float m_EndDelay = 3f;           
    public CameraControl m_CameraControl;   
    public Text m_MessageText;              
    public GameObject m_TankPrefab;
	public GameObject m_InvisibleTankPrefab;
    public TankManager[] m_Tanks;

	//intervention:
	public float m_timeLeft;
	public float currentTime;
	public Text m_TimeText;
	public bool recordTime = false;

    private int m_RoundNumber;              
    private WaitForSeconds m_StartWait;     
    private WaitForSeconds m_EndWait;       
    private TankManager m_RoundWinner;
    private TankManager m_GameWinner;       


    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        SpawnAllTanks();
        SetCameraTargets();

        StartCoroutine(GameLoop());

		m_timeLeft = 99f;
    }


    private void SpawnAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
			/*
			m_Tanks [i].m_Instance = Instantiate (m_TankPrefab, m_Tanks [i].m_SpawnPoint.position, m_Tanks [i].m_SpawnPoint.rotation) as GameObject;
			m_Tanks [i].m_PlayerNumber = i + 1;
			m_Tanks [i].Setup ();
			*/

			if (i == 0) {
				m_Tanks [i].m_Instance = Instantiate (m_TankPrefab, m_Tanks [i].m_SpawnPoint.position, m_Tanks [i].m_SpawnPoint.rotation) as GameObject;
				m_Tanks [i].m_PlayerNumber = i + 1;
				m_Tanks [i].Setup ();
			}
			else if (i != 0) {
				m_Tanks [i].m_Instance = Instantiate (m_InvisibleTankPrefab, m_Tanks [i].m_SpawnPoint.position, m_Tanks [i].m_SpawnPoint.rotation) as GameObject;
				m_Tanks [i].m_PlayerNumber = i + 1;
				m_Tanks [i].Setup ();
			}

        }

    }


    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[m_Tanks.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_Tanks[i].m_Instance.transform;
        }

        //m_CameraControl.m_Targets = targets;
    }


    private IEnumerator GameLoop()
    {
		//it goes through these three coroutines, then checks to see if there was a winner or
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (m_GameWinner != null)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
		ResetAllTanks ();
		DisableTankControl ();

		m_timeLeft = 99f;

		//m_CameraControl.SetStartPositionAndSize ();

		m_RoundNumber++;
		m_MessageText.text = "ROUND " + m_RoundNumber;

        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
		EnableTankControl ();

		recordTime = true;
		currentTime = Time.time;

		m_MessageText.text = string.Empty;

		m_TimeText.text = m_timeLeft.ToString();
		
		while (!OneTankLeft())
		{
       		yield return null;
		}
    }

	//intervention for recording Time
	public void FixedUpdate()
	{
		if (recordTime && m_timeLeft > 0) {
			m_timeLeft = 99 - Mathf.Round(Time.time-currentTime);

			m_MessageText.text = string.Empty;

			m_TimeText.text = m_timeLeft.ToString ();
		} 
		else {
			m_TimeText.text = "Time!";
		}
	}


    private IEnumerator RoundEnding()
    {
		DisableTankControl ();

		m_RoundWinner = null;

		m_RoundWinner = GetRoundWinner ();

		if (m_RoundWinner != null)
			m_RoundWinner.m_Wins++;

		m_GameWinner = GetGameWinner ();

		recordTime = false;

		string message = EndMessage ();
		m_MessageText.text = message;

        yield return m_EndWait;
    }


    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }


    private TankManager GetRoundWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                return m_Tanks[i];
        }

        return null;
    }


    private TankManager GetGameWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
                return m_Tanks[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

        if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
        }

        if (m_GameWinner != null)
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";

        return message;
    }


    private void ResetAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].Reset();
        }
    }


    private void EnableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].EnableControl();
        }
    }


    private void DisableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].DisableControl();
        }
    }
}