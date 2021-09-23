using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public string title;
    public string description;
    public List<QuestGoal> questGoals;
    public QuestStyle questStyle;
    public bool completed;

    List<QuestGoal> goalsToComplete = new List<QuestGoal>(); //jde to tohle jinak??

    public void Progress(GoalType _goalType, int _itemID) 
    {
        if (questStyle == QuestStyle.AfterEachOther)
        {
            if (_goalType == questGoals[0].goalType && _itemID == questGoals[0].itemID)
            {
                questGoals[0].AddPoint();
                if (questGoals[0].completed)
                    goalsToComplete.Add(questGoals[0]);
            }
        }
        if (questStyle == QuestStyle.AtTheSameTime)
        {
            foreach (QuestGoal goal in questGoals)
            {                
                if (_goalType == goal.goalType && _itemID == goal.itemID)
                {
                    goal.AddPoint();
                    if (goal.completed)
                        goalsToComplete.Add(goal);
                }
            }
        }

        foreach (QuestGoal goal in goalsToComplete) //jde to tohle jinak??
        {
            GoalComplete(goal);
        }
    }

    public void GoalComplete(QuestGoal questGoal)
    {
        //ukazat nejaky ui nebo zmenit ui treba to bude ukazovat 0/5 (nula z pìti) a tady se to zmeni na complete
        // pøehrát nejaký zvuky atd... (co všechno se má na konci dít tam jeste doprogramuju)
        questGoals.Remove(questGoal);
        if (questGoals.Count == 0)
        {
            completed = true;
        }
    }
}

public enum QuestStyle
{
    AfterEachOther,
    AtTheSameTime
}


