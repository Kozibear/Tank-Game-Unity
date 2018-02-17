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
    public TankManager[] m_Tanks;           
	public GameObject TimeText;
	public GameObject LyricText;

	public bool countDownExplosion;
	public bool beginningOrEndOfSong;

    private int m_RoundNumber;              
    private WaitForSeconds m_StartWait;     
    private WaitForSeconds m_EndWait;       
    private TankManager m_RoundWinner;
    private TankManager m_GameWinner;       

	public static bool resetExplosions;

    private void Start()
    {
        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

        SpawnAllTanks();
        SetCameraTargets();

        StartCoroutine(GameLoop());

		beginningOrEndOfSong = false;

		resetExplosions = false;
    }

	public void Update()
	{
		//if the timer reaches the end, we make the current tank that can shoot other tanks explode
		if ((TimeText.GetComponent<TimeCountdown> ().currentTime <= 0 && countDownExplosion) || beginningOrEndOfSong) {
			if (this.gameObject.GetComponent<MusicManager> ().redTankControls) {
				m_Tanks [1].m_Instance.GetComponent<TankHealth> ().OnDeath ();
				countDownExplosion = false;
				beginningOrEndOfSong = false;
			}
			if (this.gameObject.GetComponent<MusicManager> ().blueTankControls) {
				m_Tanks [0].m_Instance.GetComponent<TankHealth> ().OnDeath ();
				countDownExplosion = false;
				beginningOrEndOfSong = false;
			}	
		}

		if (this.gameObject.GetComponent<MusicManager> ().blueTankControls && this.gameObject.GetComponent<MusicManager> ().normalTheme.isPlaying) {

			if (LyricText.GetComponent<currentWords> ().textSelection == 1 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 21f && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 22.5f) {
				m_Tanks [0].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 2 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 27 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 29) {
				m_Tanks [0].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 3 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 30 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 31.5f) {
				m_Tanks [0].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 4 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 48.5f && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 50) {
				m_Tanks [0].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 5 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 55 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 57) {
				m_Tanks [0].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 6 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 59.5f && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 61) {
				m_Tanks [0].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 7 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 66 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 68) {
				m_Tanks [0].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 8 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 92 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 94) {
				m_Tanks [0].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 9 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 100.2f && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 101.6f) {
				m_Tanks [0].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 10 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 103 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 105.2f) {
				m_Tanks [0].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			} 
			else {
				m_Tanks [0].m_Instance.GetComponent<TankShooting> ().canShoot = false;
			}
		}

		if (this.gameObject.GetComponent<MusicManager> ().redTankControls && this.gameObject.GetComponent<MusicManager> ().normalTheme.isPlaying) {

			if (LyricText.GetComponent<currentWords> ().textSelection == 1 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 21f && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 22.5f) {
				m_Tanks [1].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 2 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 27 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 29) {
				m_Tanks [1].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 3 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 30 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 31.5f) {
				m_Tanks [1].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 4 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 48.5f && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 50) {
				m_Tanks [1].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 5 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 55 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 57) {
				m_Tanks [1].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 6 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 59.5f && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 61) {
				m_Tanks [1].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 7 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 66 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 68) {
				m_Tanks [1].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 8 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 92 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 94) {
				m_Tanks [1].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 9 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 100.2f && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 101.6f) {
				m_Tanks [1].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			}
			else if (LyricText.GetComponent<currentWords> ().textSelection == 10 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time > 103 && this.gameObject.GetComponent<MusicManager> ().normalTheme.time < 105.2f) {
				m_Tanks [1].m_Instance.GetComponent<TankShooting> ().canShoot = true;
			} 
			else {
				m_Tanks [1].m_Instance.GetComponent<TankShooting> ().canShoot = false;
			}
		}
	}


    private void SpawnAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].m_Instance =
                Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
            m_Tanks[i].m_PlayerNumber = i + 1;
            m_Tanks[i].Setup();
			m_Tanks [i].m_Instance.GetComponent<TankShooting> ().lyrics = LyricText;
			m_Tanks [i].m_Instance.GetComponent<TankShooting> ().gameManager = this.gameObject;
			m_Tanks [i].m_Instance.GetComponent<TankShooting> ().time = TimeText;
        }
    }


    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[m_Tanks.Length];

        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_Tanks[i].m_Instance.transform;
        }

        m_CameraControl.m_Targets = targets;
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

		TimeText.GetComponent<TimeCountdown> ().currentTime = 100;
		TimeText.GetComponent<TimeCountdown> ().recordedTime = 100;

		this.GetComponent<MusicManager>().normalTheme.time = 0;
		this.GetComponent<MusicManager>().reversedTheme.time = 0;

		this.GetComponent<MusicManager>().exceptionHoldDown = true;
		this.GetComponent<MusicManager> ().haveGoneForward = false;

		countDownExplosion = true;

		m_CameraControl.SetStartPositionAndSize ();

		m_RoundNumber++;
		m_MessageText.text = "ROUND " + m_RoundNumber;

		//we choose which tank to activate
		if (!this.gameObject.GetComponent<MusicManager> ().blueTankControls && !this.gameObject.GetComponent<MusicManager> ().redTankControls) {

			float randomNumber;
			randomNumber = Random.Range (0, 2);

			if (randomNumber == 0) {
				this.gameObject.GetComponent<MusicManager> ().redTankControls = true;
			}
			if (randomNumber == 1) {
				this.gameObject.GetComponent<MusicManager> ().blueTankControls = true;
			}
		}
		else if (this.gameObject.GetComponent<MusicManager> ().blueTankControls) {
			this.gameObject.GetComponent<MusicManager> ().blueTankControls = false;
			this.gameObject.GetComponent<MusicManager> ().redTankControls = true;
		}
		else if (this.gameObject.GetComponent<MusicManager> ().redTankControls) {
			this.gameObject.GetComponent<MusicManager> ().redTankControls = false;
			this.gameObject.GetComponent<MusicManager> ().blueTankControls = true;
		}

		resetExplosions = true;
			
        yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
		EnableTankControl ();

		TimeText.GetComponent<TimeCountdown> ().StartCoroutine("TimeLoop");

		m_MessageText.text = string.Empty;

		this.gameObject.GetComponent<MusicManager> ().canAccessControls = true;

		resetExplosions = false;

		while (!OneTankLeft())
		{
       		yield return null;
		}
    }


    private IEnumerator RoundEnding()
    {
		TimeText.GetComponent<TimeCountdown> ().StopCoroutine("TimeLoop");

		DisableTankControl ();

		this.gameObject.GetComponent<MusicManager> ().canAccessControls = false;

		m_RoundWinner = null;

		m_RoundWinner = GetRoundWinner ();

		if (m_RoundWinner != null)
			m_RoundWinner.m_Wins++;

		m_GameWinner = GetGameWinner ();

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