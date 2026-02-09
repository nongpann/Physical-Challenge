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

    private SpriteRenderer colorPlayer;
    public Running(playerStateMachine stateMachine) : base("Running", stateMachine)
    {
        sm = (playerStateMachine)stateMachine;
        rb = sm.GetComponent<Rigidbody2D>();
    }

    public override void Enter()
    {
        base.Enter();
        rb.gravityScale = 0f;
        for (int i = 0; i < sm.textTutorial.Length; i++)
        {
            sm.textTutorial[i].gameObject.SetActive(false);
        }
        sm.textTutorial[1].gameObject.SetActive(true);
        colorPlayer = sm.GetComponentInChildren<SpriteRenderer>();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        
        if (Input.GetMouseButtonDown(0))
        {
            rb.linearVelocity = new Vector2(0, 0);
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

        float lastRotation = totalRotation;

        Vector2 mouseViewportPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 center = new Vector2(0.5f, 0.5f);
        Vector2 direction = mouseViewportPos - center;

        float currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float deltaAngle = Mathf.DeltaAngle(lastAngle, currentAngle);
        totalRotation += deltaAngle;
        lastAngle = currentAngle;

        float distance = direction.magnitude;
        float distanceMultiplier = Mathf.Clamp01(distance / 0.25f);

        float spinSpeed = Mathf.Abs(deltaAngle);

        if (spinSpeed > 0)
        {
            currentLeftVelocity += spinSpeed * distanceMultiplier * sm.movementPower * Time.deltaTime;
            sm.power += (spinSpeed * Time.deltaTime) * 5.0f;
            sm.triangle.transform.Rotate(sm.triangle.transform.rotation.x, sm.triangle.transform.rotation.y, sm.triangle.transform.rotation.z + 20);
        }
        

        if (spinSpeed < lastSpinSpeed / 2f)
        {
            currentLeftVelocity *= sm.friction;
            if (sm.power > 0)
            {
                sm.power -= (sm.power * Time.deltaTime) * 2.0f;
            }

        }
        else if (spinSpeed <= 0)
        {
            currentLeftVelocity *= sm.friction;
            if (sm.power > 0)
            {
                sm.power -= (sm.power * Time.deltaTime) * 2.0f;
            }
        }

        lastSpinSpeed = spinSpeed;
        rb.linearVelocity = new Vector2(currentLeftVelocity, rb.linearVelocity.y);

        sm.powerColor.color = Color.Lerp(Color.white, Color.red, sm.power / 40.0f);
        
        //Debug.Log(sm.power);

    }

}
