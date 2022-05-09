using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Condition")]
    public static bool GameIsPaused = false;

    [Header("Pause Canvas")]
    public GameObject pauseMenuUI;

    [Header("Validation Canvas")]
    public GameObject validationUI;

    [Header("Win menu")]
    public GameObject winMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart(){
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void NextLevel(int next){
        SceneManager.LoadScene(next);
    }

    public void NextButton(){
        validationUI.SetActive(true);
        winMenuUI.SetActive(false);
    }

    public void No(){
        validationUI.SetActive(false);
        winMenuUI.SetActive(true);
    }
}
