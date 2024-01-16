using UnityEngine;

public class CombatEntity : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public int armor = 10;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Apply armor reduction
        damage -= armor;

        // Ensure damage doesn't go below zero
        damage = Mathf.Max(0, damage);

        // Reduce health by the modified damage amount
        currentHealth -= damage;

        // Check if the entity should be destroyed
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle death, such as disabling the GameObject or triggering death animations
        Debug.Log(gameObject.name + " has died.");


    }
}
