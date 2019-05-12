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
				vc.set_audio_level(current_volume_level[0]);
			}else if(vc.type.Equals("sfx")){
				vc.set_audio_level(current_volume_level[1]);
			}
		}
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update () {
		//
	}
}
