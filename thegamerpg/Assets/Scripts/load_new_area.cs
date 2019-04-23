using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_new_area : MonoBehaviour {
	//globals
	public string level_to_load;
	public string exit_point;

	private player_controller player;

	public bool requires_quest;
	public int quest_required;
	//public int status_required;   //change qm.quests_completed[]<bool> to qm.quests_status<int>[] for incomplete quests
	private quest_manager qm;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		player = FindObjectOfType<player_controller>();
		qm = FindObjectOfType<quest_manager>();
	}

	/* Update method
	 * called once per frame
	 */
	void Update(){
		//
	}

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			if(requires_quest){
				if(qm.quests_completed[quest_required]){
					SceneManager.LoadScene(level_to_load);
					player.start_point = exit_point;
				}
			}else{
				SceneManager.LoadScene(level_to_load);
				player.start_point = exit_point;
			}
		}
	}
}
