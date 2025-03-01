using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public int damage = 25;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Opponent"))
        {
            Debug.Log("Hit Detected");
            HealthbarTest opponentHealth = other.GetComponent<HealthbarTest>();
            if (opponentHealth != null)
            {
                opponentHealth.TakeDamage(damage);
                Debug.Log("Hit! Damge dealt: " + damage);
            }
        }
    }
}