using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Thowing : MonoBehaviour
{
    [SerializeField] private GameObject javalin;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private playerStateMachine playerStateMachine;
    private float currentAngle = 0f;

    private void Start()
    {
        if(javalin == null)
        {
            javalin = transform.GetChild(0).gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            javalin.GetComponent<Projectile>().Shoot(playerStateMachine.power);
            javalin.transform.parent = null;
        }

        float input = Input.GetAxisRaw("Horizontal");

        currentAngle -= input * rotationSpeed * Time.deltaTime;

        currentAngle = Mathf.Clamp(currentAngle, -45f, 45f);

        transform.rotation = Quaternion.Euler(0, 0, currentAngle);

    }

}


