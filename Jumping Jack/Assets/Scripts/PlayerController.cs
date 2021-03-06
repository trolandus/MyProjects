﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jumpForce;
	public UnityEngine.UI.Image[] hitPoints;
	
    private Animator myAnimator;
	private Rigidbody2D myRigidbody;
	private bool isGrounded = false;
	private int hp = 4;

	// Use this for initialization
	void Start () {
		myAnimator = this.GetComponent<Animator> ();
		myRigidbody = this.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckInput ();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.collider.tag == "Floor" || col.collider.tag == "Platform")
			isGrounded = true;
	}

	void CheckInput()
	{
		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			myAnimator.Play ("Right");
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			myAnimator.Play ("Left");
		}
		if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
			myAnimator.Play ("Idle");

		if (Input.GetKeyDown (KeyCode.UpArrow) && isGrounded) {
			myRigidbody.AddForce(new Vector2 (0.0f, jumpForce));
			isGrounded = false;
		}

		if (Input.GetKeyDown (KeyCode.Q))
			GetHurt ();

		transform.position += transform.right * Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
	}

	void GetHurt()
	{
		if (hp > 0) {
			Destroy (hitPoints [hp - 1]);
			hp--;
		}
	}
}
