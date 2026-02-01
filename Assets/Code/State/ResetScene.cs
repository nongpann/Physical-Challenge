using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : BaseState
{
    private playerStateMachine sm;
    public ResetScene(playerStateMachine stateMachine) : base("ResetScene", stateMachine)
    {
        sm = (playerStateMachine)stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }
}
