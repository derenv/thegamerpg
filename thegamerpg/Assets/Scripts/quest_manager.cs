using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quest_manager : MonoBehaviour {
	//globals
	public quest_object[] quests;
	public bool[] quests_completed;
	public dialogue_manager dm;

	public string item_collected;
	public string enemy_killed;

	/* Start method
	 * called on initialization
	 */
	void Start () {
		quests_completed = new bool[quests.Length];
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update () {
		//
	}

	public void show_quest_dialogue(string[] quest_text){
		//prepare
		//dm.lines = quest_text;
		//dm.current_line = -1;

		//use
		//dm.show_dialogue();
		dm.show_dialogue(quest_text);
	}
}
