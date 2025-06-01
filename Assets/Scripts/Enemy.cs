using System.Collections;
using Mono.Cecil;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float life;
    [SerializeField] protected float damage;
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;
    public float Life { get => life; set => life = value; }
    public float Damage { get => life; set => life = value; }
    private Vector3 targetPosition;
    private int currentPointIndex = 0;


    protected virtual void StartPatrol()
    {
        if (points.Length != 0)
        {
            targetPosition = points[currentPointIndex].position;
            StartCoroutine(Patrol());
            return;
        }
    }
    private IEnumerator Patrol()
    {
        while (true)
        {
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
            DefineNewPoint();
        }
    }

    protected virtual void TakeDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Die();
        }
    }
    protected virtual void Attack()
    {
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void DefineNewPoint()
    {
        currentPointIndex++;
        if (currentPointIndex >= points.Length)
        {
            currentPointIndex = 0;
        }
        targetPosition = points[currentPointIndex].position;
        LookAtDestination();
    }
    private void LookAtDestination()
    {
        Vector3 direction = targetPosition - transform.position;
        if (direction.x > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (direction.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            Player player = collision.gameObject.GetComponentInParent<Player>();
            if (player != null)
            {
                targetPosition = player.transform.position;
                LookAtDestination();
            }
        }
        else if (collision.gameObject.CompareTag("PlayerHitbox"))
        {
            Player player = collision.gameObject.GetComponentInParent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
