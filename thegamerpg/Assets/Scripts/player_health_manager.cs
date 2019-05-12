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
	private SpriteRenderer[] children_sprites;
	private sfx_manager sfx_man;

	private int potions;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		player_current_health = player_max_health;

		player_sprite = GetComponent<SpriteRenderer>();

		sfx_man = FindObjectOfType<sfx_manager>();

		potions = 0;
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
			children_sprites = GetComponentsInChildren<SpriteRenderer>(true);
			
			if (flash_counter > flash_length * 0.66f) {
				player_sprite.enabled = false;
				foreach(SpriteRenderer x in children_sprites){
					x.enabled = false;
				}
			}else if (flash_counter > flash_length * 0.33f) {
				player_sprite.enabled = true;
				foreach(SpriteRenderer x in children_sprites){
					x.enabled = true;
				}
			}else if (flash_counter > 0) {
				player_sprite.enabled = false;
				foreach(SpriteRenderer x in children_sprites){
					x.enabled = false;
				}
			}else{
				player_sprite.enabled = true;
				foreach(SpriteRenderer x in children_sprites){
					x.enabled = true;
				}
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
	 * increment player health by passed int
	 */
	public void heal_player(int restore){
		player_current_health += restore;

		if(player_current_health > player_max_health){
			player_current_health = player_max_health;
		}
	}

	/* 
	 * 
	 */
	public void set_max_health(){
		player_current_health = player_max_health;
	}


	public void add_potion(){
		potions++;
	}
	public void use_potion(){
		heal_player(50);
		potions--;
	}

	public int potion_amount(){
		return potions;
	}
}
