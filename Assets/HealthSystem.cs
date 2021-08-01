using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class HealthSystem : MonoBehaviour
{
    [Header("General Health")]
    public float maxHealth;
    [HideInInspector]
    public float currentHealth;

    [Header("Invincibility Frames")]
    public bool invulnerableAfterDamage = false;
    public float secondsOfInvulnerability;
    private float secondsLeftOfInvulnerability;

    private ShieldBehaviour myShield; 

    private void Start()
    {
        currentHealth = maxHealth;
        myShield = GetComponent<ShieldBehaviour>();
        
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
            float damageLeftToDeal = damageAmount;

            //Check if we have a shield. If we do, take from that first
            if (myShield != null)
            {
                damageLeftToDeal = myShield.TakeShieldDamage(damageAmount);
            }
            
            //Reduce Health
            currentHealth -= damageLeftToDeal;
            //Debug.Log(damageLeftToDeal.ToString() + " HP damage to " + gameObject.name);
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
        Destroy(gameObject);
    }

}
