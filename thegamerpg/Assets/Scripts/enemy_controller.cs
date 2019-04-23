using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemy_controller : MonoBehaviour {
	//globals
	public float move_speed;
	public bool moving;

	public float time_between_move;
	private float time_between_move_counter;
	public float time_to_move;
	private float time_to_move_counter;

	private Vector2 last_move;
	private Vector3 move_direction;

	private Animator anim;

	private Rigidbody2D rigid_body;

	public float reload_wait;

	//====================
	public BoxCollider2D walk_zone;
	private Vector2 min_walk_point;
	private Vector2 max_walk_point;
	private bool walk_zone_exists;
	//====================

	/* Start method
	 * called on initialization
	 */
	void Start(){
		//physics
		rigid_body = GetComponent<Rigidbody2D>();

		//animation
		anim = GetComponent<Animator>();
		time_between_move_counter = Random.Range(time_between_move * 0.75f,time_between_move * 1.25f);
		time_to_move_counter = Random.Range(time_to_move * 0.75f,time_to_move * 1.25f);

		//====================
		if(walk_zone != null){
			walk_zone_exists = true;
			min_walk_point = walk_zone.bounds.min;
			max_walk_point = walk_zone.bounds.max;
		}
		//====================
	}

	/* Update method
	 * called once per frame
	 */
	void Update(){
		//check if moving
		if (moving) {
			//decrement counter
			time_to_move_counter -= Time.deltaTime;

			//====================
			if (walk_zone_exists) {
				if (transform.position.y > max_walk_point.y) {
					moving = false;
					move_direction = new Vector3(move_direction.x,Random.Range(-1f,0f) * move_speed);
				}
				if (transform.position.x > max_walk_point.x) {
					moving = false;
					move_direction = new Vector3(Random.Range(-1f,0f) * move_speed,move_direction.y);
				}
				if (transform.position.y < min_walk_point.y) {
					moving = false;
					move_direction = new Vector3(move_direction.x,Random.Range(0f,1f) * move_speed);
				}
				if (transform.position.x < min_walk_point.x) {
					moving = false;
					move_direction = new Vector3(Random.Range(0f,1f) * move_speed,move_direction.y);
				}
			}
			//====================

			//set physics body
			rigid_body.velocity = move_direction;

			//check counter
			if(time_to_move_counter < 0f){
				//end movement
				moving = false;

				//setup move counters
				time_between_move_counter = Random.Range(time_between_move * 0.75f,time_between_move * 1.25f);

				move_direction = Vector3.zero;
			}
		}else{
			//decrement counter
			time_between_move_counter -= Time.deltaTime;

			//reset physics
			rigid_body.velocity = Vector2.zero;

			//check counter
			if(time_between_move_counter < 0f){
				//setup move counters & bools
				moving = true;
				time_to_move_counter = Random.Range(time_to_move * 0.75f,time_to_move * 1.25f);

				//create new physics body
				move_direction = new Vector3(Random.Range(-1f,1f) * move_speed,Random.Range(-1f,1f) * move_speed);

				//tell animator where to face
				last_move = new Vector2(move_direction.x,move_direction.y);
			}
		}
		if (anim != null && anim.isActiveAndEnabled) {
			anim.SetBool ("moving", moving);
			anim.SetFloat ("last_move_x", last_move.x);
			anim.SetFloat ("last_move_y", last_move.y);
			anim.SetFloat ("move_x", move_direction.x);
			anim.SetFloat ("move_y", move_direction.y);
		}
		/*
		if (reloading) {
			reload_wait -= Time.deltaTime;
			if (reload_wait < 0){
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				the_player.SetActive(true);
			}
		}*/
	}

	/* 
	 * called when 2D collision occurs
	 * initiates combat when colliding object is player
	 */
	void OnCollisionEnter2D(Collision2D other){
		/*
		if(other.gameObject.name == "Player"){
			other.gameObject.SetActive(false);
			reloading = true;
			the_player = other.gameObject;
		}*/
	}
}
