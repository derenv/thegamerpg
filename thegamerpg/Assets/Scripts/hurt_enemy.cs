using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hurt_enemy : MonoBehaviour {
	//globals
	public int damage_to_give;
	public GameObject damage_burst;
	public Transform hit_point;
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
		if(the_player_stats == null){
			the_player_stats = FindObjectOfType<player_stats>();
		}
	}

	/* 
	 * 
	 */
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "enemy"){
			current_damage = damage_to_give + the_player_stats.current_attack;

			other.gameObject.GetComponent<enemy_health_manager>().hurt_enemy(current_damage);
			Instantiate(damage_burst,hit_point.position,hit_point.rotation);
			var clone = Instantiate(damage_number,hit_point.position,Quaternion.Euler(Vector3.zero));
			clone.GetComponent<floating_numbers>().damage_number = current_damage;
		}
	}
}
