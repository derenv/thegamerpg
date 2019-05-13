using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn : MonoBehaviour{
    //globals
    //

    public void spawn(int delay){
        //
        Debug.Log("respawning in..");
        for(int x=delay;x<0;x--){
            Debug.Log(x);
        }
    }
}
