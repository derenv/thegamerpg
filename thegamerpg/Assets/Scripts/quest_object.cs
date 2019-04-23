using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_object : MonoBehaviour {
	//globals
	public int quest_id;
	public quest_manager qm;

	public string[] start_text;
	public string[] end_text;

	public bool is_item_quest;
	public string target_item;

	public bool is_enemy_quest;
	public string target_enemy;
	private int current_kill_amount;
	public int total_kill_amount;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		//
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update(){
		if(is_item_quest){
			if(qm.item_collected == target_item){
				qm.item_collected = null;
				end_quest();
			}
		}
		if(is_enemy_quest){
			if(qm.enemy_killed == target_enemy){
				qm.enemy_killed = null;
				current_kill_amount++;
			}
			if(current_kill_amount >= total_kill_amount){
				end_quest();
			}
		}
	}

	public void start_quest(){
		gameObject.SetActive(true);
		qm.show_quest_dialogue(start_text);
	}

	public void end_quest(){
		gameObject.SetActive(false);
		qm.quests_completed[quest_id] = true;
		qm.show_quest_dialogue(end_text);
	}
}
