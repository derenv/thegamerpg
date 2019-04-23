using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_health_manager : MonoBehaviour {
	//globals
	public int player_max_health;
	public int player_current_health;

	public bool flashing;
	public float flash_length;
	private float flash_counter;
	private SpriteRenderer player_sprite;

	private sfx_manager sfx_man;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		player_current_health = player_max_health;

		player_sprite = GetComponent<SpriteRenderer>();

		sfx_man = FindObjectOfType<sfx_manager>();
	}

	/* Update method
	 * called once per frame
	 */
	void Update(){
		if(player_current_health <= 0){
			sfx_man.player_dead.Play();

			gameObject.SetActive(false);
		}

		if(flashing){
			if (flash_counter > flash_length * 0.66f) {
				player_sprite.color = new Color (player_sprite.color.r, player_sprite.color.g, player_sprite.color.b, 0f);
			}else if (flash_counter > flash_length * 0.33f) {
				player_sprite.color = new Color (player_sprite.color.r, player_sprite.color.g, player_sprite.color.b, 1f);
			}else if (flash_counter > 0) {
				player_sprite.color = new Color (player_sprite.color.r, player_sprite.color.g, player_sprite.color.b, 0f);
			}else{
				player_sprite.color = new Color(player_sprite.color.r,player_sprite.color.g,player_sprite.color.b,1f);
				flashing = false;
			}
			flash_counter -= Time.deltaTime;
		}
	}

	/* 
	 * decrement player health by passed int
	 */
	public void hurt_player(int damage){
		player_current_health -= damage;

		flashing = true;
		flash_counter = flash_length;

		sfx_man.player_hurt.Play();
	}

	/* 
	 * 
	 */
	public void set_max_health(){
		player_current_health = player_max_health;
	}
}
