using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour {
	//globals
	public float move_speed;
	private float current_move_speed;
	private bool moving;

	public Vector2 last_move;
	private Vector2 move_input;

	private Animator anim;

	private Rigidbody2D rigid_body;

	private static bool exists;

	private bool attacking;
	public float attack_time;
	private float attack_time_counter;

	public string start_point;

	public bool can_move;

	private sfx_manager sfx_man;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		anim = GetComponent<Animator>();
		rigid_body = GetComponent<Rigidbody2D>();
		sfx_man = FindObjectOfType<sfx_manager>();

		can_move = true;

		//avoid duplicates
		if(!exists){
			exists = true;
			DontDestroyOnLoad(transform.gameObject);
		}else{
			Destroy(gameObject);
		}

		last_move = new Vector2(0, -1f);
	}

	/* Update method
	 * called once per frame
	 */
	void Update(){
		//moving check set
		moving = false;

		if(!can_move){
			rigid_body.velocity = Vector2.zero;
			return;
		}

		if(!attacking){
			//get input
			move_input = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;

			//respond to input
			if(move_input != Vector2.zero){
				rigid_body.velocity = new Vector2 (move_input.x * move_speed, move_input.y * move_speed);
				moving = true;
				last_move = move_input;
			}else{
				rigid_body.velocity = Vector2.zero;
			}

			//check for attack keypress
			if(Input.GetKeyDown (KeyCode.J)){
				attack_time_counter = attack_time;
				attacking = true;
				rigid_body.velocity = Vector2.zero;
				anim.SetBool("attack", attacking);

				//sfx
				sfx_man.player_attack.Play();
			}
		}

		//decrement counter or end attack
		if (attack_time_counter > 0) {
			attack_time_counter -= Time.deltaTime;
		} else if (attack_time_counter <= 0){
			attacking = false;
			anim.SetBool("attack", attacking);
		}

		//set animator values
		anim.SetBool ("moving", moving);
		anim.SetFloat ("move_x", Input.GetAxisRaw ("Horizontal"));
		anim.SetFloat ("move_y", Input.GetAxisRaw ("Vertical"));
		anim.SetFloat ("last_move_x", last_move.x);
		anim.SetFloat ("last_move_y", last_move.y);
	}
}
