using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_start_location : MonoBehaviour {
	//globals
	private player_controller player;
	private camera_controller player_camera;

	public Vector2 point_direction;

	public string point_name;

	/* Start method
	 * called on initialization
	 */
	void Start () {
		//move player to start position
		player = FindObjectOfType<player_controller>();

		if (player.start_point.Equals(point_name)){
			player.transform.position = transform.position;
			player.last_move = point_direction;

			//move camera to start position, respecting height difference
			player_camera = FindObjectOfType<camera_controller> ();
			player_camera.transform.position = new Vector3 (transform.position.x, transform.position.y, player_camera.transform.position.z);
		}
	}
}
