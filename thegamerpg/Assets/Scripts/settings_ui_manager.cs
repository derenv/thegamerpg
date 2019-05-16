using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class settings_ui_manager : MonoBehaviour{
    //globals
    public GameObject[] button_groups;
	public Slider music_bar;
	public Slider sfx_bar;
	public Text music_text;
	public Text sfx_text;
    public volume_manager vm;

    public int button_group;
    public Text info_text;
	public GameObject main_camera;
	public GameObject new_canvas;

    void Start(){
        vm = FindObjectOfType<volume_manager>();
		music_bar.maxValue = vm.max_volume_level[0];
        sfx_bar.maxValue = vm.max_volume_level[1];
		music_bar.value = vm.current_volume_level[0];
		sfx_bar.value = vm.current_volume_level[1];
        music_text.text = ""+(vm.current_volume_level[0] * 100);
        sfx_text.text = ""+(vm.current_volume_level[1] * 100);
        button_group = 2;
    }

    void Update(){
        if(vm == null){
            vm = FindObjectOfType<volume_manager>();
        }
		vm.update_level(0,music_bar.value);
		vm.update_level(1,sfx_bar.value);
        music_text.text = ""+(vm.current_volume_level[0] * 100);
        sfx_text.text = ""+(vm.current_volume_level[1] * 100);
    }

    public void change_button_group(int new_button_group){
        if(new_button_group < button_groups.Length){
            button_group = new_button_group;
        }

        if(new_button_group == -1){
            for(int i=0;i<button_groups.Length;i++){
                pause_controller x = FindObjectOfType<pause_controller>();
                x.stopped = false;
                x.start_movement();
                change_button_group(0);
            }
        }else{
            for(int i=0;i<button_groups.Length;i++){
                if(new_button_group == i){
                    button_groups[i].SetActive(true);
                }else{
                    button_groups[i].SetActive(false);
                }
            }
        }
    }

    public void button_click(string button){
        if(button.Equals("New Game")){
            //MAIN
            SceneManager.LoadScene("forest1");
            FindObjectOfType<player_controller>().can_move = true;
        }else if(button.Equals("Save Game")){
            //ESC
            Debug.Log("saving game..");
            change_button_group(3);
        }else if(button.Equals("Save 1") || button.Equals("Save 2") || button.Equals("Save 3")){
            string slot = button.Split(' ')[1];
            if(button_group == 3){
                //save
                SaveGame(int.Parse(slot),FindObjectOfType<player_controller>());
                change_button_group(-1);
                info_text.gameObject.SetActive(false);
            }else if(button_group == 4){
                //load
                LoadGame(int.Parse(slot));
                change_button_group(-1);
                info_text.gameObject.SetActive(false);
            }
        }else if(button.Equals("Load Game")){
            //MAIN+ESC
            change_button_group(4);
        }else if(button.Equals("Settings")){
            //MAIN+ESC
            change_button_group(1);
        }else if(button.Equals("Main Menu")){
            //MAIN+ESC
            change_button_group(2);
        }else if(button.Equals("Exit")){
            //MAIN+ESC
            if(button_group == 2){
                if(SceneManager.GetActiveScene().name.Equals("main_menu")){
                    //quit program
                    Application.Quit();
                }else{
                    //destroy all persistent objects
                    FindObjectOfType<player_controller>().reset();
                    FindObjectOfType<camera_controller>().reset();
                    FindObjectOfType<UI_manager>().reset();

                    //return to main menu
                    SceneManager.LoadScene("main_menu");
                }
            }else{
                //return to pause/main menu
                change_button_group(2);
                info_text.gameObject.SetActive(false);
            }
        }
    }

    //===============
    private void UseSaveGameObject(save save_to_load){
        //get player and update
        player_controller the_player = FindObjectOfType<player_controller>();

        //change scene & move player
        SceneManager.LoadScene(save_to_load.map);
        the_player.gameObject.transform.SetPositionAndRotation(new Vector3(float.Parse(save_to_load.coords[0]),float.Parse(save_to_load.coords[1])),new Quaternion());

        //check & ensure canvas exists
        player_stats the_stats = FindObjectOfType<player_stats>();
        if(the_stats == null){
            var clone = Instantiate(new_canvas,Vector3.zero,Quaternion.Euler(Vector3.zero));
            the_stats = clone.GetComponent<player_stats>();
        }

        //enemies
        enemy_controller[] enemies = FindObjectsOfType<enemy_controller>();
        //deactivate all initially
        foreach(enemy_controller x in enemies){
            x.gameObject.SetActive(false);
        }
        //activate each noted as active
        //for each in scene
        foreach(enemy_controller x in enemies){
            //find in save
            for(int i=0;i<save_to_load.enemies.Length;i++){
                //if in save
                if(x.gameObject.name.Equals(save_to_load.enemies[i][0])){
                    //set active & set stats
                    x.gameObject.SetActive(true);
                    x.GetComponent<enemy_health_manager>().current_health = int.Parse(save_to_load.enemies[i][2]);
                    x.gameObject.transform.SetPositionAndRotation(new Vector3(float.Parse(save_to_load.enemies[i][3]),float.Parse(save_to_load.enemies[i][4])),new Quaternion());
                    
                    //end loop prematurely
                    continue;
                }
            }
        }
        //destroy inactive enemies
        foreach(enemy_controller x in enemies){
            if(!x.gameObject.activeSelf){
                //
                Destroy(x.gameObject);
            }
        }

        //add player data
        the_player.name = save_to_load.name;
        the_player.gameObject.GetComponent<player_health_manager>().player_current_health = save_to_load.health;
        the_player.gameObject.GetComponent<player_health_manager>().add_potion(save_to_load.potions);
        the_stats.gold = save_to_load.gold;
        the_stats.current_xp = save_to_load.xp;

        //add player equipment
        for(int i=0;i<save_to_load.equipment.Length;i++){
            pickup_obj[] player_objects = the_player.GetComponentsInChildren<pickup_obj>(true);
            pickup_obj[] current_objects = the_player.GetComponentsInChildren<pickup_obj>();
            foreach(pickup_obj x in player_objects){
                if(x.gameObject.name.Equals(save_to_load.equipment[i])){
                    //catch first weapon and first shield
                    if(!x.transform.parent.gameObject.activeSelf && (x.transform.parent.gameObject.name.Equals("Weapon") || x.transform.parent.gameObject.name.Equals("Shield"))){
                        x.transform.parent.gameObject.SetActive(true);
                    }

                    //get all active in parent & deactivate
                    pickup_obj xx = x.transform.parent.GetComponentInChildren<pickup_obj>();
                    if(xx != null){
                        xx.gameObject.SetActive(false);
                    }

                    //activate new item
                    x.gameObject.SetActive(true);
                    
                    //end loop prematurely
                    continue;
                }
            }
        }

        //add quest data (MUST BE BEFORE SCENE CHANGE FOR QUEST MANAGER TO EXIST)
        quest_manager qm = FindObjectOfType<quest_manager>();
        qm.reset();
        //current quest
        qm.quests[save_to_load.current_quest].gameObject.SetActive(true);
        qm.quests[save_to_load.current_quest].current_kill_amount = save_to_load.current_count;

        //completed quests
        for(int i=0;i<qm.quests.Length;i++){
            //check which completed
            if(save_to_load.quests_completed[i] == 1){
                qm.quests_completed[i] = true;
            }
        }

        //misc quests
        if(save_to_load.misc_quests[0][0] == 1){
            qm.quests[30].gameObject.SetActive(true);
        }else if(save_to_load.misc_quests[0][1] == 1){
            qm.quests[31].gameObject.SetActive(true);
        }
        if(save_to_load.misc_quests[1][0] == 1){
            qm.quests[32].gameObject.SetActive(true);
        }else if(save_to_load.misc_quests[1][1] == 1){
            qm.quests[33].gameObject.SetActive(true);
        }

        //loot areas + keys
        for(int i=0;i<the_player.drops.Length;i++){
            if(save_to_load.drops[i] == 1){
                the_player.drops[i] = true;
            }
        }
        for(int i=0;i<the_player.keys.Length;i++){
            if(save_to_load.keys[i] == 1){
                the_player.keys[i] = true;
            }
        }

        //check if camera exists
        camera_controller camera = FindObjectOfType<camera_controller>();
        if(camera == null){
            Vector3 camera_vec = new Vector3(the_player.transform.position.z,the_player.transform.position.y,-5);
            var clone = Instantiate(main_camera,camera_vec,Quaternion.Euler(Vector3.zero));
            clone.transform.parent = the_player.gameObject.transform;
		    clone.GetComponent<camera_controller>().follow_target = the_player.gameObject;
            GameObject new_bounds = GameObject.Find("Bounds");
            if(new_bounds != null){
                new_bounds.GetComponent<bounds>().find_camera();
            }
        }
    }
    private save CreateSaveGameObject(player_controller the_player){
        save save = new save();

        //scene
        save.map = SceneManager.GetActiveScene().name;
        save.coords = new string[2];
        save.coords[0] = the_player.gameObject.transform.position.x.ToString();
        save.coords[1] = the_player.gameObject.transform.position.y.ToString();

        //enemies
        enemy_controller[] enemies = FindObjectsOfType<enemy_controller>();
        save.enemies = new string[enemies.Length][];
        for(int i=0;i<save.enemies.Length;i++){
            //
            save.enemies[i] = new string[5];
            save.enemies[i][0] = enemies[i].gameObject.name;//name
            save.enemies[i][1] = enemies[i].gameObject.activeSelf.ToString();//active
            save.enemies[i][2] = enemies[i].GetComponent<enemy_health_manager>().current_health.ToString();//health
            save.enemies[i][3] = enemies[i].gameObject.transform.position.x.ToString();//coord x
            save.enemies[i][4] = enemies[i].gameObject.transform.position.y.ToString();//coord y
        }

        //stats
        save.name = the_player.name;
        save.health = the_player.gameObject.GetComponent<player_health_manager>().player_current_health;
        save.xp = FindObjectOfType<player_stats>().current_xp;
        save.potions = the_player.gameObject.GetComponent<player_health_manager>().potion_amount();
        save.gold = FindObjectOfType<player_stats>().gold;

        //Equipment
        pickup_obj[] equipment = the_player.GetComponentsInChildren<pickup_obj>();
        save.equipment = new string[equipment.Length];
        for(int i=0;i<equipment.Length;i++){
            save.equipment[i] = equipment[i].name;
        }

        //quests
        quest_manager qm = FindObjectOfType<quest_manager>();
        save.current_quest = 0;
        save.quests_completed = new int[qm.quests_completed.Length];
        for(int i=0;i<qm.quests.Length;i++){
            //check all non-misc quests for current active
            if(qm.quests[i] != null && qm.quests[i].gameObject.activeSelf && i < 29 ){
                save.current_quest = i;
            }
            //check which completed
            if(qm.quests_completed[i]){
                save.quests_completed[i] = 1;
            }else{
                save.quests_completed[i] = 0;
            }
        }
        //quests requirements
        save.current_count = qm.quests[save.current_quest].current_kill_amount;

        //misc quests
        save.misc_quests = new int[2][];
        save.misc_quests[0] = new int[2];
        save.misc_quests[1] = new int[2];
        if(qm.quests[30].gameObject.activeSelf){
            save.misc_quests[0][0] = 1;
        }else if(qm.quests[31].gameObject.activeSelf){
            save.misc_quests[0][1] = 1;
        }
        if(qm.quests[32].gameObject.activeSelf){
            save.misc_quests[1][0] = 1;
        }else if(qm.quests[33].gameObject.activeSelf){
            save.misc_quests[1][1] = 1;
        }

        //loot areas + keys
        save.drops = new int[the_player.drops.Length];
        for(int i=0;i<the_player.drops.Length;i++){
            if(the_player.drops[i]){
                save.drops[i] = 1;
            }else{
                save.drops[i] = 0;
            }
        }
        save.keys = new int[the_player.keys.Length];
        for(int i=0;i<the_player.keys.Length;i++){
            if(the_player.keys[i]){
                save.keys[i] = 1;
            }else{
                save.keys[i] = 0;
            }
        }

        return save;
    }
    public void SaveGame(int slot, player_controller the_player){
        //create object
        save save = CreateSaveGameObject(the_player);
        
        //convert to binary object and write to file
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave"+slot+".save");
        bf.Serialize(file, save);
        file.Close();
        
        //show message
        info_text.text = "..saving in slot "+slot+"..";
        info_text.gameObject.SetActive(true);
    }
    public void LoadGame(int slot){
        //if save exists
        if (File.Exists(Application.persistentDataPath + "/gamesave"+slot+".save")){

            //fetch binary file and deserialize
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave"+slot+".save", FileMode.Open);
            save save = (save)bf.Deserialize(file);
            file.Close();

            //use data (construct player?)
            UseSaveGameObject(save);

            //show message
            info_text.text = "..loading from slot "+slot+"..";
            info_text.gameObject.SetActive(true);
        }else{
            info_text.text = "No game saved in slot "+slot+"!";
            info_text.gameObject.SetActive(true);
        }
    }
}
