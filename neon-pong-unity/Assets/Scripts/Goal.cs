using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	// Check if the ball has collided with the goal
	void OnTriggerEnter(Collider col) {
		if (col.tag == "Ball") {
			print ("Scored on goal");
			string goalName = transform.name;
			GameManager.Instance.Score (goalName);
		}
	}
}
