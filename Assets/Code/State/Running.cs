using System.Xml.Xsl;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Running : BaseState
{
    private playerStateMachine sm;
    private float totalRotation = 0f;
    private float lastAngle = 0f;
    private Rigidbody2D rb;
    
    private float currentLeftVelocity = 0f;
    private float lastSpinSpeed = 0f;
    public Running(playerStateMachine stateMachine) : base("Running", stateMachine)
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
        
        if (Input.GetMouseButtonDown(0))
        {
            rb.linearVelocity = new Vector2(0, 0);
            stateMachine.ChangeState(((playerStateMachine)stateMachine).SeeJavalinState);
        }

        if (Input.GetKeyDown("r") || sm.transform.position.x >= sm.line.transform.position.x)
        {
            stateMachine.ChangeState(((playerStateMachine)stateMachine).ResetSceneState);
        }
    }

    public override void UpdateAction()
    {
        base.UpdateAction();

        float lastRotation = totalRotation;

        Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 mousePos = Input.mousePosition;
        Vector2 direction = mousePos - center;

        float currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float deltaAngle = Mathf.DeltaAngle(lastAngle, currentAngle);
        totalRotation += deltaAngle;
        lastAngle = currentAngle;

        float distance = direction.magnitude;

        float distanceMultiplier = Mathf.Clamp01(distance / 200f);

        float spinSpeed = Mathf.Abs(deltaAngle);

        if (spinSpeed > 0)
        {
            currentLeftVelocity += spinSpeed * distanceMultiplier * sm.movementPower * Time.deltaTime;
            sm.power += (spinSpeed * Time.deltaTime) * 2;
        }
        else
        {
            sm.power = 0;
        }

        rb.linearVelocity = new Vector2(currentLeftVelocity, rb.linearVelocity.y);

        if (spinSpeed < lastSpinSpeed / 2f)
        {
            currentLeftVelocity *= sm.friction;
            if (sm.power > 0)
            {
                sm.power -= 1 * Time.deltaTime;
            }

        }
        lastSpinSpeed = spinSpeed;
        Debug.Log(sm.power);

    }

}
