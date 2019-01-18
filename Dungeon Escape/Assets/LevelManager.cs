using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public void LoadLooseLevelAfterDelay()
	{
		Invoke("LoadLooseLevel", 3.0f);
	}

	public void LoadLooseLevel()
	{
		SceneManager.LoadScene("Loose_Screen");
	}

	public void LoadWinLevel()
	{
		SceneManager.LoadScene("Win_Screen");
	}

	public void QuitRequest()
	{
		Application.Quit();
	}


}
