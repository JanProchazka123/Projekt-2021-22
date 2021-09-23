using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public string goalDescription; // n�co kratk�ho co potom bude vypsan� pred t�m kolik toho m� nap�.: "(goalDescription)zabij 3 rakety (currentValue)0/(goalValue)3 "
    public GoalType goalType;
    public int itemID; //kazej enemy/reachtrigger/vecnasebrani budou mit vlastni id a to se bude opakovat napr. chodiciSt��le� bude mit ID 1 a chodiciSek�� bude mit id 2 a merunkaKSebrani bude mit id 1 a jabloKSebrani bude mit ID 2
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