using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBar;
    public int maxStamina = 100;
    public int currentStamina;
    public WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    private Coroutine regenWait;
    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    // Update is called once per frame
    public void UseStamina(int amount)
    {
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if(regenWait != null)
            {
                StopCoroutine(regenWait);
            }
            
            regenWait = StartCoroutine(RegenStamina());
        }
        else
        {

        }
    }
    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(3);

        while(currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regenWait = null;
    }
}
