using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volume_manager : MonoBehaviour {
	//globals
	public volume_controller[] vc_objects;

	public float current_volume_level;
	public float max_volume_level = 1.0f;

	/* Start method
	 * called on initialization
	 */
	void Start () {
		vc_objects = FindObjectsOfType<volume_controller>();

		if (current_volume_level > max_volume_level) {
			current_volume_level = max_volume_level;
		}

		for (int i = 0; i<vc_objects.Length; i++) {
			vc_objects [i].set_audio_level(current_volume_level);
		}
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update () {
		
	}
}
