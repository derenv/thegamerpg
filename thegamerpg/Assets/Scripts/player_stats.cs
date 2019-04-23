using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_stats : MonoBehaviour {
	//globals
	public int current_level;
	public int current_xp;
	public int[] xp_levels;
	public int[] hp_levels;
	public int[] attack_levels;
	public int[] defence_levels;

	public int current_hp;
	public int current_attack;
	public int current_defence;
	private player_health_manager player_health;

	/* Start method
	 * called on initialization
	 */
	void Start () {
		current_hp = hp_levels [1];
		current_attack = attack_levels [1];
		current_defence = defence_levels [1];

		player_health = FindObjectOfType<player_health_manager>();
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update () {
		if(current_xp >= xp_levels[current_level]){
			level_up ();
		}
	}

	/* 
	 * 
	 */
	public void add_xp(int xp_to_add){
		current_xp += xp_to_add;
	}
	/* 
	 * 
	 */
	public void level_up(){
		current_level++;

		current_hp = hp_levels[current_level];
		player_health.player_max_health = current_hp;
		player_health.player_current_health += current_hp - hp_levels [current_level - 1];

		current_attack = attack_levels[current_level];
		current_defence = defence_levels[current_level];
	}
}
