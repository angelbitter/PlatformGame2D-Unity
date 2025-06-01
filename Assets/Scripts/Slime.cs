using System.Collections;
using UnityEngine;

public class Slime : Enemy
{
    void Start()
    { 
        base.StartPatrol();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitbox"))
        {
            Player player = collision.gameObject.GetComponentInParent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
