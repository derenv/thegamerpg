using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controller : MonoBehaviour {
	//globals
	public float move_speed;
	public bool moving;
	public float time_between_move;
	private float time_between_move_counter;
	public float time_to_move;
	private float time_to_move_counter;

	public Vector2 last_move;
	public Vector2 move_direction;

	private Animator anim;

	private Rigidbody2D rigid_body;

	//public float reload_wait;

	public BoxCollider2D walk_zone;
	private Vector2 min_walk_point;
	private Vector2 max_walk_point;
	private bool walk_zone_exists;

	public bool can_move;
	public bool attacking;
	public float attack_time;
	private float attack_time_counter;
	private sfx_manager sfx_man;

    //player
    private player_controller the_player;
    private Transform player_transform;

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

		//audio
		sfx_man = FindObjectOfType<sfx_manager>();

		//movement
		if(walk_zone != null){
			walk_zone_exists = true;
			min_walk_point = walk_zone.bounds.min;
			max_walk_point = walk_zone.bounds.max;
		}

        the_player = FindObjectOfType<player_controller>();
        player_transform = the_player.transform;
	}

	/* Update method
	 * called once per frame
	 */
	void Update(){
		//check if game stopped
		if(!can_move){
			rigid_body.velocity = Vector2.zero;
			return;
		}

		if(walk_zone_exists && (player_transform.position.y < max_walk_point.y) && (player_transform.position.x < max_walk_point.x) && (player_transform.position.y > min_walk_point.y) && (player_transform.position.x > min_walk_point.x)){
			//rigid body
			Vector3 new_pos = Vector3.MoveTowards(transform.position, player_transform.position, move_speed / 100);
			last_move = new_pos - new Vector3(transform.position.x,transform.position.y);
			transform.position = new_pos;
		}else{
			//check if moving
			if (moving) {
				//decrement counter
				time_to_move_counter -= Time.deltaTime;

				//====================
				if (walk_zone_exists) {
					if (transform.position.y > max_walk_point.y) {
						moving = false;
						move_direction = new Vector2(move_direction.x,Random.Range(-1f,0f) * move_speed);
					}
					if (transform.position.x > max_walk_point.x) {
						moving = false;
						move_direction = new Vector2(Random.Range(-1f,0f) * move_speed,move_direction.y);
					}
					if (transform.position.y < min_walk_point.y) {
						moving = false;
						move_direction = new Vector2(move_direction.x,Random.Range(0f,1f) * move_speed);
					}
					if (transform.position.x < min_walk_point.x) {
						moving = false;
						move_direction = new Vector2(Random.Range(0f,1f) * move_speed,move_direction.y);
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

					move_direction = Vector2.zero;
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
					move_direction = new Vector2(Random.Range(-1f,1f) * move_speed,Random.Range(-1f,1f) * move_speed);
					//tell animator where to face
					last_move = new Vector2(move_direction.x,move_direction.y);
				}
			}
		}
		if (anim != null && anim.isActiveAndEnabled) {
			anim.SetBool ("moving", moving);
			anim.SetFloat ("last_move_x", last_move.x);
			anim.SetFloat ("last_move_y", last_move.y);
			anim.SetFloat ("move_x", move_direction.x);
			anim.SetFloat ("move_y", move_direction.y);
		}
		//decrement counter or end attack
		if (attack_time_counter > 0) {
			attack_time_counter -= Time.deltaTime;
		} else if (attack_time_counter <= 0){
			attacking = false;
			anim.SetBool("attack", attacking);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.name == "Player"){
			//timer
			attack_time_counter = attack_time;
			attacking = true;

			//rigid body
			rigid_body.velocity = Vector2.zero;

			//animator
			anim.SetBool("attack", true);

			//sfx
			sfx_man.player_attack.Play();
		}
	}
}
