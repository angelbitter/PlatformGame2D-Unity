using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

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
        Destroy(gameObject);
    }   
}

