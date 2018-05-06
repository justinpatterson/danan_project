using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsCanvas : MonoBehaviour {
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Start Game");
    }
}
