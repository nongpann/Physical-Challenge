using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Thowing : MonoBehaviour
{
    [SerializeField] private GameObject javalin;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private playerStateMachine playerStateMachine;
    private float currentAngle = 0f;
    [HideInInspector] public bool isThrowed = false;
    private float timeChangeAngle = 0;
    private float maxTimeChangeAngle = 0.5f;
    private bool changeAngle = false;
    private int randomNumber = 0;

    private void Start()
    {
        if(javalin == null)
        {
            javalin = transform.GetChild(0).gameObject;
        }
    }

    private void Update()
    {
        if (playerStateMachine.getState() == "Running")
        {
            
            float input = Input.GetAxisRaw("Horizontal");

            currentAngle -= input * rotationSpeed * Time.deltaTime;

            //currentAngle = Mathf.Clamp(currentAngle, 0f, 45f);

            transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            
            if (playerStateMachine.currentLeftVelocity >= 1 )
            {
                
                if (!changeAngle)
                {
                    randomNumber = Random.Range(1, 100);
                    changeAngle = true;
                }

                if (randomNumber > 50)
                {
                    currentAngle += ((rotationSpeed + playerStateMachine.currentLeftVelocity) / 2) * Time.deltaTime;
                }
                
                else {
                    currentAngle -= ((rotationSpeed + playerStateMachine.currentLeftVelocity) / 2) * Time.deltaTime;
                }
            }
            timeChangeAngle += Time.deltaTime;

            if (timeChangeAngle > maxTimeChangeAngle)
            {
                changeAngle = false;
                timeChangeAngle = 0;
            }

            transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            float zRot = transform.eulerAngles.z;
            if (zRot > 180)
            {
                zRot -= 360;
            }

            if (zRot > 70 || zRot < -25)
            {
                javalin.GetComponent<Projectile>().Shoot(0);
                isThrowed = true;
                javalin.transform.parent = null;
            }
        }

        if (playerStateMachine.getState() == "SeeJavalin" && !isThrowed)
        {
            isThrowed = true;
            javalin.GetComponent<Projectile>().Shoot(playerStateMachine.power);
            javalin.transform.parent = null;
        }
        
    }

}


