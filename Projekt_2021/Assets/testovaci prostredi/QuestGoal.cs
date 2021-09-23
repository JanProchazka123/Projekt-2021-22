using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public string goalDescription; // nìco kratkýho co potom bude vypsaný pred tím kolik toho máš napø.: "(goalDescription)zabij 3 rakety (currentValue)0/(goalValue)3 "
    public GoalType goalType;
    public int itemID; //kazej enemy/reachtrigger/vecnasebrani budou mit vlastni id a to se bude opakovat napr. chodiciStøíleè bude mit ID 1 a chodiciSekáè bude mit id 2 a merunkaKSebrani bude mit id 1 a jabloKSebrani bude mit ID 2
    public int currentValue = 0;
    public int goalValue;
    public bool completed;

    public void AddPoint()
    {
        currentValue++;
        if (currentValue >= goalValue)
        {
            completed = true;
        }
        else
        {
            completed = false;
        }
    }
}

public enum GoalType
{
    Reach,
    Kill,
    Gather,
    Talk
}