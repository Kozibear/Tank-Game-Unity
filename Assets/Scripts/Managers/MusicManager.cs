using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	public AudioSource normalTheme;
	public AudioSource reversedTheme;

	public float normalThemeCurrentTime;
	public float reversedThemeCurrentTime;

	public bool blueTankControls;
	public bool redTankControls;

	public bool canAccessControls;

	public bool exceptionHoldDown;

	public bool canExplodeTank;

	public bool haveGoneForward;

	public float[] samples;
	public float[] samples2;

	public static float currentSample;

	// Use this for initialization
	void Start () {
		normalThemeCurrentTime = 0;
		reversedThemeCurrentTime = 0;

		normalTheme.time = 0;
		reversedTheme.time = 0;

		//blueTankControls = false;
		//redTankControls = false;
		canAccessControls = false;

		exceptionHoldDown = true;

		canExplodeTank = false;

		haveGoneForward = false;

		samples = new float[normalTheme.clip.samples*normalTheme.clip.channels];
		normalTheme.clip.GetData (samples, 0);

		samples2 = new float[reversedTheme.clip.samples*reversedTheme.clip.channels];
		reversedTheme.clip.GetData (samples2, 0);
	}
	
	// Update is called once per frame
	void Update() {

		if (blueTankControls && canAccessControls) {
			//the moment that we press down on the W key, and we're not pressing the S key, we start playing the normal song
			//Or alternatively, it also works if we're currently pressing the key and we lift up the other key
			//Or alternatively, it also works if we're holding down the key before we gain control of the song, and the song's current time is set to 0
			if ((Input.GetKeyDown (KeyCode.W) && !Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.W) && Input.GetKeyUp (KeyCode.S)) && !normalTheme.isPlaying || Input.GetKey (KeyCode.W) && (exceptionHoldDown)) {
				PlayNormalSong ();
				exceptionHoldDown = false;
				haveGoneForward = true;
			}

			//when we lift the key up, we pause it
			if (Input.GetKeyUp (KeyCode.W) && normalTheme.isPlaying) {
				PauseNormalSong ();
			}

			//key down, and the other key not being pressed, we play the song
			//Or alternatively, it also works if we're currently pressing the key and we lift up the other key
			if (haveGoneForward && ((Input.GetKeyDown (KeyCode.S) && !Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S) && Input.GetKeyUp (KeyCode.W)) && !reversedTheme.isPlaying || Input.GetKey (KeyCode.S) && exceptionHoldDown)) {

				PlayReversedSong ();
				exceptionHoldDown = false;
			}

			//when we lift the key up, we pause it
			if (haveGoneForward && (Input.GetKeyUp (KeyCode.S) && reversedTheme.isPlaying)) {
				PauseReversedSong ();
			}
		}

		if (redTankControls && canAccessControls) {
			//the moment that we press down on the W key, and we're not pressing the S key, we start playing the normal song
			//Or alternatively, it also works if we're currently pressing the key and we lift up the other key
			if ((Input.GetKeyDown (KeyCode.UpArrow) && !Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.UpArrow) && Input.GetKeyUp (KeyCode.DownArrow)) && !normalTheme.isPlaying || Input.GetKey (KeyCode.UpArrow) && (exceptionHoldDown)) {

				PlayNormalSong ();
				exceptionHoldDown = false;
				haveGoneForward = true;
			}

			//when we lift the key up, we pause it
			if (Input.GetKeyUp (KeyCode.UpArrow) && normalTheme.isPlaying) {
				PauseNormalSong ();
			}

			//key down, and the other key not being pressed, we play the song
			//Or alternatively, it also works if we're currently pressing the key and we lift up the other key
			if (haveGoneForward && ((Input.GetKeyDown (KeyCode.DownArrow) && !Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) && Input.GetKeyUp (KeyCode.UpArrow)) && !reversedTheme.isPlaying || Input.GetKey (KeyCode.DownArrow) && exceptionHoldDown)) {

				PlayReversedSong ();
				exceptionHoldDown = false;
			}

			//when we lift the key up, we pause it
			if (haveGoneForward && (Input.GetKeyUp (KeyCode.DownArrow) && reversedTheme.isPlaying)) {
				PauseReversedSong ();
			}
		}

		if (!canAccessControls) {

			if (reversedTheme.isPlaying) {
				PauseReversedSong ();
			}

			if (normalTheme.isPlaying) {
				PauseNormalSong ();
			}
		}
		//if we can explode the tank, the reversed theme is playing, and it's time is at 0, we make the current tank explode:
		if (canExplodeTank && reversedTheme.time >= reversedTheme.clip.length)
		{
			this.GetComponent<GameManager> ().beginningOrEndOfSong = true;
			canExplodeTank = false;
		}

		//alternatively, if the normal time is playing, and we reach the end, we make the current tank explode:
		if (canExplodeTank && normalTheme.time >= normalTheme.clip.length) 
		{
			this.GetComponent<GameManager> ().beginningOrEndOfSong = true;
			canExplodeTank = false;
		}
	}

	public void PauseNormalSong()
	{
		if (normalThemeCurrentTime < normalTheme.clip.length && normalTheme.isPlaying) {
			normalThemeCurrentTime = normalTheme.time;
			reversedTheme.time = reversedTheme.clip.length - normalThemeCurrentTime;
		} 
		else {
			reversedTheme.time = 0;
		}

		normalTheme.Pause ();
	}

	public void PauseReversedSong()
	{
		if (reversedThemeCurrentTime < reversedTheme.clip.length && reversedTheme.isPlaying) {
			reversedThemeCurrentTime = reversedTheme.time;
			normalTheme.time = normalTheme.clip.length - reversedThemeCurrentTime;
		} 
		else {
			normalTheme.time = 0;
		}

		reversedTheme.Pause ();
	}

	public void PlayNormalSong()
	{
		canExplodeTank = true;
		normalTheme.Play ();
		StartCoroutine(normalSongDecibels());
	}

	public void PlayReversedSong()
	{
		reversedTheme.Play ();
		StartCoroutine(reversedSongDecibels());
	}

	public IEnumerator normalSongDecibels()
	{
		while (normalTheme.isPlaying) { 
			yield return new WaitForSeconds (0.05f);
			currentSample = Mathf.Abs(samples [normalTheme.timeSamples])*20;
		}

		yield return null;
	}

	public IEnumerator reversedSongDecibels()
	{
		while (reversedTheme.isPlaying) {
			yield return new WaitForSeconds (0.05f);
			currentSample = Mathf.Abs(samples2 [reversedTheme.timeSamples])*20;
		}

		yield return null; 
	}
}
