using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
	public GameObject LifeUnits;

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log(other.name);
		Player player = other.GetComponent<Player>();

		

		if (player != null)
		{
			player.Health = 1;
			player.Damage();
			LifeUnits.SetActive(false);
		}
	}
}
