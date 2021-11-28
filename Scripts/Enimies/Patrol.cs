using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Enitity
{


    private float speed = 1.5f;
    public int positionOnPatrol;
    public float stoppingDistance;
    public Transform point;
    private Vector3 dir;
    bool movieingRight = true;
    private SpriteRenderer sprite;

    Transform player;

    bool chill = false;
    bool angry = false;
    bool goback = false;
    // Start is called before the first frame update
    void Start()
    {
        dir = transform.right;
		lives = 3;
		sprite = GetComponentInChildren<SpriteRenderer> ();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, point.position)<positionOnPatrol && angry == false)
        {
            chill = true;
        }
        if(Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            angry = true;
            chill = false;
            goback = false;
        }
        if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            goback = true;
            angry = false;
        }

        if(chill == true)
        {
            Chill();
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

    void Chill()
    {
        if(transform.position.x > point.position.x + positionOnPatrol)
        {
            movieingRight = false;
        }
        else if(transform.position.x < point.position.x + positionOnPatrol)
        {
            movieingRight = true;
        }

        if(movieingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            sprite.flipX = dir.x < 0.0f;
        }
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
            Hero.Instance.GetDamage();
        }
    }
}
