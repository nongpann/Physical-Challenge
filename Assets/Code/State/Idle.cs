using UnityEngine;

public class Idle : BaseState
{
    private float _horizontalInput;
    private playerStateMachine sm;

    public Idle (playerStateMachine stateMachine) : base("Idle", stateMachine) 
    {
        sm = (playerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetKeyDown("e"))
        {
            stateMachine.ChangeState(((playerStateMachine)stateMachine).RunningState);
        }

        if (Input.GetKeyDown("r"))
        {
            stateMachine.ChangeState(((playerStateMachine)stateMachine).ResetSceneState);
        }
    }

}
