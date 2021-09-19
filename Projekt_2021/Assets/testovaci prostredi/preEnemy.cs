using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class preEnemy : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public HealthBar healthBar;

    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(-10);
        }
    }

    void TakeDamage(int damage)
    {        
        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;
        else if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
}
