using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_over_time : MonoBehaviour {
	//globals
	public float time_to_destroy;

	/* Start method
	 * called on initialization
	 */
	void Start(){
		//
	}

	/* Update method
	 * called once per frame
	 */
	void Update(){
		time_to_destroy -= Time.deltaTime;

		if(time_to_destroy <= 0){
			Destroy(gameObject);
		}
	}
}
