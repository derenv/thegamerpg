using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soldier_movement : MonoBehaviour
{
    //globals
	private Animator anim;

    void Start(){
			anim = GetComponent<Animator>();
    }

    //
    void Update(){
		//tell animator where to face
		if (anim != null && anim.isActiveAndEnabled) {
			anim.SetBool ("raise", false);
			anim.SetBool ("heal", false);
			anim.SetBool ("attack", false);
			anim.SetBool ("block", false);
			anim.SetBool ("moving", false);
			anim.SetFloat ("last_move_x", 0);
			anim.SetFloat ("last_move_y", -1);
			anim.SetFloat ("move_x", 0);
			anim.SetFloat ("move_y", 0);
		}
    }
}
