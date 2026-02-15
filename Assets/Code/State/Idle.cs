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
        sm.stageManager.spawnStage();
        for (int i = 0; i < sm.textTutorial.Length; i++)
        {
            sm.textTutorial[i].gameObject.SetActive(false);
        }
        sm.textTutorial[0].gameObject.SetActive(true);
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

        //if (Input.GetKeyDown("c")) {
        //    stateMachine.ChangeState(((playerStateMachine)stateMachine).seeStageState);
        //}

        if (Input.GetKeyDown("r"))
        {
            stateMachine.ChangeState(((playerStateMachine)stateMachine).debugState);
        }
    }

}
