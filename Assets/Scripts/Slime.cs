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
        base.OnTriggerEnter2D(collision);
        // Debug.Log("Slime collided with: " + collision.gameObject.name);
    }

}
