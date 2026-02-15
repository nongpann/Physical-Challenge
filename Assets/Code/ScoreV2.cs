using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class ScoreV2 : MonoBehaviour
{
    [SerializeField] private Projectile javelin;
    [SerializeField] private Thowing isThrow;
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject FSpanel;
    [SerializeField] private GameObject resultButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private AudioClip[] Button;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();    
    }

    void Update()
    {
        if (javelin.isMultiplied && isThrow.isThrowed)
        {
            resultButton.SetActive(true);
        }
    }

    public void OpenScore()
    {
        
        source.clip = Button[0];
        source.Play();
        resultButton.SetActive(false);
       
        Invoke("score", source.clip.length / 2);
        
    }
    public void playAgian()
    {
        source.clip = Button[1];
        source.Play();
        Invoke("Reload", source.clip.length);
    }
    public void returnToMainmenu()
    {
        source.clip = Button[1];
        source.Play();
        Invoke("mainMenu", source.clip.length);
    }

    void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void score()
    {
        FSpanel.gameObject.SetActive(true);
        float score = javelin.gameObject.transform.position.x - line.transform.position.x + 10.0f;
        if (score > 0)
        {
            scoreText.text = "Score : " + score.ToString("F2") + " M";
        }
        else
        {
            scoreText.text = "You dropped your javelin.";
        }
    }
}
