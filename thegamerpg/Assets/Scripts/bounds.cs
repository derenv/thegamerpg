using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bounds : MonoBehaviour {
	//globals
	private BoxCollider2D the_bounds;
	private camera_controller the_camera;

	/* Start method
	 * called on initialization
	 */
	void Start () {
		the_bounds = GetComponent<BoxCollider2D>();
		the_camera = GetComponent<camera_controller>();
		if(the_camera != null){
			the_camera.set_bounds (the_bounds);
		}
	}
}
