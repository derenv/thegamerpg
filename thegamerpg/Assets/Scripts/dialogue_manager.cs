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

	private player_controller the_player;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		the_player = FindObjectOfType<player_controller>();
		dialogues = new List<string[]>();
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update(){
		if(active && Input.GetKeyUp(KeyCode.Space)){
			current_line++;
		}

		if (current_line >= lines.Length) {
			Debug.Log ("current array:" + current_array + "/" + dialogues.Count);
			if(dialogues.Count > current_array) {
				Debug.Log ("more arrays exist..");
				//set to next list
				lines = dialogues[current_array];

				//remove old text
				if (dialogues.Count > 1) {
					dialogues.RemoveAt (current_array);
				}

				//set new current 
				current_array++;
			} else {
				Debug.Log ("deactivating dialogue box..");
				d_box.SetActive (false);
				active = false;
				the_player.can_move = true;
			}
			current_line = 0;
		}

		if(current_line < lines.Length && active){
			Debug.Log ("setting active dialogue box line to "+current_line+" from array "+current_array);
			d_text.text = lines[current_line];
		}
	}

	public void show_box(string input){
		d_box.SetActive(true);
		active = true;
		the_player.can_move = false;

		d_text.text = input;
	}

	public void show_dialogue(){
		d_box.SetActive(true);
		active = true;
		the_player.can_move = false;
	}

	public void show_dialogue(string[] text){
		//prepare
		Debug.Log("current list:"+current_array);
		Debug.Log("pre-size:"+dialogues.Count);
		if(active){
			Debug.Log("adding array to active array list");
			dialogues.Add(text);
		}else{
			Debug.Log("adding array to empty array list");
			Debug.Log ("old capacity:" + dialogues.Capacity);
			dialogues.Clear();
			dialogues.TrimExcess ();

			dialogues.Add(text);

			Debug.Log("post-size:"+dialogues.Count);
			Debug.Log("current list:"+current_array);
			Debug.Log("====");
			Debug.Log ("current capacity:" + dialogues.Capacity);
			current_array = dialogues.IndexOf(text);
			Debug.Log("current list:"+current_array);
			current_line = 0;

			//activate dialogue box
			d_box.SetActive(true);
			active = true;

			//restrict player movement
			the_player.can_move = false;
		}
	}
}
