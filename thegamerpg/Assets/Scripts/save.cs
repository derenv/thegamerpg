using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class save{
    //scene
    public string map;
    public string[] coords;

    //enemies
    //name-health-coordx-coordy

    //player stats
    public string name;
    public int health;
    public int level;
    public int xp;
    public int potions;
    public int gold;

    //player equipment
    public string[] armour;
    public string weapon;
    public string shield;

    //player quests
    public int current_quest;
    public int enemies;
    
	private int current_kills;
    public int[] quests_completed;//cannot store bools, instead 1/0s
    public int[] drops;
}
