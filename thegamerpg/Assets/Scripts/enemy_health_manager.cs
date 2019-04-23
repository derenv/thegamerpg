using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_health_manager : MonoBehaviour {
	//globals
	public int max_health;
	public int current_health;
	public int xp;
	private player_stats the_player_stats;

	public quest_manager qm;
	public string enemy_name;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		current_health = max_health;

		the_player_stats = FindObjectOfType<player_stats>();

		qm = FindObjectOfType<quest_manager>();
	}

	/* Update method
	 * called once per frame
	 */
	void Update(){
		if(current_health <= 0){
			qm.enemy_killed = enemy_name;

			Destroy(gameObject);

			the_player_stats.add_xp(xp);
		}
	}

	/* 
	 * 
	 */
	public void hurt_enemy(int damage){
		current_health -= damage;
	}

	/* 
	 * 
	 */
	public void set_max_health(){
		current_health = max_health;
	}
}
