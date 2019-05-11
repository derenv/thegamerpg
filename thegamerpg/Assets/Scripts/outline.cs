using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class outline : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
	//globals
    private Text text;

    /* Start method
	 * called on initialization
	 */
    void Start(){
        text = GetComponent<Text>();
        text.color = Color.black;
    }

    /* 
     * Called when the mouse enters the GUIElement or Collider.
	 */
    public void OnPointerEnter(PointerEventData eventData){
        text.color = Color.red;
    }

    /* 
     * Called when the mouse leaves the GUIElement or Collider.
	 */
    public void OnPointerExit(PointerEventData eventData){
        text.color = Color.black;
    }
}
