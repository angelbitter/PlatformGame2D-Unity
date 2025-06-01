using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Wizard : Enemy
{
    [SerializeField] private FireBall fireBallPrefab;
    [SerializeField] private Transform fireBallSpawnPoint;
    [SerializeField] private float fireBallCooldown;

    private ObjectPool<FireBall> fireBallPool;
    private Animator animator;
    private bool attacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fireBallPool = new ObjectPool<FireBall>(CreateFireBall, OnGetFireBall, OnReleaseFireBall, OnDestroyFireBall, false, 10, 20);
        animator = GetComponent<Animator>();
    }

    private FireBall CreateFireBall()
    {
        FireBall disparoCopy = Instantiate(fireBallPrefab);
        disparoCopy.GetFromPool();
        disparoCopy.MyPool = fireBallPool;
        return disparoCopy;
    }
    private void OnGetFireBall(FireBall fireBall)
    {
        fireBall.gameObject.SetActive(true);
        fireBall.GetFromPool();
    }
    private void OnReleaseFireBall(FireBall fireBall)
    {
        fireBall.gameObject.SetActive(false);
    }
    private void OnDestroyFireBall(FireBall fireBall)
    {
        Destroy(fireBall.gameObject);
    }
    IEnumerator AttackRoutine()
    {
        if (!attacking)
        {
            attacking = true;
            while (true)
            {
                yield return new WaitForSeconds(fireBallCooldown);
                animator.SetTrigger("atacar");
            }
        }
    }
    private void ThrowFireBall()
    {
        Debug.Log("Throwing FireBall");
        FireBall fireBall = fireBallPool.Get();
        fireBall.transform.position = fireBallSpawnPoint.position;
        fireBall.transform.rotation = base.transform.rotation;
        fireBall.Launch();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            Player player = collision.gameObject.GetComponentInParent<Player>();
            if (player != null)
            {
                StartCoroutine(AttackRoutine());
            }
        }
    }
}
