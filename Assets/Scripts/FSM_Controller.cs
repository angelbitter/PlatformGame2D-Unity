using Unity.VisualScripting;
using UnityEngine;

public class FSM_Controller : MonoBehaviour
{
    private State currentState;
    [SerializeField] private PatrolState patrolState;
    [SerializeField] private ChaseState chaseState;
    [SerializeField] private AttackState attackState;

    private void Awake()
    {
        patrolState = GetComponent<PatrolState>();
        chaseState = GetComponent<ChaseState>();
        attackState = GetComponent<AttackState>();
        currentState = patrolState;
        currentState.OnEnterState(this);
    }

    private void Update()
    {
        currentState.OnUpdateState();
    }

    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.OnExitState();
        }

        currentState = newState;
        currentState.OnEnterState(this);
    }

}
