using UnityEngine;

public class SeeStage : BaseState
{
    private playerStateMachine sm;
    public SeeStage(playerStateMachine stateMachine) : base("SeeStage", stateMachine)
    {
        sm = (playerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        sm.stageCamera.SetActive(true);
        for (int i = 0; i < sm.textTutorial.Length; i++)
        {
            sm.textTutorial[i].gameObject.SetActive(false);
        }
        sm.textTutorial[3].gameObject.SetActive(true);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        if (Input.GetKeyDown("c"))
        {
            sm.stageCamera.SetActive(false);
            stateMachine.ChangeState(((playerStateMachine)stateMachine).idleState);
        }
    }
}
