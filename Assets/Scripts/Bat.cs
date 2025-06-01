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
    }
    private void AttackBat()
    {

    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
}
