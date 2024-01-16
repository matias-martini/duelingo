using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public LayerMask enemyLayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PerformMeleeAttack();
        }
    }

    void PerformMeleeAttack()
    {
        // Perform a raycast to detect enemies in front of the player
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange, enemyLayer))
        {
            // Check if the hit object has an Enemy component (you can replace this with your enemy script)
            Enemy enemy = hit.collider.GetComponent<Enemy>();

            // If an enemy is hit, apply damage or perform other actions
            if (enemy != null)
            {
                enemy.TakeDamage(10); // Adjust the damage value as needed
            }
        }
    }
}