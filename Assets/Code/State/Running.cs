using System.Xml.Xsl;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Running : BaseState
{
    private playerStateMachine sm;
    private float totalRotation = 0f;
    private float lastAngle = 0f;
    private Rigidbody2D rb;
    
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

        sm.currentLeftVelocity = 0f;
        totalRotation = 0f;
        lastSpinSpeed = 0f;

        Vector2 mouseViewportPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 center = new Vector2(0.5f, 0.5f);
        Vector2 direction = mouseViewportPos - center;
        lastAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

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
        
        if (Input.GetMouseButtonDown(0) || sm.javalin.isThrowed)
        {
            rb.linearVelocity = new Vector2(0, 0);
            sm.source.clip = sm.ThrowSound;
            sm.source.Play();
            stateMachine.ChangeState(((playerStateMachine)stateMachine).SeeJavalinState);
        }

        if (Input.GetKeyDown("r") || sm.transform.position.x >= 9.6f)
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

        // Calculate angle BEFORE applying aspect ratio correction
        float currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float deltaAngle = Mathf.DeltaAngle(lastAngle, currentAngle);
        totalRotation += deltaAngle;
        lastAngle = currentAngle;

        // Apply aspect ratio correction for distance calculation
        float aspectRatio = (float)Screen.width / Screen.height;
        direction.x *= aspectRatio;
        float distance = direction.magnitude;
        float distanceMultiplier = Mathf.Clamp01(distance / 0.5f);

        float spinSpeed = Mathf.Abs(deltaAngle);
        if (spinSpeed > 0)
        {
            sm.currentLeftVelocity += spinSpeed * distanceMultiplier * sm.movementPower * Time.deltaTime;
            if (sm.power < 50)
            {
                sm.power += (sm.currentLeftVelocity * Time.deltaTime);
            }

            if (sm.power > 50)
            {
                sm.power = 50;
            }
            
            sm.triangle.transform.Rotate(0, 0, sm.currentLeftVelocity * Time.deltaTime * 5);
        }
        if (spinSpeed < lastSpinSpeed / 1.5f)
        {
            sm.currentLeftVelocity *= sm.friction;
            if (sm.power > 0)
            {
                sm.power -= (sm.power * Time.deltaTime) * 3.0f;
            }
        }
        else if (spinSpeed <= 0)
        {
            sm.currentLeftVelocity *= sm.friction;
            if (sm.power > 0)
            {
                sm.power -= (sm.power * Time.deltaTime) * 3.0f;
            }
        }
        lastSpinSpeed = spinSpeed;
        rb.linearVelocity = new Vector2(sm.currentLeftVelocity, rb.linearVelocity.y);
        sm.powerColor.value = sm.power;

        Debug.Log(sm.power);
    }

}
