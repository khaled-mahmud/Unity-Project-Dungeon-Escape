using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	//variable to datermine if the damage function can be called
	private bool _canDamage = true;

	private void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log("Hit " + other.name);

		IDamagable hit = other.GetComponent<IDamagable>();
		if(hit != null)
		{
			//if can attack
			if(_canDamage == true)
			{
				hit.Damage();
				//set that variable to false
				_canDamage = false;
				StartCoroutine(ResetDamage());
			}
		}
	}

	//coroutine to reset variable after 0.5f
	IEnumerator ResetDamage()
	{
		yield return new WaitForSeconds(0.5f);
		_canDamage = true;
	}
}
