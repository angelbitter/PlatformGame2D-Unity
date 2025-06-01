using UnityEngine;

public class Bat : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        base.StartPatrol();
    }
    protected override void Attack()
    {
        AttackBat();
        // Additional initialization for Bat can go here
    }
    protected override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        // Additional Bat-specific damage handling can go here
        // For example, play a sound or trigger an animation
    }
    private void AttackBat()
    {
        // Initialize Bat-specific properties if needed
        // For example, set the speed or range of movement
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        // Debug.Log("Bat collided with: " + collision.gameObject.name);
    }
}
