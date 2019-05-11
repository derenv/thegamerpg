using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogue_manager : MonoBehaviour {
	//globals
	public GameObject d_box;
	public Text d_text;

	public bool active;

	public string[] lines;
	public int current_line;

	public List<string[]> dialogues;
	public int current_array;

	private pause_controller pause;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		//the_player = FindObjectOfType<player_controller>();
		dialogues = new List<string[]>();
		pause = FindObjectOfType<pause_controller>();
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update(){
		if(active && Input.GetKeyUp(KeyCode.Space)){
			current_line++;
		}

		if (current_line >= lines.Length) {
			if(dialogues.Count > current_array) {
				//set to next list
				lines = dialogues[current_array];

				//increment array pointer
				current_array++;
			} else {
				//deactivate text box
				d_box.SetActive (false);
				active = false;
				if(!pause.stopped){
					pause.start_movement();
				}

				//clear lines fetched current array
				lines = new string[0];

				//clear all arrays
				dialogues.Clear();
				dialogues.TrimExcess();

				//reset array pointer
				current_array = 0;
			}
			//reset line pointer
			current_line = 0;
		}

		if(current_line < lines.Length && active){
			d_text.text = lines[current_line];
		}
	}

	public void show_box(string input){
		d_box.SetActive(true);
		active = true;
		pause.stop_movement();

		d_text.text = input;
	}

	public void show_dialogue(){
		d_box.SetActive(true);
		active = true;
		pause.stop_movement();
	}

	public void show_dialogue(string[] text){
		//prepare
		if(active){
			//add array to list of current dialogues
			dialogues.Add(text);
		}else{
			//reset and add new
			//clear arrays
			dialogues.Clear();
			dialogues.TrimExcess ();

			//add new text
			dialogues.Add(text);

			//reset line & array counters
			current_array = dialogues.IndexOf(text);
			current_line = 0;

			//activate dialogue box
			d_box.SetActive(true);
			active = true;

			//restrict player/enemy/npc/boss movement
			pause.stop_movement();
		}
	}
}
