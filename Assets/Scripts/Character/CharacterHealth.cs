using UnityEngine;
using UnityEngine.UI;

namespace Character
{
    public class CharacterHealth : MonoBehaviour
    {
        public int startingHealth = 100;
        public int currentHealth;
        public int maxHealth;
        public Text healthAmount;

        private Animator animator;
        private bool isDead;

        public void Awake()
        {
            animator = GetComponent<Animator>();
            currentHealth = startingHealth;
            maxHealth = startingHealth;
            healthAmount.text = "Health: " + currentHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
        
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        
            healthAmount.text = "Health: " + currentHealth;
        }

        public bool IsDead => isDead;
    }
}