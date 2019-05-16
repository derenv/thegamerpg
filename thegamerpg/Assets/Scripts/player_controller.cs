using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

	private bool blocking;

	private bool healing;
	public float heal_time;
	private float heal_time_counter;

	public string start_point;

	public bool can_move;

	private sfx_manager sfx_man;

	private Vector2 pause_velocity;
	private player_health_manager phm;
	private player_stats stats;
	public int block_modifier;

	//loot areas & keys
	public bool[] drops;
	public bool[] keys;

	//test
	private bool found;
	//====

	/* Start method
	 * called on initialization
	 */
	void Start(){
		anim = GetComponent<Animator>();
		rigid_body = GetComponent<Rigidbody2D>();
		sfx_man = FindObjectOfType<sfx_manager>();
		phm = GetComponent<player_health_manager>();

		can_move = true;

		//avoid duplicates
		if(!exists){
			exists = true;
			DontDestroyOnLoad(transform.gameObject);
		}else{
			Destroy(gameObject);
		}

		pause_velocity = Vector2.zero;
	}

	public void reset(){
		exists = false;
		Destroy(gameObject);
	}

	/* Update method
	 * called once per frame
	 */
	void Update(){
		//cleanup
		if(SceneManager.GetActiveScene().name.Equals("main_menu")){
			can_move = false;
		}
		if(stats == null){
			stats = FindObjectOfType<player_stats>();
		}

		//moving check set
		moving = false;

		//check if game stopped
		if(!can_move){
			pause_velocity = rigid_body.velocity;
			rigid_body.velocity = Vector2.zero;
			return;
		}else if(can_move && pause_velocity != Vector2.zero){
			rigid_body.velocity = pause_velocity;
			pause_velocity = Vector2.zero;
		}

		if(!attacking && !blocking && !healing){
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
			if(Input.GetKeyDown(KeyCode.J)){
				//if the player has a weapon
				if(transform.Find("Weapon").gameObject.activeSelf){
					//timer
					attack_time_counter = attack_time;
					attacking = true;

					//rigid body
					rigid_body.velocity = Vector2.zero;

					//animator
					anim.SetBool("attack", attacking);

					//sfx
					sfx_man.player_attack.Play();
				}
			}
			if(Input.GetKeyDown(KeyCode.B)){
				//if the player has a shield
				if(transform.Find("Shield").gameObject.activeSelf){
					//rigid body
					rigid_body.velocity = Vector2.zero;

					//animator
					blocking = true;
					anim.SetBool("block", blocking);

					//no sfx here, occurs when hit from enemy occurs
					if(stats.defence_levels[stats.current_level] == 0){
						stats.current_defence = block_modifier;
					}else{
						stats.current_defence = stats.defence_levels[stats.current_level] * block_modifier;
					}
				}
			}
			if(Input.GetKeyDown(KeyCode.H)){
				//if the player has any potions
				if(phm.potion_amount() > 0){
					//timer
					heal_time_counter = heal_time;
					healing = true;

					//rigid body
					rigid_body.velocity = Vector2.zero;

					//animator
					anim.SetBool("heal", healing);

					//sfx
					sfx_man.player_heal.Play();

					phm.use_potion();
				}
			}
		}
		
		//decrement counter or end attack
		if (attack_time_counter > 0) {
			attack_time_counter -= Time.deltaTime;
		} else if (attack_time_counter <= 0){
			attacking = false;
			anim.SetBool("attack", attacking);
		}
		if(Input.GetKeyUp(KeyCode.B)){
			//if the player has a shield
			blocking = false;
			anim.SetBool("block", blocking);
			stats.current_defence = stats.defence_levels[stats.current_level];
		}
		//decrement counter or end heal
		if (heal_time_counter > 0) {
			heal_time_counter -= Time.deltaTime;
		} else if (heal_time_counter <= 0){
			healing = false;
			anim.SetBool("heal", healing);
		}

		//set animator values
		anim.SetBool ("moving", moving);
		anim.SetFloat ("move_x", Input.GetAxisRaw ("Horizontal"));
		anim.SetFloat ("move_y", Input.GetAxisRaw ("Vertical"));
		anim.SetFloat ("last_move_x", last_move.x);
		anim.SetFloat ("last_move_y", last_move.y);
	}
}
