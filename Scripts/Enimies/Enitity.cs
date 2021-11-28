using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enitity : MonoBehaviour
{
	private Vector3 pos;
	public GameObject Silver;
	protected int lives;
	public GameObject explosion;

	public virtual void GetDamage()
	{
		lives--;
		if (lives < 1)
		{
			Die();
		}
	}

	public virtual void Die()
	{
		GameObject explosionRef = (GameObject)Instantiate(explosion);
		explosionRef.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		Destroy (this.gameObject);

	}

}
