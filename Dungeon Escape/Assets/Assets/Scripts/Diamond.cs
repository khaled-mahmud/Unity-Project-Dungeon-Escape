using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
	//onTriggerEnter to collect it
	//check for the player
	//add the value of the diamond to the player
	public int gems = 1;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			//collect me
			Player player = other.GetComponent<Player>();

			if(player != null)
			{
				player.AddGems(gems);
				Destroy(this.gameObject);
			}
			Destroy(this.gameObject);
		}
	}
}
