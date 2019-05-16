using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class save{
    //scene
    public string map;
    public string[] coords;

    //enemies
    public string[][] enemies;
    //name-active-health-coordx-coordy

    //player stats
    public string name;
    public int health;
    public int level;
    public int xp;
    public int potions;
    public int gold;

    //player equipment
    public string[] equipment;

    //player quests
    public int current_quest;
    public int[] quests_completed;
    public int[][] misc_quests;

    //requirements
	public int current_count;

    //loot areas & keys
    public int[] drops;
    public int[] keys;
}
