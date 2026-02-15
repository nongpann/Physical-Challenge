using System.Xml.Xsl;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DebugState : BaseState
{
    private playerStateMachine sm;
    private Rigidbody2D rb;
    public DebugState(playerStateMachine stateMachine) : base("Debug", stateMachine)
    {
        sm = (playerStateMachine)stateMachine;
        rb = sm.GetComponent<Rigidbody2D>();
    }

    public override void Enter()
    {
        base.Enter();
        rb.gravityScale = 0f;
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (Input.GetMouseButtonDown(0) || sm.javalin.isThrowed)
        {
            rb.linearVelocity = new Vector2(0, 0);
            //sm.source.clip = sm.ThrowSound;
            //sm.source.Play();
            stateMachine.ChangeState(((playerStateMachine)stateMachine).SeeJavalinState);
        }

        if (Input.GetKeyDown("r") || sm.transform.position.x >= 10.0f)
        {
            stateMachine.ChangeState(((playerStateMachine)stateMachine).ResetSceneState);
        }
    }

    public override void UpdateAction()
    {
        base.UpdateAction();
        sm.power = sm.debugPower;
        sm.transform.rotation = Quaternion.Euler(0, 0, sm.debugAngle);
        //Debug.Log(sm.power);
    }

}
