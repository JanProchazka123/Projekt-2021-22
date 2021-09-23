    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestingSystem : MonoBehaviour
{
    public static Dictionary<string, Quest> playerQuests;

    //jde to tohle jinak??
    static List<string> questsToRemove;

    void Start()
    {
        playerQuests = new Dictionary<string, Quest>();
        questsToRemove = new List<string>();
    }

    void Update()
    {
        foreach (KeyValuePair<string, Quest> quest in playerQuests)
        {
            foreach (QuestGoal questGoal in quest.Value.questGoals)
            {
                Debug.Log(questGoal.goalDescription + questGoal.currentValue);
            }
        }
    }

    public static void AcceptQuest(Quest quest)
    {
        playerQuests.Add(quest.title, quest);
        UIChangeManager.Instance.QuestAccepted();
    }

    public static void ProgressQuests(GoalType _goalType, int _itemID)
    {
        foreach (KeyValuePair<string, Quest> quest in playerQuests)
        {
            quest.Value.Progress(_goalType, _itemID);

            if (quest.Value.completed)
            {
                UIChangeManager.Instance.QuestCompleted(quest.Key);
                questsToRemove.Add(quest.Key); //jde to tohle jinak??
            }
        }

        foreach (string questTitle in questsToRemove) //jde to tohle jinak??
        {
            playerQuests.Remove(questTitle); 
        }
    }
}
