using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;


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
    public float power = 0;

    public float movementPower = 10f;
    public float friction = 0.95f;
    public GameObject followJavalinCamera;
    public GameObject line;
    

    private void Awake()
    {
        idleState = new Idle(this);
        RunningState = new Running(this);
        SeeJavalinState = new SeeJavalin(this);
        ResetSceneState = new ResetScene(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    public void destroy(GameObject coin)
    {
        Destroy(coin);
    }
}
