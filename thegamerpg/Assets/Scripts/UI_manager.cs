using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_manager : MonoBehaviour {
	//globals
	public Slider health_bar;
	public Text hp_text;
	public Text lvl_text;
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
		health_bar.maxValue = player_health.player_max_health;
		health_bar.value = player_health.player_current_health;
		hp_text.text = "HP : "+player_health.player_current_health+"/"+player_health.player_max_health;
		lvl_text.text = "LVL: "+the_player_stats.current_level;
	}
}
