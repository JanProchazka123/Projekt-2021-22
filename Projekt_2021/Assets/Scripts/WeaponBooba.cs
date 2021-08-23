using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponBooba: MonoBehaviour 
{
   public Rigidbody playerBody;
   public PlayerMovement movementScript;
   [Header("Up/Down")]
   public float bobbingSpeedUD = 0.03f;
   public float bobbingAmountUD = 0.005f;
  
   [Header("Left/Right")]
   public float bobbingSpeedLR = 0.1f;
   public float bobbingAmountLR = 0.05f;
   [Header("Rotation")]
   public float rotationSpeedLR = 1f;
   public float rotationAmountLR = 3f;
   private Quaternion initialRotation;
   [Header("Times slowed when slow walk")]
   public float timesSlowed;
   //Privates
   float midpoint = 0f;
   float midpoint2 = 0f;
   private float timer = 0.0f;
   private float timer2 = 0.0f;
   void FixedUpdate () 
   {
      //transform.localRotation = Quaternion.Euler(new Vector3(0f ,rotationAmountLR , 0f));
      if(movementScript.grounded == true)
      {
         float waveslice = 0.0f;
         Vector3 positionVectorWeapon = transform.localPosition; 
         if (Mathf.Abs(playerBody.velocity.x) == 0 && Mathf.Abs(playerBody.velocity.z) == 0) 
         {
            timer = 0.0f;
         }
         else 
         {
            waveslice = Mathf.Sin(timer);
            if (Input.GetKey(KeyCode.LeftShift) == false)
            {
            timer = timer + bobbingSpeedUD;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
            timer = timer + bobbingSpeedUD / timesSlowed;
            }
            if (timer > Mathf.PI * 2) 
            {
               timer = timer - (Mathf.PI * 2);
            }
         }
         if (waveslice != 0) 
         {
            float translateChange = waveslice * bobbingAmountUD;
            float totalAxes = Mathf.Abs(playerBody.velocity.x) + Mathf.Abs(playerBody.velocity.z);
            totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            positionVectorWeapon.y = midpoint + translateChange;
         }
         else 
         {
            positionVectorWeapon.y = midpoint;
         }
         transform.localPosition = positionVectorWeapon;
         //1st
         float waveslice2 = 0.0f;
         Vector3 positionVectorWeapon2 = transform.localPosition;
         
         if (Mathf.Abs(playerBody.velocity.x) == 0 && Mathf.Abs(playerBody.velocity.z) == 0) 
         {
            timer2 = 0.0f;
         }
         else 
         {
            waveslice2 = Mathf.Sin(timer2);
            if (Input.GetKey(KeyCode.LeftShift) == false)
            {
               timer2 = timer2 + bobbingSpeedLR;
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
               timer2 =  timer2 + bobbingSpeedLR / timesSlowed;
            }
            
            if (timer2 > Mathf.PI * 2) 
            {
               timer2 = timer2 - (Mathf.PI * 2);
            }
         }
         if (waveslice2 != 0) 
         {
            float translateChange2 = waveslice2 * bobbingAmountLR;
            float totalAxes2 = Mathf.Abs(playerBody.velocity.x) + Mathf.Abs(playerBody.velocity.z);
            totalAxes2 = Mathf.Clamp (totalAxes2, 0.0f, 1.0f);
            translateChange2 = totalAxes2 * translateChange2;
            positionVectorWeapon2.x = midpoint2 + translateChange2;
         }
         else 
         {
            positionVectorWeapon2.x = midpoint2;
         }
         transform.localPosition = positionVectorWeapon2;
      }
      if(movementScript.grounded == false)
      {
         
      }
   }
}