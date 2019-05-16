using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loot_area : MonoBehaviour{
    //globals
    private player_controller the_player;
    private quest_manager qm;
    public int drop_number;
    public int quest_required;

    void Start(){
        the_player = FindObjectOfType<player_controller>();
        qm = FindObjectOfType<quest_manager>();

        //if drop not found before
        if(the_player.drops[drop_number]){
            gameObject.SetActive(false);
        }
    }

    void OnTriggerStay2D(Collider2D other){
        if(other.name.Equals("Player")){
            if(qm.quests_completed[quest_required]){
                pickup_obj[] items = GetComponentsInChildren<pickup_obj>(true);
                foreach(pickup_obj item in items){
                    item.pickup_item(the_player);
                }
                the_player.drops[drop_number] = true;
                gameObject.SetActive(false);
            }
        }
    }
}
