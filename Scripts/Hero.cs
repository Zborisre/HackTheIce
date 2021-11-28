using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hero : Enitity
{

	public GameObject FireBollass;
	[SerializeField] private Transform player;
	public float maxCooldownss;
	public float maxspeedCooldownss;
	private float maxstamina = 20;
	float stamina;

	public int maxHP;
	int HP;
	public GameObject backGround;


	[SerializeField] private AudioSource jumpSound;
	[SerializeField] private AudioSource painSound;

	[SerializeField] private float speed = 0;
	[SerializeField] private float speedfast = 0;
	[SerializeField] private float jumpForce = 3f;

	//private int money = 0;
	//private int goldmoney = 0;

	private bool isGrounded = false;

	public bool isAttacking = false;
	public bool isRecharged = true;

	public Transform attackPos;
	public float attackRange;
	public LayerMask enemy;

	private float moveInput;

	private Rigidbody2D rb;
	private Animator anim;
	private SpriteRenderer sprite;

	public static Hero Instance { get; set; }
	public Image dashSkill;
	public Image dashSpeed;
	public Image hpBar;

	private int extraJump;
	public int extraJumpValue;

	private States State
	{
		get { return (States)anim.GetInteger ("state"); }
		set { anim.SetInteger ("state", (int)value); }
    }


    private void Awake()
	{
		extraJump = extraJumpValue;
		HP = maxHP;
		stamina = maxstamina;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		sprite = GetComponentInChildren<SpriteRenderer> ();
		Instance = this;
		isRecharged = true;
	}


	private void CheckHP()
	{
		float newScales = (float)HP / (float)maxHP;
		hpBar.fillAmount = newScales;
	}


	private void FixedUpdate()
	{
		CheckGround ();
	}

	// Update is called once per frame
	private void Update() {
		CheckHP();
		if (isGrounded)
        {
			extraJump = extraJumpValue;
        }
		if (isGrounded) State = States.idle;
		CheckSpeedCoolDownss();
		if (Input.GetButton("Horizontal"))
			Run();
		if (Input.GetButtonDown("Jump") && extraJump > 0)
		{
			State = States.jump;
			Jump();
			extraJump--;
		}
		else if (isGrounded && Input.GetButtonDown("Jump") && extraJump == 0)
		{
			State = States.jump;
			Jump();
		}
	}


	private void Run()
	{
		State = States.run;
		Vector3 dir = transform.right * Input.GetAxis("Horizontal");

		transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

		sprite.flipX = dir.x < 0.0f;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//var a = 5;
		//Debug.Log("Test");
		//Debug.Log(collision.gameObject.name);
		if (collision.tag.Equals("Perehod"))
		{
			SceneManager.LoadScene(3);
		}
	}

	private void Jump()
	{
		rb.AddForce(new Vector2(0,600));
		jumpSound.Play ();
	}

	private void CheckSpeedCoolDownss()
	{

		if (stamina < maxstamina)
		{
			stamina += (2*Time.deltaTime);
		}
	}

	private void CheckGround()
	{
		Collider2D[] collider = Physics2D.OverlapCircleAll (transform.position, 0.3f);
		isGrounded = collider.Length > 1;

		if (!isGrounded) State = States.jump;
	}

	public override void GetDamage()
	{
			HP -= 1;
			painSound.Play();
			if (HP <= 0)
			{
				Die();
			}
	}

	public enum States
	{
		idle,
		run,
		jump,
		attack,
		runs
	}

}


