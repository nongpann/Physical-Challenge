using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject textRO;
    [SerializeField] private GameObject cameraStage;
    private Score round;
    private Rigidbody2D rb;
    private bool fired = false;
    private int score = 0;
    public bool isMultiplied = false;
    

    private void Start()
    {
        round = GameObject.Find("Manager").GetComponent<Score>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void FixedUpdate()
    {
        if (fired && rb.linearVelocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (transform.position.y <= -5.0f)
        {
            score *= 0;
            textRO.SetActive(true);
            cameraStage.SetActive(true);
            isMultiplied = true;
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Shoot(float power)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        rb.AddForce(transform.right * power, ForceMode2D.Impulse);
        fired = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        fired = false;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.position += transform.up * 0.15f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Multiplier1") && !isMultiplied)
        {
            score *= 1;
            isMultiplied = true;
        }
        else if (other.CompareTag("Multiplier2") && !isMultiplied)
        {
            score *= 2;
            isMultiplied = true;
        }
        else if (other.CompareTag("Multiplier3") && !isMultiplied)
        {
            score *= 3;
            isMultiplied = true;
        }
        else if (other.CompareTag("Multiplier4") && !isMultiplied)
        {
            score *= 4;
            isMultiplied = true;
        }
        else if (other.CompareTag("Multiplier5") && !isMultiplied)
        {
            score *= 5;
            isMultiplied = true;
        }
        else if (other.CompareTag("RunningFloor") && !isMultiplied)
        {
            score *= 0;
            isMultiplied = true;
        }
        else
        {
            Destroy(other.gameObject);
            score++;
        }
            
    }

    public int Getscore()
    {
        return score;
    }
}