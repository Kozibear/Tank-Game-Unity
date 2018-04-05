using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenMusic : MonoBehaviour {

	public AudioSource normalTheme;

	public float[] samples;

	public static float currentSample;

	// Use this for initialization
	void Start () {

		samples = new float[normalTheme.clip.samples*normalTheme.clip.channels];
		normalTheme.clip.GetData (samples, 0);
		PlayNormalSong ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

	public void PlayNormalSong()
	{
		normalTheme.Play ();
		StartCoroutine(normalSongDecibels());
	}

	public IEnumerator normalSongDecibels()
	{
		while (normalTheme.isPlaying) { 
			yield return new WaitForSeconds (0.05f);
			currentSample = Mathf.Abs(samples [normalTheme.timeSamples])*20;
		}

		yield return null;
	}
}
