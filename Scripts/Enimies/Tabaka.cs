using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tabaka : Enitity
{

	private float speed = 1.5f;
	private Vector3 dir;
	private bool isTimes = true;
	private SpriteRenderer sprite;
	public float stoppingDistance = 20;
	public int positionOnPatrol = 10;
	public Transform point;
	public float maxCooldownss = 2;
	float Cooldownss = 0;


	Transform player;

	bool chill = false;
	bool angry = false;
	bool goback = false;

	// Update is called once per frame
	private void Start()
	{
		dir = transform.right;
		lives = 3;
		sprite = GetComponentInChildren<SpriteRenderer>();
		player = GameObject.FindGameObjectWithTag("Player").transform;


	}

	void Update()
	{
		CheckCoolDownss();
		StartCoroutine(TimesAnimation());
		if (Vector2.Distance(transform.position, point.position) < positionOnPatrol && angry == false)
		{
			chill = true;
		}
		if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
		{
			angry = true;
			chill = false;
			goback = false;
		}
		if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
		{
			goback = true;
			angry = false;
		}

		if (chill == true)
		{
			Move();
		}
		else if (angry == true)
		{
			Angry();
		}
		else if (goback == true)
		{
			GoBack();
		}
	}


	private IEnumerator TimesAnimation()
	{
		for (int i = 0; i < 100; i++)
		{
			isTimes = false;
			yield return new WaitForSeconds(0.9f);
			isTimes = true;
			yield return new WaitForSeconds(0.9f);
		}
	}

	private void Move()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.1f + transform.right * dir.x * 0.7f, 0.1f);
		if (colliders.Length > 0)
			dir *= -1f;

		transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

		sprite.flipX = dir.x > 0.0f;
	}

	void Angry()
	{
		transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
		speed = 2.5f;
	}

	void GoBack()
	{
		transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == Hero.Instance.gameObject)
		{
			for (int i = 0; i < 1000; i++)
			{
				if (Cooldownss >= maxCooldownss)
				{
					Cooldownss = 0;
					Hero.Instance.GetDamage();
				}
			}
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

