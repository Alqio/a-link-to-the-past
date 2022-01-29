using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthComponent : MonoBehaviour
{
    public float maxHealth = 100.0f;
    float currentHealth;

    void Start() {
        currentHealth = maxHealth;
    }

    void ApplyDamage(float damageAmount) {
        Debug.Log("Applied damage to player");
        currentHealth -= damageAmount;

        if (currentHealth < 0) {
            SceneManager.LoadScene("Main");
        }
    }
}
