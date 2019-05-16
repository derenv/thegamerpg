using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_controller : MonoBehaviour{
	//globals
	private static bool ac_exists;

    /* Start method
	 * called on initialization
	 */
    void Start(){
		//avoid duplicates
        if(!ac_exists){
			ac_exists = true;
			DontDestroyOnLoad(transform.gameObject);
		}else{
			Destroy(gameObject);
		}
    }
}
