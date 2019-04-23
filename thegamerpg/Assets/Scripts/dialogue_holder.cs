using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogue_holder : MonoBehaviour {
	//globals
	public string[] dialogue;
	private dialogue_manager dm;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		dm = FindObjectOfType<dialogue_manager>();
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update(){
		//
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			if(Input.GetKeyUp(KeyCode.Space)){
				if(!dm.active){
					dm.lines = dialogue;
					dm.current_line = -1;
					dm.show_dialogue();
				}

				if(transform.parent.GetComponent<villager_movement>() != null){
					transform.parent.GetComponent<villager_movement>().can_move = false;
				}
			}
		}
	}
}
