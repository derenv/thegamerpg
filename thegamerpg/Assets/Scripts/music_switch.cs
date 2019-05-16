using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music_switch : MonoBehaviour {
	//globals
	private music_controller mc;

	public int new_track;
	public bool switch_on_start;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		mc = FindObjectOfType<music_controller>();

		if(switch_on_start){
			mc.switch_track(new_track);
			gameObject.SetActive(false);
		}
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update(){
		//
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name == "Player" && mc.current_track != new_track){
			mc.switch_track(new_track);
		}
	}
}
