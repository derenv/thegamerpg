﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_manager : MonoBehaviour {
	//globals
	public Slider health_bar;
	public Slider xp_bar;
	public Text hp_text;
	public Text xp_text;
	public Text lvl_text;
	public Text potion_text;
	public Text gold_text;
	public player_health_manager player_health;
	private player_stats the_player_stats;
	private static bool ui_exists;

	/* Start method
	 * called on initialization
	 */
	void Start () {
		//avoid duplicates
		if(!ui_exists){
			ui_exists = true;
			DontDestroyOnLoad(transform.gameObject);
		}else{
			Destroy(gameObject);
		}

		the_player_stats = GetComponent<player_stats>();
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update () {
		//health
		health_bar.maxValue = player_health.player_max_health;
		health_bar.value = player_health.player_current_health;
		hp_text.text = "HP : "+player_health.player_current_health+"/"+player_health.player_max_health;

		//level
		lvl_text.text = ""+the_player_stats.current_level;

		//xp
		xp_bar.maxValue = the_player_stats.xp_levels[the_player_stats.current_level];
		xp_bar.value = the_player_stats.current_xp;
		xp_text.text = "XP : "+the_player_stats.current_xp+"/"+the_player_stats.xp_levels[the_player_stats.current_level];

		//potions
		potion_text.text = ""+player_health.potion_amount();

		//gold
		gold_text.text = ""+the_player_stats.gold;
	}
}
