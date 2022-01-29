using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealthComponent : MonoBehaviour
{
    public float maxHealth = 100.0f;
    public float currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    public void ApplyDamage(float damageAmount) {
        Debug.Log("Applied damage to monster");
        currentHealth -= damageAmount;

        //TODO: winstate

    }
}
