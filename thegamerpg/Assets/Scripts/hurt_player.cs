using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurt_player : MonoBehaviour {
	//globals
	public int damage_to_give;
	public GameObject damage_number;
	private int current_damage;

	private player_stats the_player_stats;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		the_player_stats = FindObjectOfType<player_stats>();
	}

	/* Update method
	 * called once per frame
	 */
	void Update(){
		//
	}

	/* 
	 * called when 2D collision occurs
	 * initiates combat when colliding object is player
	 */
	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.name == "Player"){
			//calculate damage
			current_damage = damage_to_give - the_player_stats.current_defence;
			if (current_damage < 0) {
				current_damage = 0;
			}

			//apply damage
			other.gameObject.GetComponent<player_health_manager>().hurt_player(current_damage);

			//hit number anim
			var clone = Instantiate(damage_number,other.transform.position,Quaternion.Euler(Vector3.zero));
			clone.GetComponent<floating_numbers>().damage_number = current_damage;
		}
	}
}
