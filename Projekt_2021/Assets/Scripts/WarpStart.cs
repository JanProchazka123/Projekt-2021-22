using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpStart : MonoBehaviour
{
    public bool startJump = false;
    public MonitorScript monitorScript;
    public AI_Movement aiMovement;
    public GameObject gloub;
    public GameObject warning;
    public GameObject four;
    public GameObject three;
    public GameObject two;
    public GameObject one;
    public int jumpingTime = 5;
    public Material skybox1;
    public Material skybox2;
    private Coroutine demencia;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startJump == true)
        {
            StartingWapr();
        }
    }
    void OnTriggerStay()
    {
        if (Input.GetKeyDown("e"))
        {
            StartingWapr();
        }
    }

    void StartingWapr()
    {
        monitorScript.yesorno = false;
        aiMovement.afk = true;
        demencia = StartCoroutine(JumpIn4()); 

    }
    private IEnumerator JumpIn4()
    {
        yield return new WaitForSeconds(1);
        gloub.SetActive(false);
        warning.SetActive(true);
        four.SetActive(true);
        demencia = StartCoroutine(JumpIn3());
    }
    private IEnumerator JumpIn3()
    {
        yield return new WaitForSeconds(1);
        three.SetActive(true);
        four.SetActive(false);
        demencia = StartCoroutine(JumpIn2());
    }
        private IEnumerator JumpIn2()
    {
        yield return new WaitForSeconds(1);
        two.SetActive(true);
        three.SetActive(false);
        demencia = StartCoroutine(JumpIn1());
    }
    private IEnumerator JumpIn1()
    {
        yield return new WaitForSeconds(1);
        one.SetActive(true);
        two.SetActive(false);
        demencia = StartCoroutine(Jumping());
    }
    private IEnumerator Jumping()
    {
        yield return new WaitForSeconds(1);
        RenderSettings.skybox = skybox2;

        warning.SetActive(false);
        one.SetActive(false);
        yield return new WaitForSeconds(jumpingTime);
        gloub.SetActive(true);
         monitorScript.yesorno = true;
        aiMovement.afk = false;
        RenderSettings.skybox = skybox1;
    }
}
