using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCollider : MonoBehaviour
{
	private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
		levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();

		if (player != null)
		{
			levelManager.LoadWinLevel();
		}
	}

}
