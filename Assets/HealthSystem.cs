using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Header("General Health")]
    public float maxHealth;
    public float currentHealth;

    [Header("Invincibility Frames")]
    public bool invulnerableAfterDamage = false;
    public float secondsOfInvulnerability;
    private float secondsLeftOfInvulnerability;

    private void Start()
    {
        currentHealth = maxHealth;
        
        
    }

    private void Update()
    {
        if (secondsLeftOfInvulnerability > 0)
        {
            secondsLeftOfInvulnerability -= Time.deltaTime;
        }
    }

    //Tell this health system to take damage. Returns whether damage was dealt or not (might not if player is invulnerable after damage)
    public bool TakeDamage(float damageAmount)
    {
        
        if ((invulnerableAfterDamage == false) || (secondsLeftOfInvulnerability <= 0))
        {
            currentHealth -= damageAmount;
            Debug.Log(damageAmount.ToString() + " damage taken.");
            if (invulnerableAfterDamage)
            {
                secondsLeftOfInvulnerability = secondsOfInvulnerability;
            }
            if (currentHealth <= 0)
            {
                Die();
            }
            return true;
        } 
        else
        {
            return false;
        }

    }

    public void Die()
    {
        //Destroy(gameObject);
    }

}
