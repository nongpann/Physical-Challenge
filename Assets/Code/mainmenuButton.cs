using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenuButton : MonoBehaviour
{
    [SerializeField] private GameObject mainmenu;
    [SerializeField] private AudioClip Button;
    private AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void startGame()
    {
        source.clip = Button;
        source.Play();
        Invoke("start", source.clip.length);
    }

    public void endGame()
    {
        source.clip = Button;
        source.Play();
        Invoke("end", source.clip.length);
    }

    void start()
    {
        SceneManager.LoadScene("Gameplay");
    }

    void end()
    {
        Application.Quit();
    }
}
