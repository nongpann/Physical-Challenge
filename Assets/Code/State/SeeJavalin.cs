using UnityEngine;

public class SeeJavalin : BaseState
{
    private playerStateMachine sm;
    public SeeJavalin(playerStateMachine stateMachine) : base("SeeJavalin", stateMachine)
    {
        sm = (playerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        sm.followJavalinCamera.SetActive(true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetKeyDown("r"))
        {
            stateMachine.ChangeState(((playerStateMachine)stateMachine).ResetSceneState);
        }
    }
}
