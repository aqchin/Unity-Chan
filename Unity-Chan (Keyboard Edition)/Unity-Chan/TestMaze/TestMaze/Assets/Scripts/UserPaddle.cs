using UnityEngine;
using System.Collections;

public class UserPaddle : MonoBehaviour {

	public GameObject paddle;
	public GameObject dock;
	public GameObject top;
	public GameObject bot;
	public GameObject lft;
	public GameObject rgt;

	public float sensitivity;
	public float z_offset;
	public static bool isStarted;

	private static bool lock_rot = false;
	private static bool lo_click = false;
	private Vector3 rest_pos;
	private Vector3 paddle_pos;
	public static bool started;
	public static bool menu;

	// Use this for initialization
	void Start () {
		isStarted = false;
		started = false;
		menu = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && !menu) {
			if (!started)
				audio.Play ();
			isStarted = true;
			started = true;
		}
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.LoadLevel("Pong");
		if (isStarted) {
			Screen.lockCursor = true;
			float v = Input.GetAxis ("Mouse Y");
			float h = Input.GetAxis ("Mouse X");
			float rotv = Input.GetAxis ("Vertical");
			float roth = Input.GetAxis ("Horizontal");
			this.transform.Translate (new Vector3 (h,v,0), Space.World);
			Vector3 rotation = new Vector3(-rotv*45, roth*30,0);
			this.transform.rotation = Quaternion.Euler (rotation);
		}

	}

	public static void reset() {
		isStarted = false;
		lock_rot = false;
		lo_click = false;
	}
}
