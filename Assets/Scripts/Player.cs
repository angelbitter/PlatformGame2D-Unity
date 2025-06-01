using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inputH;
    
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheckPoint; // Distance to check for ground
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;
    [SerializeField] private LayerMask whatIsDamageable;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDamage; 
    private bool attacking = false;

    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        StartAttack();
        Jump();
    }
    private void Movement()
    {
        if (attacking)
        {
            return; // Prevent movement while attacking
        }
        inputH = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(inputH * movementSpeed, rb.linearVelocity.y);

        if (inputH != 0)
        {
            if (inputH > 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else if (inputH < 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
    }
    private void StartAttack()
    {
        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            if (CheckOnGround())
            {
                rb.linearVelocity = Vector2.zero; 
            }
            attacking = true;
            animator.SetTrigger("attack");
        }
    }
    private void Attack()
    {
        Collider2D[] collidersHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsDamageable);
        foreach (Collider2D collider in collidersHit)
        {
            LifeSystem lifeSystem = collider.GetComponent<LifeSystem>();
            if (lifeSystem != null)
            {
                Debug.Log("Attacking: " + collider.name);
                lifeSystem.GetDamaged(attackDamage);
            }
        }
    }
    private void OnAttackAnimationEnd()
    {
        attacking = false;
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && CheckOnGround() && !attacking)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("jump");
        }
    }
    private bool CheckOnGround()
    {
        Debug.DrawRay(groundCheckPoint.position, Vector2.down * groundCheckDistance, Color.red, 0.5f);
        bool hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, whatIsGround);
        if (hit)
        {
            animator.SetTrigger("land");
        }
        else
        {
            animator.ResetTrigger("land");
        }
        return hit;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
