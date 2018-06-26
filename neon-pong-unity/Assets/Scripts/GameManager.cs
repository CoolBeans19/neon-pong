using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject ball;

	public Text player1ScoreText;
	public Text player2ScoreText;
	public Text winText;
	public Text restartHint;

	public static int player1Score;
	public static int player2Score;
	public int maxScore;

	public bool launched;
	public bool gameOver;

	public static GameManager Instance;

	void Awake(){
		Instance = this;
		launched = false;
		gameOver = false;
	}

	// Use this for initialization
	void Start () {
		player1Score = 0;
		player2Score = 0;
		winText.text = "";
		restartHint.text = "Press Space to launch the ball";

		setScoreText();
	}

	void Update() {
		if (!launched) {
			if (Input.GetKey (KeyCode.Space)) {
				launched = true;
				ball.GetComponent<BallController> ().Launch ();
				restartHint.text = "";
			}
		}
		else if (gameOver) {
			if (Input.GetKeyUp (KeyCode.Space)) {
				restartGame ();
			}
		}

		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public void Score(string goalID) {
		if (goalID == "Player1Goal") {
			player2Score++;
		} else {
			player1Score++;
		}

		setScoreText ();

		if (player1Score >= maxScore) {
			endGame (1);
		} else if (player2Score >= maxScore) {
			endGame (2);
		}
	}

	public void restartGame() {
		launched = false;
		gameOver = false;
		ball.SetActive (true);
		ball.GetComponent<BallController> ().TotalReset ();
		player1Score = 0;
		player2Score = 0;
		winText.text = "";
		restartHint.text = "Press Space to launch the ball";
		setScoreText ();
	}

	void setScoreText() {
		player1ScoreText.text = player1Score.ToString();
		player2ScoreText.text = player2Score.ToString();
	} 

	void endGame(int player) {
		ball.SetActive(false);
		gameOver = true;
		winText.text = "Player " + player + " wins!";
		restartHint.text = "Press Space to restart, Escape to quit";
	}
		
}
