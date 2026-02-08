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
        Rigidbody2D rb = sm.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        for (int i = 0; i < sm.textTutorial.Length; i++)
        {
            sm.textTutorial[i].gameObject.SetActive(false);
        }
        sm.textTutorial[2].gameObject.SetActive(true);
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
