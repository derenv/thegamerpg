using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key_lock : MonoBehaviour{
    //globals
    public int key_num;

    public void unlock(){
        gameObject.SetActive(false);

        //find sfx manager and play unlock sound
        //
    }

    void OnTriggerStay2D(Collider2D other){
		if(other.gameObject.name == "Player"){
            //get player and check if they have the key
            player_controller player = other.gameObject.GetComponent<player_controller>();
            if(player.keys[key_num]){
                unlock();
            }
        }
    }
}
