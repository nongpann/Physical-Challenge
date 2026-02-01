using UnityEngine;

public class Target : MonoBehaviour
{

    [SerializeField] private Score sc;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        sc.Setscore();
        Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
