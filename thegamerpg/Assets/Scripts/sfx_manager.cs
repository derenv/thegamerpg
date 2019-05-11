using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sfx_manager : MonoBehaviour {
	//globals
	public AudioSource player_hurt;
	public AudioSource player_dead;
	public AudioSource player_attack;

	//private static bool sfx_manager_exists;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		//avoid duplicates
		/*
		if(!sfx_manager_exists){
			sfx_manager_exists = true;
			DontDestroyOnLoad(transform.gameObject);
		}else{
			Destroy(gameObject);
		}*/
	}
}
