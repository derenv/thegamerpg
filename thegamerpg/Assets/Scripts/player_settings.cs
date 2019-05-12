using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_settings : MonoBehaviour{
    //globals
    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private AudioSource myaudio;

    public void Awake(){
        //if music preference does not exist
        if (!PlayerPrefs.HasKey("music_toggle")){
            //set and save default music preference
            PlayerPrefs.SetInt("music_toggle", 1);
            toggle.isOn = true;
            myaudio.enabled = true;
            PlayerPrefs.Save ();
        }else{
            //else deal with music preference
            if (PlayerPrefs.GetInt ("music_toggle") == 0){
                myaudio.enabled = false;
                toggle.isOn = false;
            }else{
                myaudio.enabled = true;
                toggle.isOn = true;
            }
        }
        //if sfx preference does not exist
        if (!PlayerPrefs.HasKey("sfx_toggle")){
            //set and save default sfx preference
            PlayerPrefs.SetInt("sfx_toggle", 1);
            toggle.isOn = true;
            myaudio.enabled = true;
            PlayerPrefs.Save ();
        }else{
            //else deal with sfx preference
            if (PlayerPrefs.GetInt ("sfx_toggle") == 0){
                myaudio.enabled = false;
                toggle.isOn = false;
            }else{
                myaudio.enabled = true;
                toggle.isOn = true;
            }
        }
    }

    public void ToggleMusic(){
        if (toggle.isOn){
           PlayerPrefs.SetInt ("music_toggle", 1);
            myaudio.enabled = true;
        }else{
            PlayerPrefs.SetInt ("music_toggle", 0);
            myaudio.enabled = false;
        }
        PlayerPrefs.Save ();
    }
    public void ToggleSfx(){
        if (toggle.isOn){
           PlayerPrefs.SetInt ("sfx_toggle", 1);
            myaudio.enabled = true;
        }else{
            PlayerPrefs.SetInt ("sfx_toggle", 0);
            myaudio.enabled = false;
        }
        PlayerPrefs.Save ();
    }
}
