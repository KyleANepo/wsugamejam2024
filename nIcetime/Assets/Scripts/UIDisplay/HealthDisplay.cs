using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public static int scoreSubtract = 0;

    [Header("Audio")]
    public AudioSource takeDmg;

    // Start is called before the first frame update
    void Start()
    {
        scoreSubtract = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0)
        {
            Debug.Log("Lose menu here!");
        }

    }

    // Healthdisplay is a bad name for this, this is the player health tracker
    public void TakeDamage(float damage)
    {
        takeDmg.Play();
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100;
    } 

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100;
    }
}
