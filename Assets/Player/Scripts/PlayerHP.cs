using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    //hp
    public int currentPlayerHealth;
    public int totalPlayerHealth = 100;

    //shield
    public int currentPlayerShield;
    public int totalPlayerShield = 100;
    public GameObject shieldIndicator;

    public UIController uiController;
    public GameObject hpIndicator;

    //death
    public RestartWorld restartWorld;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayerShield = 0;
        currentPlayerHealth = totalPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayerShield > totalPlayerShield) 
        { 
            currentPlayerShield = totalPlayerShield;
        }

        if (currentPlayerHealth > totalPlayerHealth)
        {
            currentPlayerHealth = totalPlayerHealth;
        }

        uiController.DisplayText("HealthText", currentPlayerHealth + " / " + totalPlayerHealth);
        uiController.DisplayText("ShieldText", currentPlayerShield.ToString());

        HPIndicator();

        if (currentPlayerHealth <= 0) 
        { 
            restartWorld.Reset();
        }
    }

    public void TakeDamage(int damage)
    {
        if (currentPlayerShield > 0) //make sure sheild dmg is first 
        {
            currentPlayerShield = currentPlayerShield - damage;

            if (currentPlayerShield < 0) //makes sure if sheild breaks remaining dmg goes through
            {
                currentPlayerHealth = currentPlayerHealth + currentPlayerShield;
                currentPlayerShield = 0;
            }
        }

        else
        {
            currentPlayerHealth = currentPlayerHealth - damage;
        }     
    }

    public void GainShield(int amount)
    {
        currentPlayerShield = currentPlayerShield + amount;
    }

    public void GainLife(int amount)
    {
        currentPlayerHealth = currentPlayerHealth + amount;
    }

    public void HPIndicator()
    {
        float normalizedHealth = (float)currentPlayerHealth / totalPlayerHealth;
        float maxAlpha = 0.4f; //maximum alpha cap
        float newAlpha = Mathf.Clamp01(normalizedHealth * maxAlpha);
        UpdateHpIndicatorAlpha(newAlpha);
    }

    private void UpdateHpIndicatorAlpha(float alpha)
    {
        Renderer rend = hpIndicator.GetComponent<Renderer>();

        if (rend != null)
        {
            Color color = rend.material.color;
            color.a = alpha;
            rend.material.color = color;
        }
    }
}
