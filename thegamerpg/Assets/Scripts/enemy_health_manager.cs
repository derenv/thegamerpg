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

	private UI_manager ui;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		current_health = max_health;

		the_player_stats = FindObjectOfType<player_stats>();

		qm = FindObjectOfType<quest_manager>();

		ui = FindObjectOfType<UI_manager>();
	}

	/* Update method
	 * called once per frame
	 */
	void Update(){
		if(gameObject.name.Equals("Boss")){
			ui.boss_hp_bar.gameObject.SetActive(true);
			if(ui == null){
				ui = FindObjectOfType<UI_manager>();
			}
			ui.boss_hp_bar.maxValue = max_health;
			ui.boss_hp_bar.value = current_health;
		}

		if(the_player_stats == null){
			the_player_stats = FindObjectOfType<player_stats>();
		}
		if(current_health <= 0){
			qm.enemy_killed = enemy_name;

			Destroy(gameObject);

			if(gameObject.name.Equals("Boss")){
				ui.boss_hp_bar.gameObject.SetActive(false);
			}

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
