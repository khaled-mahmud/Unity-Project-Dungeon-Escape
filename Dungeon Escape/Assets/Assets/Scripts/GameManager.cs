using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if(_instance == null)
			{
				Debug.LogError("GameManager is null");
			}
			return _instance;
		}
	}

	public bool HasKeyToCastle { get; set; }

	public Player player { get; private set; }

	private void Awake()
	{
		_instance = this;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
}
