using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour {

	public float speed;

	public bool player1 = true;

	private KeyCode moveUp = KeyCode.W;
	private KeyCode moveDown = KeyCode.S;

	private Rigidbody rb;

	private Collider self;

	private Animator anim;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> ();
		self = GetComponent<Collider> ();
		anim = GetComponent<Animator> ();
		if (!player1) {
			moveUp = KeyCode.UpArrow;
			moveDown = KeyCode.DownArrow;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (moveUp)) {
			rb.velocity = new Vector3(0.0f, 0.0f, speed);
		}
		else if (Input.GetKey (moveDown)) {
			rb.velocity = new Vector3(0.0f, 0.0f, -speed);
		}
		else if (!(Input.GetKey(moveUp) || Input.GetKey(moveDown))) {
			rb.velocity = Vector3.zero;
		}

		if (transform.position.z >= 10.0f - (self.bounds.size.z / 2)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, 10.0f - (self.bounds.size.z / 2));
		}

		if (transform.position.z <= -10.0f + (self.bounds.size.z / 2)) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, -10.0f + (self.bounds.size.z / 2));
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Ball") {
			anim.SetTrigger ("Collision");
		}
	}

}
