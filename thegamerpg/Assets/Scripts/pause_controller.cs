using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_controller : MonoBehaviour{
    //globals
	private player_controller the_player;
    public bool stopped;
    private dialogue_manager dm;
    private settings_ui_manager sum;

    void Start() {
        stopped = false;
        the_player = FindObjectOfType<player_controller>();
        dm = FindObjectOfType<dialogue_manager>();
    }
    
    void Update() {
		if(!SceneManager.GetActiveScene().name.Equals("main_menu")){
            if(dm == null){
                dm = FindObjectOfType<dialogue_manager>();
            }
            //check for escape char
            if(!dm.active){
                sum = FindObjectOfType<settings_ui_manager>();
                if(!stopped && Input.GetKeyUp(KeyCode.Escape)){
                    stopped = true;
                    stop_movement();
                    sum.change_button_group(2);
                }else if(stopped && Input.GetKeyUp(KeyCode.Escape)){
                    //paused
                    if(sum.button_group == 2){
                        stopped = false;
                        start_movement();
                        sum.change_button_group(0);
                    }
                }
            }
        }
    }

    public void start_movement(){
		the_player.can_move = true;
		enemy_controller[] enemies = FindObjectsOfType<enemy_controller>();
		villager_movement[] npcs = FindObjectsOfType<villager_movement>();
		foreach(enemy_controller enemy in enemies){
			enemy.can_move = true;
		}
		foreach(villager_movement npc in npcs){
			npc.can_move = true;
		}
	}

	public void stop_movement(){
        //disable player
		the_player.can_move = false;

        //disable boss
        /*???????
        boss_controller[] bosses = FindObjectsOfType<boss_controller>();
        foreach(boss_controller boss in bosses){
			boss.can_move = false;
		}
        */

        //disable enemies
		enemy_controller[] enemies = FindObjectsOfType<enemy_controller>();
		foreach(enemy_controller enemy in enemies){
			enemy.can_move = false;
		}

        //disable npcs
		villager_movement[] npcs = FindObjectsOfType<villager_movement>();
		foreach(villager_movement npc in npcs){
			npc.can_move = false;
		}
	}
}
