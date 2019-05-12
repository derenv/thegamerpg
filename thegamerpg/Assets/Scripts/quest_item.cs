using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_item : MonoBehaviour {
	//globals
	public bool start;
	public int start_quest_id;
	public int quest_id;
	private quest_manager qm;
	public string item_name;
	private bool ended;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		qm = FindObjectOfType<quest_manager>();

		if(qm.quests_completed[quest_id]){
			if(start){
				if(qm.quests_completed[start_quest_id]){
					gameObject.SetActive(false);
				}
			}else{
				gameObject.SetActive(false);
			}
		}
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update(){
		//add start text to dialogue queue
		if(ended){
			if(start && !qm.quests_completed[start_quest_id] && qm.quests_completed[quest_id]){
				qm.quests[start_quest_id].start_quest();

				gameObject.SetActive(false);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			if(!qm.quests_completed[quest_id] && qm.quests[quest_id].gameObject.activeSelf){
				qm.item_collected = item_name;

				//activate any loot areas
				loot_area[] areas = GetComponentsInChildren<loot_area>(true);
				foreach(loot_area x in areas){
					if(x.quest_required == quest_id){
						x.gameObject.SetActive(true);
					}
				}

				//allow loot box to show
				gameObject.transform.DetachChildren();
				
				//for end/start quest ordering in dialogue queue
				ended = true;
			}
		}
	}
}
