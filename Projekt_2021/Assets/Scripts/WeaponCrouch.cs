using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCrouch : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public StaminaBar staminaBar;
    [Header("CTRL")]
    public float rotationAmountCTRL = 45f;
    public float speedRotationCTRL = 4f;
    public float positionAmountCTRL = 4f;
    
    //Privates
    private Quaternion initialRotation;
    private Vector3 initialPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.localRotation;
        initialRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {

            if (staminaBar.currentStamina >= 10 && Input.GetKey(KeyCode.LeftControl))
            {
                Crouch();
            }

        else
        {
            CrouchStop();
        }

        

        if(playerMovement.grounded == false)
        {
            Quaternion tiltFall = Quaternion.Euler(new Vector3(-60,0f,0f));
            transform.localRotation = Quaternion.Lerp(transform.localRotation, tiltFall, Time.deltaTime);

            Vector3 tiltFall1 = new Vector3(0f, 0f, 0.5f);
            transform.localPosition = Vector3.Lerp(transform.localPosition, tiltFall1, Time.deltaTime);
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime);
    }
    public void Crouch()
    {
        Vector3 tiltLR1 = new Vector3(positionAmountCTRL * -0.1f, 0f, 0f);
        Quaternion tiltLR2 = Quaternion.Euler(new Vector3(0f,0f,rotationAmountCTRL));
        transform.localRotation = Quaternion.Lerp(transform.localRotation,tiltLR2,Time.deltaTime * speedRotationCTRL);
        transform.localPosition = Vector3.Lerp(transform.localPosition, tiltLR1, Time.deltaTime * speedRotationCTRL);
    }
    public void CrouchStop()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation,initialRotation,Time.deltaTime * speedRotationCTRL);
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition, Time.deltaTime * speedRotationCTRL);
    }
}
