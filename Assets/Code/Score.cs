using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    [HideInInspector] public static Score Instance { get; private set; }
    [HideInInspector] public int round = 0;

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI roundText;
    private GameObject FSpanel;
    private TextMeshProUGUI FSText;
    private Projectile projectile;
    private int showScore = 0;
    private int currScore = 0;
    private int oldScore = 0;
    

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (round > 2)
        {
            ResetEverything();
        }
        round++;

        oldScore = PlayerPrefs.GetInt("PlayerScoreKey", 0);
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        roundText = GameObject.Find("Round").GetComponent<TextMeshProUGUI>();
        FSText = GameObject.Find("Final Score").GetComponent<TextMeshProUGUI>();
        FSpanel = GameObject.Find("Final Score Panel");
        FSpanel.SetActive(false);
        projectile = GameObject.FindFirstObjectByType<Projectile>();
    }

    void Update()
    {
        roundText.text = "Round : " + round;

        showScore = projectile.Getscore();
        Setscore();
        scoreText.text = "Score : " + showScore + " + " + oldScore;

        if (projectile.isMultiplied && round >= 3)
        {
            FSpanel.SetActive(true);
            FSText.text = "Your score : " + (showScore + oldScore);
        }
        
    }

    private void Setscore()
    {
        currScore = showScore + oldScore;
        PlayerPrefs.SetInt("PlayerScoreKey", currScore);
        PlayerPrefs.Save();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        PlayerPrefs.DeleteAll();
    }

    private void ResetEverything()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        showScore = 0;
        currScore = 0;
        oldScore = 0;
        round = 0;
    }
}
