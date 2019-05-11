using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause_controller : MonoBehaviour{
    //globals
	private player_controller the_player;
    public bool stopped;
    private dialogue_manager dm;

    void Start() {
        stopped = false;
        the_player = FindObjectOfType<player_controller>();
        dm = FindObjectOfType<dialogue_manager>();
    }
    
    void Update() {
        //check for escape char
        if(!dm.active){
            if(!stopped && Input.GetKeyUp(KeyCode.Escape)){
                stopped = true;
                stop_movement();
            }else if(stopped && Input.GetKeyUp(KeyCode.Escape)){
                stopped = false;
                start_movement();
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
