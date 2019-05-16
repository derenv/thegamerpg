using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogue_holder : MonoBehaviour {
	//globals
	public string[] dialogue;
	private dialogue_manager dm;
	private UI_manager ui;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		dm = FindObjectOfType<dialogue_manager>();
		ui = FindObjectOfType<UI_manager>();
	}

	private void OnTriggerExit2D(Collider2D other) {
		ui.toggle_prompt(false);
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			if(Input.GetKeyUp(KeyCode.Space)){
				if(!dm.contains(dialogue)){
					dm.show_dialogue(dialogue);
				}
			}else{
				//show spacebar prompt
				ui.toggle_prompt(true);
			}
		}
	}
}
