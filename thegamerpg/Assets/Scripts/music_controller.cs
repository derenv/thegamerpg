using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music_controller : MonoBehaviour {
	//globals
	//private static bool mc_exists;

	public AudioSource[] tracks;

	public int current_track;
	public bool music_playing;

	/* Start method
	 * called on initialization
	 */
	void Start () {
		//avoid duplicates
		/*
		if(!mc_exists){
			mc_exists = true;
			DontDestroyOnLoad(transform.gameObject);
		}else{
			Destroy(gameObject);
		}*/
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update () {
		if(music_playing){
			if(!tracks[current_track].isPlaying){
				tracks[current_track].Play();
			}
		}else{
			tracks[current_track].Stop();
		}
	}

	public void switch_track(int new_track){
		tracks [current_track].Stop();
		current_track = new_track;
		tracks[current_track].Play();
	}
}
