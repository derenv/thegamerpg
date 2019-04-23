using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volume_controller : MonoBehaviour {
	//globals
	private AudioSource the_audio;

	private float audio_level;
	public float default_audio;

	/* Start method
	 * called on initialization
	 */
	void Start () {
		the_audio = GetComponent<AudioSource>();
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update () {
		
	}

	public void set_audio_level(float volume){
		if (the_audio == null) {
			the_audio = GetComponent<AudioSource>();
		}
		audio_level = default_audio * volume;
		the_audio.volume = audio_level;
	}
}
