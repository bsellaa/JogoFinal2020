using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavGame.Managers;

namespace NavGame.Core
{
    public class DamageableGameObject : TouchableGameObject
    {
        public GameObject defeatPanel;
        public GameObject victoryPanel;
		public DefenseStats defenseStats;
		public int currentHealth;
        public Transform damageTransform;
        public OnDamageTakenEvent onDamageTaken;
        public OnHealthChangedEvent onHealthChanged;
        public OnDiedEvent onDied;

        bool isDead = false;

        protected virtual void Awake()
        {
            currentHealth = defenseStats.maxHealth;
            if(damageTransform == null)
            {
                damageTransform = transform;
            }
        }

        public void TakeDamage(int amount)
        {
            amount -= defenseStats.armor;
            amount = Mathf.Clamp(amount, 1, defenseStats.maxHealth);

            currentHealth -= amount;

            if(onDamageTaken != null)
            {
                onDamageTaken(damageTransform.position, amount);
            }

            if(onHealthChanged != null)
            {
                onHealthChanged(defenseStats.maxHealth, currentHealth);
            }

            if(currentHealth <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            bool isAlly=true;
            if (!isDead)
            {
                if(gameObject.tag=="Enemy") isAlly=false;
                isDead = true;
                Destroy(gameObject);
                if(onDied != null)
                {
                    onDied();
                }
            }


            if(isAlly)
            {
                defeatPanel.SetActive(true);
            }
            else
            {
                victoryPanel.SetActive(true);
            }
        }
    }
}
