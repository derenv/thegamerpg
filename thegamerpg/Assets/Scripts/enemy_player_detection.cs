using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_player_detection : MonoBehaviour{
    private player_stats the_player_stats;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		the_player_stats = FindObjectOfType<player_stats>();
	}
    
	void OnTriggerEnter2D(Collider2D other){
		if(the_player_stats == null){
			the_player_stats = FindObjectOfType<player_stats>();
		}
		if(other.gameObject.name == "Player"){
			//enemy anim
			if(this.gameObject.GetComponent<boss_controller>() != null){
				this.gameObject.GetComponent<boss_controller>().do_anim();
			}else{
				this.gameObject.GetComponent<enemy_controller>().do_anim();
			}
		}
	}
}
