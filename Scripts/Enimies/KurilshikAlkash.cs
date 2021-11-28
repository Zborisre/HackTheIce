using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurilshikAlkash : Enitity
{
	public bool FireBolla = true;
	public GameObject FireBoll;
	public float maxCooldownss = 2;
	float Cooldownss = 0;
	private void Start()
	{
		lives = 1;
	}
	private void Update()
	{
		CheckCoolDownss();
		if (FireBolla)
		Udar();
	}

	private void Udar()
	{
		for (int i = 0; i < Random.Range(70, 99); i++)
		{
			if (Cooldownss < maxCooldownss) return;
			Cooldownss = 0;
			Instantiate(FireBoll, transform.position, Quaternion.identity);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == Hero.Instance.gameObject)
		{
			FireBolla = false;
		}
	}
	private void CheckCoolDownss()
	{
		if (Cooldownss < maxCooldownss)
		{
			Cooldownss += Time.deltaTime;
		}
	}
}