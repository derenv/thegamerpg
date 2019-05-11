using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_trigger : MonoBehaviour {
	//globals
	private quest_manager qm;
	public int end_quest_id;
	public int start_quest_id;

	public bool interact;
	public bool contact;

	public bool start;
	public bool end;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		qm = FindObjectOfType<quest_manager>();
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update(){
		//
	}
	/*
	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			if(Input.GetKeyUp(KeyCode.Space)){
				print(Input.inputString);
				contact = true;
				if(!qm.quests_completed[quest_id] && interact){
					//if this is a start quest trigger AND the quest is not active
					if(start && !qm.quests[quest_id].gameObject.activeSelf){
						qm.quests[quest_id].start_quest();
					}
					if(end && qm.quests[quest_id].gameObject.activeSelf){
						qm.quests[quest_id].end_quest();
					}
				}
			}
		}
	}*/


	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			if(interact){
				if(Input.GetKeyUp(KeyCode.Space)){
					if(end && !qm.quests_completed[end_quest_id] && qm.quests[end_quest_id].gameObject.activeSelf){
						//if this is a end quest trigger
						//if the quest is not completed
						//if the quest is active
						qm.quests[end_quest_id].end_quest();
					}
					if(start && !qm.quests_completed[start_quest_id] && !qm.quests[start_quest_id].gameObject.activeSelf){
						//if this is a start quest trigger
						//if the quest is not completed
						//if the quest is not active
						qm.quests[start_quest_id].start_quest();
					}
				}
			}else{
				if(end && !qm.quests_completed[end_quest_id] && qm.quests[end_quest_id].gameObject.activeSelf){
					//if this is a end quest trigger
					//if the quest is not completed
					//if the quest is active
					qm.quests[end_quest_id].end_quest();
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
