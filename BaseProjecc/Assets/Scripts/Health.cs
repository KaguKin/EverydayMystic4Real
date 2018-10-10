using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int CurrHealth { get; set; }
    public int MaxHealth { get; set; }
    public IAIController owner;

    public void TakeDamage(int dmg)
    {
        CurrHealth -= dmg;
        CheckForDeath();
    }

    void CheckForDeath()
    {
        if(CurrHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        owner.OnDeath();
    }

    public void Initialise(int newMaxHealth, IAIController newOwner)
    {
        MaxHealth = newMaxHealth;

        CurrHealth = MaxHealth;

        owner = newOwner;
    }
}
