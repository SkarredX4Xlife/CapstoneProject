using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarTest : MonoBehaviour
{
    private int Health = 250;
    public int maxHealth = 250;
    public Image Healthbar_Fill1;
    public Animator animator;

    public string gettingHitAnimation = "GettingHitAnimation";
    public string dyingAnimation = "Dying";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        animator.Play(gettingHitAnimation);
        Health -= damage;
        Health = Mathf.Clamp(Health, 0, maxHealth);
        UpdateHealthbar();
        if (Health <= 0)
        {
            Die();
        }
    }

    public void UpdateHealthbar()
    {
        if (Healthbar_Fill1 != null)
        {
            Healthbar_Fill1.fillAmount = (float)Health / maxHealth;
        }
    }

    private void Die()
    {
        animator.Play(dyingAnimation);
        Debug.Log("Character Has Died");
    }
}
