using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenButtonBehaviour : MonoBehaviour 
{
	public void Reset() 
	{
		SceneManager.LoadScene ("Start Game");
	}
	public void Quit() 
	{
			Application.Quit ();
	}
}
