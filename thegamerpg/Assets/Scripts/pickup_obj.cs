using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_obj : MonoBehaviour{
    public void pickup_item(player_controller the_player){
        if(GetComponent<key>() != null){
            the_player.keys[GetComponent<key>().key_num]=true;
            return;
        }
        //add health potion
        if(gameObject.name.Equals("health_potion")){
            player_health_manager phm = FindObjectOfType<player_health_manager>();
            phm.add_potion(1);
            return;
        }
        if(gameObject.name.Contains("gold")){
            //add gold
            string g_name = gameObject.name;
            player_stats stats = FindObjectOfType<player_stats>();
            if(g_name.Contains("small")){
                stats.gold += 1;
            }else if(g_name.Contains("medium")){
                stats.gold += 5;
            }else if(g_name.Contains("large")){
                stats.gold += 10;
            }else if(g_name.Contains("massive")){
                stats.gold += 50;
            }
            return;
        }

        //otherwise activate correct item(s)
        pickup_obj[] player_objects = the_player.GetComponentsInChildren<pickup_obj>(true);
        pickup_obj[] current_objects = the_player.GetComponentsInChildren<pickup_obj>();
        foreach(pickup_obj x in player_objects){
            if(x.gameObject.name.Equals(gameObject.name)){
                //catch first weapon and first shield
                if(!x.transform.parent.gameObject.activeSelf && (x.transform.parent.gameObject.name.Equals("Weapon") || x.transform.parent.gameObject.name.Equals("Shield"))){
                    x.transform.parent.gameObject.SetActive(true);
                }

                //get all active in parent & deactivate
                pickup_obj xx = x.transform.parent.GetComponentInChildren<pickup_obj>();
                if(xx != null){
                    xx.gameObject.SetActive(false);
                }

                //activate new item
                x.gameObject.SetActive(true);
                
                //end loop prematurely
                continue;
            }
        }
    }
}
