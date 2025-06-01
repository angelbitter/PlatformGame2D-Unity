using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class FireBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D collisionHitbox;
    [SerializeField] private Transform triggerHitbox;
    [SerializeField] private float impulseBall;
    [SerializeField] private float timeAlive;
    [SerializeField] private float damage;
    private Collider2D hitboxCollider;
    // Reference to the Animator component
    private Animator animator;
    private bool isActive = false;
    public ObjectPool<FireBall> MyPool { get; set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void GetFromPool()
    {
        isActive = true;
        animator = GetComponent<Animator>();
        hitboxCollider = GetComponent<Collider2D>();
        hitboxCollider.isTrigger = false;
        collisionHitbox.bodyType = RigidbodyType2D.Dynamic;
        StartCoroutine(DestroyAfterTime());
    }
    public void Launch()
    {
        collisionHitbox.AddForce(transform.right * impulseBall, ForceMode2D.Impulse);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitbox"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Explode();
        }
    }
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(timeAlive);
        if (isActive)
        {
             Explode();
        }
    }
    private void Explode()
    {
        collisionHitbox.bodyType = RigidbodyType2D.Static;
        hitboxCollider.isTrigger = true;
        animator.SetTrigger("explotar");
    }
    private void OnDestroy()
    {
        isActive = false;
        MyPool.Release(this);
    }
}
