using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public Maze mazePrefab;

	public Player playerPrefab;
	public Box boxPrefab;
	public Player samurai;

	public static bool select;
	public static bool select2;
	public static bool select3;
	public static bool bossSelect;

	private Maze mazeInstance;

	public static Box boxInstance;
	public static Box boxInstance2;
	public static Box boxInstance3;
	public static Player samuraiInstance;
	public static int picked;

	private Vector3 magholder;
	private Vector3 magholder2;
	private Vector3 magholder3;
	private Vector3 magholder4;
	float t = 0.5f;
	bool audio1 = false;
	bool start = false;

	public static Player playerInstance;
	private void Start () {
		StartCoroutine(BeginGame());
		Screen.lockCursor = true;

		select = false;
		select2 = false;
		select3 = false;

		picked = 0;

	}
	private IEnumerator Delay ()
	{
		yield return new WaitForSeconds(this.transform.GetChild (0).audio.clip.length);
	}
	private void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("Scene");
		}
		if (start) {
			if (!this.transform.GetChild (0).audio.isPlaying) {
				t -= Time.deltaTime;
				if (t < 0) {
					this.transform.GetChild (1).audio.Play ();
					start = false;
				}
			}
		}
		if(!(playerInstance==null || boxInstance==null || boxInstance2==null || boxInstance3==null || samuraiInstance==null)) {
			magholder = playerInstance.transform.GetChild (0).transform.position - boxInstance.transform.position;
			magholder2 = playerInstance.transform.GetChild (0).transform.position - boxInstance2.transform.position;
			magholder3 = playerInstance.transform.GetChild (0).transform.position - boxInstance3.transform.position;
			magholder4 = playerInstance.transform.GetChild (0).transform.position - samuraiInstance.transform.position;
			if(magholder.magnitude<1.3 && boxInstance.active) {
				select = true;
				select2 = false;
				select3 = false;
				bossSelect = false;
			} else if(magholder2.magnitude<1.3 && boxInstance2.active) {
				select = false;
				select2 = true;
				select3 = false;
				bossSelect = false;
			} else if(magholder3.magnitude<1.3 && boxInstance3.active) {
				select = false;
				select2 = false;
				select3 = true;
				bossSelect = false;
			} else if (magholder4.magnitude < 1.3 && samuraiInstance.active) {
				select = false;
				select2 = false;
				select3 = false;
				bossSelect = true;
			} else {
				select = false;
				select2 = false;
				select3 = false;
				bossSelect = false;
			}
		}

	}

	private IEnumerator BeginGame () {
		Camera.main.clearFlags = CameraClearFlags.Skybox;
		Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
		mazeInstance = Instantiate(mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
		playerInstance = Instantiate(playerPrefab) as Player;
		boxInstance = Instantiate (boxPrefab) as Box;
		boxInstance2 = Instantiate (boxPrefab) as Box;
		boxInstance3 = Instantiate (boxPrefab) as Box;
		samuraiInstance = Instantiate (samurai) as Player;
		playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
		boxInstance.SetLocation (mazeInstance.GetCell (mazeInstance.RandomCoordinates));
		boxInstance2.SetLocation (mazeInstance.GetCell (mazeInstance.RandomCoordinates));
		boxInstance3.SetLocation (mazeInstance.GetCell (mazeInstance.RandomCoordinates));
		samuraiInstance.SetLocation (mazeInstance.GetCell (mazeInstance.RandomCoordinates));
		this.transform.position = samuraiInstance.transform.position;
		audio.Play ();
		Debug.Log (this.transform.GetChild (0).transform.name);
		this.transform.GetChild (0).audio.Play ();
		start = true;
		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
	}

	private void RestartGame () {
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);
		if (playerInstance != null) {
			Destroy(playerInstance.gameObject);
		}
		StartCoroutine(BeginGame());
	}
}