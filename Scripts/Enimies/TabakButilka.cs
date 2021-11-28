using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabakButilka : Enitity {

	private float speed = 3.5f;
	private int stoppingDistance = 20;

	Transform player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
		if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
			Destroy(this.gameObject);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject == Hero.Instance.gameObject) {
			Destroy(this.gameObject);
			Hero.Instance.GetDamage ();
		}
	}

}
