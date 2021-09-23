using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIChangeManager : MonoBehaviour
{
    private static UIChangeManager _instance;
    public static UIChangeManager Instance { get { return _instance; } }

    public Text questStartEndText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    //tohle mi celkove prijde jako retarded øešení ale nevim jak jinak
    public void QuestAccepted()
    {
        StartCoroutine(QuestAccept());
    }
    public void QuestCompleted(string questTitle)
    {
        StartCoroutine(QuestComplete(questTitle));
    }

    IEnumerator QuestComplete(string questTitle)
    {
        questStartEndText.text = "mise" + " " + questTitle + "splnena!!!!!!!!!!";

        yield return new WaitForSecondsRealtime(5);

        questStartEndText.text = "";
    }

    IEnumerator QuestAccept()
    {
        questStartEndText.text = "mise akceptovana!!!!!!!!!!";

        yield return new WaitForSecondsRealtime(5);

        questStartEndText.text = "";
    }
}
