using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;   

public class PlayerHealth : MonoBehaviour
{

    public float health; // Player's health
    public float maxHealth; // Player's maximum health
    public Image healthBar; // UI element to display health
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHealth = health; // Set maxHealth to the initial health value
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1); // Update the health bar fill amount based on current health
        if(health <= 0)
        {
            Destroy(gameObject); // Destroy the game object if health is zero or below
        }
    }
}
