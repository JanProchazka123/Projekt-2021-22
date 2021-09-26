using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WarpDrive : MonoBehaviour
{
    public VisualEffect warpSpeedVFX;
    private bool warpActive;
    public float rate = 0.02f;
    public MeshRenderer cylinder;
    public float delay = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        warpSpeedVFX.Stop();
        warpSpeedVFX.SetFloat("WarpAmount", 0);

        cylinder.material.SetFloat("Active_",0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            warpActive = true;
            StartCoroutine(ActivateParticles());
            StartCoroutine(ActivateShader());
        }
        if(Input.GetKeyUp(KeyCode.V))
        {
            warpActive = false;
            StartCoroutine(ActivateParticles());
            StartCoroutine(ActivateShader());
        }
    }
        IEnumerator ActivateParticles()
    {
        if(warpActive)
        {
            warpSpeedVFX.Play();

            float amount = warpSpeedVFX.GetFloat("WarpAmount");
            while (amount < 1 & warpActive)
            {
                 amount += rate;
                 warpSpeedVFX.SetFloat("WarpAmount", amount);
                 yield return new WaitForSeconds (0.1f);
            }
        }
        else
        {
            float amount = warpSpeedVFX.GetFloat("WarpAmount");
            while (amount > 0 & !warpActive)
            {
                 amount -= rate;
                 warpSpeedVFX.SetFloat("WarpAmount", amount);
                 yield return new WaitForSeconds (0.1f);

                 if(amount <= 0+rate)
                 {
                     amount = 0;
                     warpSpeedVFX.SetFloat("WarpAmount", amount);
                     warpSpeedVFX.Stop();
                 }
            }
            
        }
    }
       IEnumerator ActivateShader()
    {

        if(warpActive)
        {
            yield return new WaitForSeconds (delay);
            float amount = cylinder.material.GetFloat("Active_");
            while (amount < 1 & warpActive)
            {
                 amount += rate;
                 cylinder.material.SetFloat("Active_", amount);
                 yield return new WaitForSeconds (0.1f);
            }
        }
        else
        {
            float amount = cylinder.material.GetFloat("Active_");
            while (amount > 0 & !warpActive)
            {
                 amount -= rate;
                 cylinder.material.SetFloat("Active_", amount);
                 yield return new WaitForSeconds (0.1f);

                 if(amount <= 0 + rate)
                 {
                     amount = 0;
                     cylinder.material.SetFloat("Active_", amount);
                 }
            }
            
        }
    }
}
