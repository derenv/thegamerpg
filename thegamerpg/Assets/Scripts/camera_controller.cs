using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour {
	//globals
	public GameObject follow_target;
	private Vector3 target_position;
	public float move_speed;

	private static bool exists;

	public BoxCollider2D bounds_box;
	private Vector3 min_bounds;
	private Vector3 max_bounds;

	private Camera the_camera;
	private float half_height;
	private float half_width;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		//avoid duplicates
		if(!exists){
			exists = true;
			DontDestroyOnLoad(transform.gameObject);
		}else{
			Destroy(gameObject);
		}

		min_bounds = bounds_box.bounds.min;
		max_bounds = bounds_box.bounds.max;

		the_camera = GetComponent<Camera>();
		half_height = the_camera.orthographicSize;
		half_width = half_height * Screen.width / Screen.height;
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update(){
		target_position = new Vector3(follow_target.transform.position.x,follow_target.transform.position.y,transform.position.z);
		transform.position = Vector3.Lerp(transform.position,target_position,move_speed*Time.deltaTime);

		if(bounds_box != null){
			float clamped_x = Mathf.Clamp(transform.position.x, min_bounds.x + half_width, max_bounds.x - half_width);
			float clamped_y = Mathf.Clamp(transform.position.y, min_bounds.y + half_height, max_bounds.y - half_height);
			transform.position = new Vector3(clamped_x, clamped_y, transform.position.z);
		}
	}

	public void set_bounds(BoxCollider2D new_bounds){
		bounds_box = new_bounds;

		min_bounds = bounds_box.bounds.min;
		max_bounds = bounds_box.bounds.max;
	}
}
