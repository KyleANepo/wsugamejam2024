using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject winScreenUI;

    public static bool GameWon = false;

    // Start is called before the first frame update
    void Start()
    {
        Win();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Win()
    {
        Time.timeScale = 0f;
        GameWon = true;
    }

    public void nextLevel()
    {
        Time.timeScale = 1.0f;
        GameWon = false;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu...");
        Time.timeScale = 1f;
        GameWon = false;
        SceneManager.LoadScene("StartMenu");
    }
}
