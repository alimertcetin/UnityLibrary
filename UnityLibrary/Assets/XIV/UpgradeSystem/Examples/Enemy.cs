using System;
using UnityEngine;

namespace XIV.UpgradeSystem.Examples
{
    public class Enemy : MonoBehaviour
    {
        public float health;
        public float damageAmount;

        public void TakeDamage(float amount, Action callbackIfDies)
        {
            health -= amount;
            if (health <= 0)
            {
                callbackIfDies?.Invoke();
            }
        }
    }
}