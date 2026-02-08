using UnityEngine;
using UnityEngine.SceneManagement;

public class stageManager : MonoBehaviour
{
    [SerializeField] private GameObject[] stagePrefab;

    [HideInInspector] public bool isSpawn = false;

    public void spawnStage()
    {
        if (isSpawn == false)
        {
            int randomStage = Random.Range(0, stagePrefab.Length);
            Instantiate(stagePrefab[randomStage], transform);
            isSpawn = true;
        }

    }

    public void deleteStage()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

    }

    public void restartScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
