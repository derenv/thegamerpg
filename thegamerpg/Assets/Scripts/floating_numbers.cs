using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class floating_numbers : MonoBehaviour {
	//globals
	public float move_speed;
	public int damage_number;
	public Text display_number;

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
		display_number.text = ""+damage_number;
		transform.position = new Vector3(transform.position.x,transform.position.y + (move_speed * Time.deltaTime),transform.position.z);
	}
}
