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
				if(!dm.contains(dialogue)){
					dm.show_dialogue(dialogue);
				}
			}else{
				//show spacebar prompt
				//
			}
		}
	}
}
