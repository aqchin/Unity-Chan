using UnityEngine;
using System.Collections;

public class BallPhysics : MonoBehaviour {

	public float force;
	public float accel;
	public float maxForce;
	public GameObject ball;
	public static GameObject s_ball;
	public GameObject lose_wall;

	private bool touch;
	private bool starting = false;
	public static Vector3 storeVel;
	private string lastTap = "n/a";
	private bool losed = false;
	float t;

	// Use this for initialization
	void Start () {
		// Initial velocity
		s_ball = ball;
		losed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (UserPaddle.isStarted) {
			if(ball.transform.position.z <= lose_wall.transform.position.z || losed) {
				if (!audio.isPlaying) {
				t = audio.clip.length;
				audio.Play ();
				}
				losed = true;
				t -= Time.deltaTime;
				//Debug.Log (t);
				if (t < 0) {
					UserPaddle.reset();
					Application.LoadLevel("pong");
				}
			}


			if(!starting) {
				ball.rigidbody.isKinematic = false;
				ball.rigidbody.velocity = new Vector3 (0.0f, -1.0f, 1.0f);
				starting = true;
			}

			if (ball.rigidbody.mass != 1.0f) ball.rigidbody.mass = 1.0f;
			ball.rigidbody.velocity = force * ball.rigidbody.velocity.normalized;
			//Debug.Log (lastTap);

			if (ball.rigidbody.velocity.z < 5.0f && ball.rigidbody.velocity.z > -5.0f) {
				//Debug.Log (ball.rigidbody.velocity);
				if (lastTap == "AI Paddle") {
					ball.rigidbody.velocity += new Vector3 (0.0f, 0.0f, -5.0f);
				} else if (lastTap == "Player Paddle") {
					ball.rigidbody.velocity += new Vector3 (0.0f, 0.0f, 5.0f);
				}
			}
		} 
		else ball.rigidbody.isKinematic = true;
	}

	void OnTriggerEnter(Collider other) {
		if (!touch) {
			touch = true;

			if (other.name == "AI Paddle" || other.name == "Player Paddle") {
				if (ball.rigidbody.detectCollisions && force * accel < maxForce)
					force *= accel;
				
				ball.rigidbody.velocity -= 
					new Vector3 (0.0f, 0.0f, 3 * ball.rigidbody.velocity.z);
			}
			
			if (other.name == "AI Paddle") {
				lastTap = "AI Paddle";
				//ball.rigidbody.AddForce (new Vector3 (0, 0, -force));
				ball.rigidbody.AddForce(force * other.transform.forward);
			} else if (other.name == "Player Paddle") {
				lastTap = "Player Paddle";
				ball.rigidbody.velocity = Vector3.Reflect(ball.rigidbody.velocity,
				                                          other.transform.up.normalized);
				//ball.rigidbody.AddForce(force * other.rigidbody.transform.forward);
			}
			else if(other.name == "Win") {
				Screen.lockCursor = false;
				Application.LoadLevel("winity_chan");
			}
		}
	}

	void OnTriggerExit(Collider other) {
		touch = false;
	}

	void lose() {
		UserPaddle.reset();
		audio.Play ();
		pauseAndResume();
		float t = audio.clip.length;
		float time = Time.time;
		while (Time.time - time < t);
		Application.LoadLevel("pong");
	}

	public static void pauseAndResume() {
		if (UserPaddle.isStarted) {
			BallPhysics.storeVel = s_ball.rigidbody.velocity;
			UserPaddle.isStarted = false;
		}
		else if (!UserPaddle.isStarted) {
			s_ball.rigidbody.isKinematic = false;
			s_ball.rigidbody.velocity = BallPhysics.storeVel;
			UserPaddle.isStarted = true;
		}
	}
}
