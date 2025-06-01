using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private float patrolSpeed;
    [SerializeField] private Transform route;
    private Transform currentPatrolPoint;
    private int currentPatrolIndex;
    private List<Transform> patrolPoints = new List<Transform>();

    public override void OnEnterState(FSM_Controller controller)
    {
        base.OnEnterState(controller);

        foreach (Transform child in route)
        {
            patrolPoints.Add(child);
        }
        currentPatrolPoint = patrolPoints[0];
    }
    public override void OnExitState()
    {
        patrolPoints.Clear();
    }

    public override void OnUpdateState()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPatrolPoint.position, patrolSpeed * Time.deltaTime);

        if (transform.position == currentPatrolPoint.position)
        {
            CalculateNextPatrolPoint();
        }
    }

    private void CalculateNextPatrolPoint()
    {
        currentPatrolIndex++;
        currentPatrolIndex %= patrolPoints.Count;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
    }
}
