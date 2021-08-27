using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Movement : MonoBehaviour
{
    public GameObject rotejt;
    public Animator aiAnim;
    private int rotateZ;
    //static Random.Range rnd = new Random();
    private int jaka;
    private int casAnim;
    public bool afk = false;
    private Coroutine animacia;
    // Start is called before the first frame update
    void Start()
    {
        Randomak();
        if(afk == true)
        {
            jaka = 0;
            aiAnim.SetInteger("jakaAnim", jaka);
        }
        else
        {

            animacia = StartCoroutine(Animacno());
            
        }
    }
    void Randomak()
    {
            casAnim = Random.Range(2,4);
            rotateZ = Random.Range(1,4);
            jaka = Random.Range(1,4);
    }

    // Update is called once per frame
    void Update()
    {
        if(afk == false && jaka == 0)
        {
            Plebikea();
        }
    }
    void Plebikea()
    {
        Randomak();
        if(afk == true)
        {
            jaka = 0;
            aiAnim.SetInteger("jakaAnim", jaka);

        }
        
        else
        {
            animacia = StartCoroutine(Animacno()); 
        }

    }
    private IEnumerator Animacno()
    {
        //rotejt.transform.Rotate(0,0,rotateZ, Space.Self);
        aiAnim.SetInteger("jakaAnim", jaka);
        yield return new WaitForSeconds(casAnim);
        Plebikea();
    }
}
