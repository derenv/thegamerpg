using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_obj : MonoBehaviour{
    public void pickup_item(player_controller the_player){
        //add health potion
        if(gameObject.name.Equals("health_potion")){
            the_player.phm.add_potion();
            return;
        }
        if(gameObject.name.Contains("gold")){
            //add gold instead (DUMMY)
            string g_name = gameObject.name;
            if(g_name.Contains("small")){
                //
            }else if(g_name.Contains("medium")){
                //
            }else if(g_name.Contains("large")){
                //
            }else if(g_name.Contains("massive")){
                //
            }
            return;
        }

        //otherwise activate correct item(s)
        pickup_obj[] player_objects = the_player.GetComponentsInChildren<pickup_obj>(true);
        pickup_obj[] current_objects = the_player.GetComponentsInChildren<pickup_obj>();
        foreach(pickup_obj x in player_objects){
            if(x.gameObject.name.Equals(gameObject.name)){
                //check if parent active
                if(!x.transform.parent.gameObject.activeSelf){
                    //activate new item & parent
                    x.transform.parent.gameObject.SetActive(true);
                    x.gameObject.SetActive(true);
                }else{
                    //deactivate current item
                    pickup_obj xx = x.transform.parent.GetComponentInChildren<pickup_obj>();
                    if(xx != null){
                        xx.gameObject.SetActive(false);
                    }

                    //activate new item
                    x.gameObject.SetActive(true);
                }
                //end loop prematurely
                continue;
            }
        }
    }
}
