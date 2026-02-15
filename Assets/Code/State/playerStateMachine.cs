using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;


public class playerStateMachine : StateMachine
{
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Running RunningState;
    [HideInInspector]
    public SeeJavalin SeeJavalinState;
    [HideInInspector]
    public ResetScene ResetSceneState;
    [HideInInspector]
    public SeeStage seeStageState;
    [HideInInspector]
    public DebugState debugState;
    [HideInInspector]
    public float power = 0;
    [HideInInspector]
    public float currentLeftVelocity = 0f;
    
    public float movementPower = 10f;
    public float friction = 0.95f;
    public GameObject followJavalinCamera;
    public GameObject stageCamera;
    public GameObject line;
    public stageManager stageManager;
    public GameObject[] textTutorial;
    public Slider powerColor;
    public GameObject triangle;
    public Thowing javalin;
    public AudioClip ThrowSound;
    public AudioSource source;

    public float debugPower;
    public float debugAngle;



    private void Awake()
    {
        idleState = new Idle(this);
        RunningState = new Running(this);
        SeeJavalinState = new SeeJavalin(this);
        ResetSceneState = new ResetScene(this);
        seeStageState = new SeeStage(this);
        debugState = new DebugState(this);
    }

    protected override BaseState GetInitialState()
    {
        return debugState;
    }

    public void destroy(GameObject coin)
    {
        Destroy(coin);
    }
}
