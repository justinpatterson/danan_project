using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameCanvas : MonoBehaviour {
    public void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(GameManager.instance);
            GameManager.instance = null;
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Round 1");
    }
    public void StartInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }
}
