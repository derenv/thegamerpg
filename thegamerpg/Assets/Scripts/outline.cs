using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class outline : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler{
    public void OnPointerClick(PointerEventData eventData){
        //check which button clicked
        if(eventData.pointerCurrentRaycast.gameObject.name.Equals("New Game")){
            //MAIN
            Debug.Log("new game..");
            SceneManager.LoadScene("forest1");
        }else if(eventData.pointerCurrentRaycast.gameObject.name.Equals("Save Game")){
            //ESC
            Debug.Log("saving game..");
        }else if(eventData.pointerCurrentRaycast.gameObject.name.Equals("Load Game")){
            //MAIN+ESC
            Debug.Log("loading game..");
        }else if(eventData.pointerCurrentRaycast.gameObject.name.Equals("Settings")){
            //MAIN+ESC
            Debug.Log("opening settings..");
        }else if(eventData.pointerCurrentRaycast.gameObject.name.Equals("Exit")){
            //MAIN+ESC
            Debug.Log("..Quitting..");
            Application.Quit();
        }
    }

    public void OnPointerEnter(PointerEventData eventData){
        //outline visible
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData){
        //outline gone
        Debug.Log("Mouse Exit");
    }
/* 
    //===============
    private save CreateSaveGameObject(){
        save save = new save();

        //scene
        //

        //enemies
        //

        //stats
        //

        //equipment
        //

        //quests
        //

        return save;
    }
    public void SaveGame(){
        //create object
        save save = CreateSaveGameObject();
        
        //convert to binary object and write to file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
        
        Debug.Log("Game Saved");
    }
    public void LoadGame(){
        //if save exists
        if (File.Exists(Application.persistentDataPath + "/gamesave.save")){

            //fetch binary file and deserialize
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            save save = (save)bf.Deserialize(file);
            file.Close();

            //use data
            //

            Debug.Log("Game Loaded");
        }else{
            Debug.Log("No game saved!");
        }
    }*/
}