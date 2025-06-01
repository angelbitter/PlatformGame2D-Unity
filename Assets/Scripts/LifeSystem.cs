using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    public void GetDamaged(float damage)
    {
        maxHealth -= damage;
        if (maxHealth <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        // Handle death logic here, e.g., play animation, destroy object, etc.
        Debug.Log("Character has died.");
        Destroy(gameObject);
    }   
}

