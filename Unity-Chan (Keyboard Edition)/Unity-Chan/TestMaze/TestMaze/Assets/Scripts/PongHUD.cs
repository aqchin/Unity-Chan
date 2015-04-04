using UnityEngine;
using System.Collections;

public class PongHUD : MonoBehaviour {

	public GUISkin gskin;
	//public GameObject cube;
	//public bool select;
	int selectedBox = -1;
	private bool show = false;
	private const int ID = 0;
	private Rect rec = new Rect(Screen.width / 2 - 85, Screen.height /2 - 200, 170, 400);
	private string style = "CursedText";
	private int ind = 0;
	private bool sel = false;
	float t = 0;
	bool menu = false;
	
	private bool introShown;
	private float introTime;
	//private float introTime2;
	
	
	// Use this for initialization
	void Start () {
		introShown = true;
		introTime = 2.0f;
		//introTime2 = 10.0f;
	}
	
	// Update is called once per frame
	private void Update () {
		t++;
		t = t % 50;
		/*if (controlScript == null) {
			GameObject unityChan = GameObject.Find ("Unity-Chan(Clone)/unitychan");
			controlScript = (UnityChanControlScriptWithRgidBody) unityChan.GetComponent("UnityChanControlScriptWithRgidBody");
		}*/
		if (Input.GetKeyDown (KeyCode.F) && !show) {
			if (UserPaddle.started)
				BallPhysics.pauseAndResume ();
			UserPaddle.menu = true;
			show = true;
			menu = true;
		} 
		else if(Input.GetKeyDown (KeyCode.W) && show) {
			ind--;
			if(ind < 0)
				ind = 3;
		} 
		else if(Input.GetKeyDown (KeyCode.S) && show) {
			ind++;
			if(ind > 3)
				ind = 0;
		} 
		else if(Input.GetKeyDown (KeyCode.F) && show) {
			if(show)
				sel = true;
		}

		if(introShown) {
			if(introTime < 0.0f) {
				introShown = false;
			} else {
				introTime -= Time.deltaTime;
			}
			if (UserPaddle.started) {
				introShown = false;
			}
		}
	}
	
	void OnGUI() {
		GUI.skin = gskin;
		//GUI.color = Color.yellow;
		//MenuDisplay ();
		if (show) {
			//UserPaddle.isStarted = false;
			rec = GUI.Window (ID, rec, MenuDisplay, "");
			//dResume();
		}
		if (introShown) {
			GUIStyle biggerFonts = new GUIStyle("label");
			biggerFonts.fontSize = 30;
			GUI.Label (new Rect(Screen.width/2 - 200, Screen.height/2 - 100, 400, 200),"Let's D-D-D-Duel! \n Left Click to Start", biggerFonts);
		} 

	}
	
	void MenuDisplay(int ID) {
		if(ind == 0) {
			style = "LegendaryText";
			if(sel) {
				show = false;
				UserPaddle.menu = false;
				sel = false;
				menu = false;
				if (UserPaddle.started)
					BallPhysics.pauseAndResume ();
			}
		}
		GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 - (float)(rec.height * 0.25), 150, 32), "Resume", style);
		/*if (GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 - (float)(rec.height * 0.25), 150, 32), "Return To Game", style)) {
			show = false;
		}*/
		style = "CursedText";
		
		if(ind == 1) {
			style = "LegendaryText";
			if(sel) {
				show = false;
				UserPaddle.menu = false;
				sel = false;
				PaddleAI.difficulty = 1;
				menu = false;
				if (UserPaddle.started)
					BallPhysics.pauseAndResume ();
			}
		}
		GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 - (float)(rec.height * 0.10), 150, 32), "Dummies Difficulty", style);
		/*if (GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 - (float)(rec.height * 0.10), 150, 32), "TROLOLOLOL", style)) {

		}*/
		style = "CursedText";
		
		if(ind == 2) {
			style = "LegendaryText";
			if(sel) {
				sel = false;
				UserPaddle.menu = false;
				show = false;
				PaddleAI.difficulty = 7;
				menu = false;
				if (UserPaddle.started)
					BallPhysics.pauseAndResume ();
			}
		}
		GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 + (float)(rec.height * 0.05), 150, 32), "Medium Difficulty", style);
		/*if (GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 + (float)(rec.height * 0.05), 150, 32), "Hubert is so Hot~", style)) {

		}*/
		style = "CursedText";
		
		if(ind == 3) {
			style = "LegendaryText";
			if(sel) {
				show = false;
				sel = false;
				PaddleAI.difficulty = 9000;
				menu = false;
				if (UserPaddle.started)
					BallPhysics.pauseAndResume ();
			}
		}
		GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 + (float)(rec.height * 0.20), 150, 32), "Impossible Difficulty", style);
		//GUI.HorizontalSlider(new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 + (float)(rec.height * 0.20), 1, 32), 3, 0, 100);
		/*if (GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 + (float)(rec.height * 0.20), 150, 32), "Quit", style)) {
			if(ind == 3)
				style = "LegendaryText";
		}*/
		style = "CursedText";
		
	}
}
