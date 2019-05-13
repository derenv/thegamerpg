using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_trigger : MonoBehaviour {
	//globals
	private quest_manager qm;
	public int end_quest_id;
	public int start_quest_id;

	public bool interact;

	public bool start;
	public bool end;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		qm = FindObjectOfType<quest_manager>();
	}
	
	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			if(interact){
				if(Input.GetKeyUp(KeyCode.Space)){
					if(end && !qm.quests_completed[end_quest_id] && qm.quests[end_quest_id].gameObject.activeSelf){
						//if this is a end quest trigger
						//if the quest is not completed
						//if the quest is active
						qm.quests[end_quest_id].end_quest();

						//activate any loot areas
						loot_area[] areas = GetComponentsInChildren<loot_area>(true);
						foreach(loot_area x in areas){
							if(x.quest_required == end_quest_id){
								x.gameObject.SetActive(true);
							}
						}
					}
					if(start && !qm.quests_completed[start_quest_id] && !qm.quests[start_quest_id].gameObject.activeSelf && ((end && qm.quests_completed[end_quest_id]) || !end)){
						//if this is a start quest trigger
						//if the quest is not completed
						//if the quest is not active
						qm.quests[start_quest_id].start_quest();
					}
				}else{
					//show spacebar prompt
					//
				}
			}else{
				if(end && !qm.quests_completed[end_quest_id] && qm.quests[end_quest_id].gameObject.activeSelf){
					//if this is a end quest trigger
					//if the quest is not completed
					//if the quest is active
					qm.quests[end_quest_id].end_quest();

					//activate any loot areas
					loot_area[] areas = GetComponentsInChildren<loot_area>(true);
					foreach(loot_area x in areas){
						if(x.quest_required == end_quest_id){
							x.gameObject.SetActive(true);
						}
					}
				}
				if(start && !qm.quests_completed[start_quest_id] && !qm.quests[start_quest_id].gameObject.activeSelf){
					//if this is a start quest trigger
					//if the quest is not completed
					//if the quest is not active
					qm.quests[start_quest_id].start_quest();
				}
			}
		}
	}
}
