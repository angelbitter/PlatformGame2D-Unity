using UnityEngine;

public abstract class State : MonoBehaviour
{
    protected FSM_Controller controller;

    public virtual void OnEnterState(FSM_Controller controller)
    {
        this.controller = controller;
    }
    public abstract void OnExitState();
    public abstract void OnUpdateState();

}
