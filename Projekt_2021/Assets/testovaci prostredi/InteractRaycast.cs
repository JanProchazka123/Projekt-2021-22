using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractRaycast : MonoBehaviour
{
    public Transform playerCamera;
    public int interactionDistance = 3;
    public Text questAcceptUI;

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            if (hit.transform.CompareTag("quest giver"))
            {
                Quest hitQuest = hit.transform.GetComponent<QuestGiver>().qGiverQuest;

                // jestli je to questgiver a nemam quest v dictionary zobraz acceptUI
                if (!QuestingSystem.playerQuests.ContainsKey(hitQuest.title))
                {
                    questAcceptUI.gameObject.SetActive(true);
                }

                // jestli je to questgiver, zmacknu na nej "e" a nemam quest S TIMHLE TITLEM vezmi si jeho quest 
                // (kdyz budou mit questy stejnej title tak to nebude fungovat)
                // !!!(pozdeji upravit na usekey)!!!
                if (Input.GetKeyDown(KeyCode.E) && !QuestingSystem.playerQuests.ContainsKey(hitQuest.title))
                {
                    QuestingSystem.AcceptQuest(hitQuest);
                    questAcceptUI.gameObject.SetActive(false);
                }
            }
            if (!hit.transform.CompareTag("quest giver"))
            {
                questAcceptUI.gameObject.SetActive(false);
            }

            //tohle je jen testovaci if na to jestli funguje progresování questem
            if (hit.transform.CompareTag("gather object"))
            {
                int hitGatherObjectID = hit.transform.GetComponent<GatherObject>().id;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    QuestingSystem.ProgressQuests(GoalType.Gather, hitGatherObjectID);

                    hit.transform.gameObject.SetActive(false); 
                }
            }
        }
        else
        {
            questAcceptUI.gameObject.SetActive(false);
        }
    }    
}
