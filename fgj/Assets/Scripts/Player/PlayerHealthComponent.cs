using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthComponent : MonoBehaviour
{
    public float maxHealth = 100.0f;
    float currentHealth;

    public HealthBarComponent healthBarComponent;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBarComponent != null)
        {
            healthBarComponent.Initialize(maxHealth);
        }
    }

    void ApplyDamage(float damageAmount)
    {
        Debug.Log("Applied damage to player");
        currentHealth -= damageAmount;
        healthBarComponent.SetValue(currentHealth);
        if (currentHealth < 0)
        {
            SceneManager.LoadScene("SceneElias");
        }
    }
}
