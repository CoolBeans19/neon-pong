using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	public bool launched;

	public float speed;
	public float maxSpeed;

	public AudioClip ScoreClip;
	public AudioClip HitPaddle;
	public AudioClip HitWall;
	public AudioSource src;

	private Rigidbody rb;

	private Animator anim;

	void Awake() {
		src = GetComponent<AudioSource> ();

		rb = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();

		rb.velocity = new Vector3 (0.0f, 0.0f, 0.0f);;

		launched = false;
	}

	void OnTriggerEnter (Collider col) {
		if (col.CompareTag("Paddle")) {

			float z = hitFactor (transform.position,
				          col.transform.position,
				          col.bounds.size.z);
			
			rb.velocity = new Vector3(Mathf.Clamp(((-rb.velocity.x) * 1.07f), -maxSpeed, maxSpeed), 0.0f, Mathf.Clamp((1.5f * z * speed), -maxSpeed, maxSpeed));
			anim.SetTrigger ("HitPaddle");
			src.PlayOneShot (HitPaddle, 0.7f);
		}
		else if (col.CompareTag("Wall")) {
			rb.velocity = new Vector3(rb.velocity.x, 0.0f, -rb.velocity.z);
			anim.SetTrigger ("HitWall");
			src.PlayOneShot (HitWall, 0.7f);
		}
		else if (col.CompareTag("Goal")) {
			StartCoroutine(ResetBall ());
			src.PlayOneShot (ScoreClip, 0.7f);
		}
	}

	public void Launch() {
		if (!launched) {
			launched = true;
			rb.velocity = new Vector3(RandomDirection () * speed, 0.0f, RandomDirection () * speed * 0.5f);
			anim.SetTrigger ("Launch");
		}
	}

	public void TotalReset () {
		launched = false;
		rb.velocity = Vector3.zero;
		transform.position = new Vector3 (0.0f, 0.5f, 0.0f);
	}

	IEnumerator ResetBall () {
		rb.velocity = Vector3.zero;
		transform.position = new Vector3 (0.0f, 0.5f, 0.0f);

		// Pause for one second before launching the ball
		yield return new WaitForSeconds(1.0f);

		rb.velocity = new Vector3 (RandomDirection () * speed, 0.0f, RandomDirection () * speed * 0.5f);
		anim.SetTrigger ("Launch");
	}

	float RandomDirection() {
		float random = Random.value;
		if (random > 0.5) {
			return 1;
		} else {
			return -1;
		}
	}

	float hitFactor(Vector3 ballPos, Vector3 paddlePos,
		float paddleHeight) {
		// ascii art:
		// ||  1 <- at the top of the racket
		// ||
		// ||  0 <- at the middle of the racket
		// ||
		// || -1 <- at the bottom of the racket
		return ((ballPos.z - paddlePos.z)) / paddleHeight;
	}

}
