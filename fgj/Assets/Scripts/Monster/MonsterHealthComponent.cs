using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealthComponent : MonoBehaviour
{
    public float maxHealth = 300.0f;
    public float currentHealth;
    public HealthBarComponent healthBarComponent;

    void Start() {
        currentHealth = maxHealth;
         if (healthBarComponent != null) {
            healthBarComponent.Initialize(maxHealth);
        }
    }

    public void ApplyDamage(float damageAmount) {
        Debug.Log("Applied damage to monster");
        currentHealth -= damageAmount;
        healthBarComponent.SetValue(currentHealth);
        //TODO: winstate

    }
}
