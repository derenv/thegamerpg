using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_item : MonoBehaviour {
	//globals
	public int quest_id;
	private quest_manager qm;
	public string item_name;

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

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			if(!qm.quests_completed[quest_id] && qm.quests[quest_id].gameObject.activeSelf){
				qm.item_collected = item_name;
				gameObject.SetActive(false);
			}
		}
	}
}
