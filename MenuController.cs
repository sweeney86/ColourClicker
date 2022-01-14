using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject modePanel;

    public void OnClickPlay()
    {
        modePanel.SetActive(true);
    }

    public void OnClickEasy()
    {
        SceneManager.LoadSceneAsync("Easy");
    }

    public void OnClickNormal()
    {
        SceneManager.LoadSceneAsync("Normal");
    }

    public void OnClickHard()
    {
        SceneManager.LoadSceneAsync("Hard");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
