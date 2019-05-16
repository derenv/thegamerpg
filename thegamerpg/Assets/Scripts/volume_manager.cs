using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volume_manager : MonoBehaviour {
	//globals
	public volume_controller[] vc_objects;

	public float[] current_volume_level;
	public float[] max_volume_level;
 	
	/* Start method
	 * called on initialization
	 */
	void Start () {
		//get all audio objects
		vc_objects = FindObjectsOfType<volume_controller>();

		if (!PlayerPrefs.HasKey("music_volume")){
			//set and save default music preference
            PlayerPrefs.SetFloat("music_volume", 1);
            PlayerPrefs.Save();
		}else{
			current_volume_level[0] = PlayerPrefs.GetFloat("music_volume");
		}
		if (!PlayerPrefs.HasKey("sfx_volume")){
			//set and save default music preference
            PlayerPrefs.SetFloat("sfx_volume", 1);
            PlayerPrefs.Save();
		}else{
			current_volume_level[1] = PlayerPrefs.GetFloat("sfx_volume");
		}


		//correct max levels
		if (current_volume_level[0] > max_volume_level[0]) {
			current_volume_level[0] = max_volume_level[0];
		}
		if (current_volume_level[1] > max_volume_level[1]) {
			current_volume_level[1] = max_volume_level[1];
		}

		//correct levels on each audio object
		foreach(volume_controller vc in vc_objects) {
			if(vc.type.Equals("music")){
        		//if music preference does not exist
				vc.set_audio_level(current_volume_level[0]);
				//else deal with music preference
			}else if(vc.type.Equals("sfx")){
				vc.set_audio_level(current_volume_level[1]);
			}
		}
	}

	void Update() {
		//set and save default music & sfx volume preferences
		PlayerPrefs.SetFloat("music_volume", current_volume_level[0]);
		PlayerPrefs.Save();
		PlayerPrefs.SetFloat("sfx_volume", current_volume_level[1]);
		PlayerPrefs.Save();

		//correct levels on each audio object
		foreach(volume_controller vc in vc_objects) {
			if(vc.type.Equals("music")){
				vc.set_audio_level(current_volume_level[0]);
			}else if(vc.type.Equals("sfx")){
				vc.set_audio_level(current_volume_level[1]);
			}
		}
	}
	
	/* Update method
	 * called once per frame
	 */
	public void update_level (int type, float new_level) {
		//correct levels on each audio object
		current_volume_level[type] = new_level;
	}
}
