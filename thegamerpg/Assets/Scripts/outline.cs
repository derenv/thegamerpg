using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class outline : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler{
    CanvasGroup alpha_ref;
    public void OnPointerClick(PointerEventData eventData){
        //check which button clicked
        settings_ui_manager sum = FindObjectOfType<settings_ui_manager>();
        if(sum != null){
            string button = eventData.pointerCurrentRaycast.gameObject.name;
            sum.button_click(button);
            alpha_ref.alpha = 1.0f;
        }
    }

    public void OnPointerEnter(PointerEventData eventData){
        //outline visible
        if(eventData.pointerCurrentRaycast.gameObject == gameObject){
            alpha_ref = gameObject.transform.parent.GetComponent<CanvasGroup>();
            alpha_ref.alpha = 0.2f;
        }
    }

    public void OnPointerExit(PointerEventData eventData){
        //outline gone
        alpha_ref.alpha = 1.0f;
    }
}