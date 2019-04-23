using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class villager_movement : MonoBehaviour {
	//globals
	//movement
	public float move_speed;
	public float walk_time;
	public float wait_time;
	private float wait_counter;
	private float walk_counter;
	public bool walking;
	private int walk_direction;

	//physics
	private Rigidbody2D rigid_body;

	//walk area
	public BoxCollider2D walk_zone;
	private Vector2 min_walk_point;
	private Vector2 max_walk_point;
	private bool walk_zone_exists;

	//dialogue
	public bool can_move;
	private dialogue_manager d_manager;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		d_manager = FindObjectOfType<dialogue_manager>();

		can_move = true;

		rigid_body = GetComponent<Rigidbody2D>();

		wait_counter = wait_time;
		walk_counter = walk_time;

		choose_direction();

		if(walk_zone != null){
			walk_zone_exists = true;
			min_walk_point = walk_zone.bounds.min;
			max_walk_point = walk_zone.bounds.max;
		}
	}
	
	/* Update method
	 * called once per frame
	 */
	void Update(){
		if(!d_manager.active){
			can_move = true;
		}

		if(!can_move){
			rigid_body.velocity = Vector2.zero;
			return;
		}

		if(walking){
			walk_counter -= Time.deltaTime;

			switch (walk_direction) {
				case 0:
					rigid_body.velocity = new Vector2 (0, move_speed);

					if(walk_zone_exists && transform.position.y > max_walk_point.y){
						walking = false;
						wait_counter = wait_time;
					}

					break;
				case 1:
					rigid_body.velocity = new Vector2 (move_speed, 0);

					if (walk_zone_exists && transform.position.x > max_walk_point.x) {
						walking = false;
						wait_counter = wait_time;
					}

					break;
				case 2:
					rigid_body.velocity = new Vector2 (0, -move_speed);

					if (walk_zone_exists && transform.position.y < min_walk_point.y) {
						walking = false;
						wait_counter = wait_time;
					}

					break;
				case 3:
					rigid_body.velocity = new Vector2 (-move_speed, 0);

					if (walk_zone_exists && transform.position.x < min_walk_point.x) {
						walking = false;
						wait_counter = wait_time;
					}

					break;
			}

			if (walk_counter < 0) {
				walking = false;
				wait_counter = wait_time;
			}
		} else {
			wait_counter -= Time.deltaTime;

			rigid_body.velocity = Vector2.zero;

			if (wait_counter < 0) {
				choose_direction ();
			}
		}
	}

	/* 
	 * 
	 */
	public void choose_direction(){
		walk_direction = Random.Range(0, 4);

		walking = true;
		walk_counter = walk_time;
	}
}
