using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    public GameObject winScreenUI;
    public TimerDisplay td;
    private int score;

    public GameObject oneStar;
    public GameObject twoStar;
    public GameObject threeStar;

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
        score = td.finalScore;
        Stars(score);
    }

    public void Stars(int score)
    {
        if (score >= 988)
        {
            oneStar.SetActive(false);
            twoStar.SetActive(false);
            threeStar.SetActive(true);
        }
        else if (score >= 975)
        {
            oneStar.SetActive(false);
            twoStar.SetActive(true);
            threeStar.SetActive(false);
        }
        else
        {
            oneStar.SetActive(true);
            twoStar.SetActive(false);
            threeStar.SetActive(false);
        }
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
