using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
	//move left 3 meters per second
	//detect player and deal damage(IDamagable Interface)
	//destroy this after 5 seconds

	private void Start()
	{
		Destroy(this.gameObject, 5.0f);
	}

	private void Update()
	{
		transform.Translate(Vector3.right * 3 * Time.deltaTime);
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		IDamagable hit = other.GetComponent<IDamagable>();

		if(hit != null)
		{
			hit.Damage();
			Destroy(this.gameObject);
		}
	}
	
}
