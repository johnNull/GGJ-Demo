using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Script : MonoBehaviour {
	public int speed;
	float maxSpeed = 2.5f;
	public bool grounded = false, doubleJump = true;
	Rigidbody2D rb2d;
	Animator anim;
	// Use this for initialization
	void Start () {
		speed = 0;
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump") && grounded) {
			rb2d.AddForce (new Vector3 (0, 24, 0));
		} else if (Input.GetButtonDown ("Jump") && doubleJump) {
			doubleJump = false;
			rb2d.velocity = new Vector3 (rb2d.velocity.x, 0, 0);
			rb2d.AddForce (new Vector3 (0, 24, 0));
		}
	}

	void FixedUpdate() {
		//Moving and flipping orientation of character
		int force= 0;
		if (Input.GetKey ("d")) {
			force = 8;
			transform.localScale = new Vector3 (1, 1, 1);
		}
		else if (Input.GetKey ("a")) {
			force = -8;
			transform.localScale = new Vector3 (-1, 1, 1);
		}

		//Slide reduction
		else if (Mathf.Abs(rb2d.velocity.x) > 0) {
			rb2d.AddForce (new Vector3 (-rb2d.velocity.x / 2, 0, 0));
		}
			
		//Actually adding force
		if (Mathf.Abs (rb2d.velocity.x) < maxSpeed) {
			rb2d.AddForce (new Vector3 (force, 0, 0));
		}
		anim.SetInteger("Speed", Mathf.Abs(force));//Let the animator know whether to idle or walk
	}

	void OnTriggerEnter2D(){
		grounded = true;
		anim.SetBool("Grounded", grounded);
		doubleJump = true;
	}

	void OnTriggerExit2D(){
		grounded = false;
		anim.SetBool("Grounded", grounded);
	}
}
