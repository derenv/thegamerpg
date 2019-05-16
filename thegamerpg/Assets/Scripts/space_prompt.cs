using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space_prompt : MonoBehaviour{
    void Start() {
        gameObject.SetActive(false);
    }
    public void toggle_prompt(bool value){
        gameObject.SetActive(value);
    }
}
