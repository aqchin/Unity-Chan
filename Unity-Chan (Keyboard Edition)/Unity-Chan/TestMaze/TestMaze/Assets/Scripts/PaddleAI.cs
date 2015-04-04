using UnityEngine;
using System.Collections;

public class PaddleAI : MonoBehaviour {

	public GameObject paddle;
	public GameObject ball;
	public static float difficulty = 7;

	private float z;

	// Use this for initialization
	void Start () {
		z = paddle.rigidbody.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		// Translate paddle
		if (UserPaddle.isStarted) {
			float step = difficulty * Time.deltaTime;
			paddle.rigidbody.position = 
			Vector3.MoveTowards (
				new Vector3 (paddle.rigidbody.position.x,
			        	paddle.rigidbody.position.y, z), 
				new Vector3 (ball.rigidbody.position.x,
			        	ball.rigidbody.position.y, z), step);
		}
	}
}
