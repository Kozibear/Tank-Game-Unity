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

	// Use this for initialization
	void Start () {
		normalThemeCurrentTime = 0;
		reversedThemeCurrentTime = 0;

		normalTheme.time = 0;
		reversedTheme.time = 0;

		blueTankControls = false;
		redTankControls = false;
		canAccessControls = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (blueTankControls && canAccessControls) {
			//the moment that we press down on the W key, and we're not pressing the S key, we start playing the normal song
			//Or alternatively, it also works if we're currently pressing the key and we lift up the other key
			if ((Input.GetKeyDown (KeyCode.W) && !Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.W) && Input.GetKeyUp (KeyCode.S)) && !normalTheme.isPlaying) {

				PlayNormalSong ();
			}

			//when we lift the key up, we pause it
			if (Input.GetKeyUp (KeyCode.W) && normalTheme.isPlaying) {
				PauseNormalSong ();
			}

			//key down, and the other key not being pressed, we play the song
			//Or alternatively, it also works if we're currently pressing the key and we lift up the other key
			if ((Input.GetKeyDown (KeyCode.S) && !Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.S) && Input.GetKeyUp (KeyCode.W)) && !reversedTheme.isPlaying) {

				PlayReversedSong ();
			}

			//when we lift the key up, we pause it
			if (Input.GetKeyUp (KeyCode.S) && reversedTheme.isPlaying) {
				PauseReversedSong ();
			}
		}

		if (redTankControls && canAccessControls) {
			//the moment that we press down on the W key, and we're not pressing the S key, we start playing the normal song
			//Or alternatively, it also works if we're currently pressing the key and we lift up the other key
			if ((Input.GetKeyDown (KeyCode.UpArrow) && !Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.UpArrow) && Input.GetKeyUp (KeyCode.DownArrow)) && !normalTheme.isPlaying) {

				PlayNormalSong ();
			}

			//when we lift the key up, we pause it
			if (Input.GetKeyUp (KeyCode.UpArrow) && normalTheme.isPlaying) {
				PauseNormalSong ();
			}

			//key down, and the other key not being pressed, we play the song
			//Or alternatively, it also works if we're currently pressing the key and we lift up the other key
			if ((Input.GetKeyDown (KeyCode.DownArrow) && !Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.DownArrow) && Input.GetKeyUp (KeyCode.UpArrow)) && !reversedTheme.isPlaying) {

				PlayReversedSong ();
			}

			//when we lift the key up, we pause it
			if (Input.GetKeyUp (KeyCode.DownArrow) && reversedTheme.isPlaying) {
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
		normalTheme.Play ();
	}

	public void PlayReversedSong()
	{
		reversedTheme.Play ();
	}
}
