using UnityEngine;
using System.Collections;

public class PlayerHUD : MonoBehaviour {
	
	public GUISkin gskin;
	//public GameObject cube;
	//public bool select;
	
	public SixenseInput.Controller r_controller = null;
	public SixenseInput.Controller l_controller = null;
	Quaternion oldRot;
	Quaternion newRot;
	Vector3 oldPos;
	Vector3 newPos;
	int selectedBox = -1;
	bool moveMode = false;
	bool rotateMode = false;
	private bool show = false;
	private bool bossShow = false;
	private bool bossQualify = false;
	private const int ID = 0;
	private const int ID1 = 1;
	private Rect rec = new Rect(Screen.width / 2 - 85, Screen.height /2 - 200, 170, 250);
	private string style = "CursedText";
	private int ind = 0;
	private int bossInd = 0;
	bool rot = false;
	float rotx,roty;
	private bool sel = false;
	private bool bossSel = false;
	UnityChanControlScriptWithRgidBody controlScript = null;
	float t = 0;
	bool menu = false;

	private bool introShown;
	private float introTime;
	private float introTime2;

	
	// Use this for initialization
	void Start () {
		introShown = true;
		introTime = 5.0f;
		introTime2 = 10.0f;
	}
	
	// Update is called once per frame
	private void Update () {
		t++;
		t = t % 50;
		if (controlScript == null) {
			GameObject unityChan = GameObject.Find ("Unity-Chan(Clone)/unitychan");
			controlScript = (UnityChanControlScriptWithRgidBody) unityChan.GetComponent("UnityChanControlScriptWithRgidBody");
		}
		rotx = Input.GetAxis ("Vertical");
		roty = Input.GetAxis ("Horizontal");
		if (Input.GetKeyDown(KeyCode.F) && !menu) {
			if(GameManager.select || GameManager.select2 || GameManager.select3) {
				if(GameManager.select) {
					//Debug.Log (GameManager.select + " " + GameManager.select2 + " " + GameManager.select3);
					//GameManager.boxInstance.audio.Play ();
					this.transform.GetChild (2).audio.Play ();
					GameManager.boxInstance.active = false;
				}
				else if(GameManager.select2) {
					//GameManager.boxInstance2.audio.Play ();
					this.transform.GetChild (2).audio.Play ();
					GameManager.boxInstance2.active = false;
				}
				else if(GameManager.select3) {
					//GameManager.boxInstance3.audio.Play ();
					this.transform.GetChild (2).audio.Play ();
					GameManager.boxInstance3.active = false;
				}
				show = false;
				sel = false;
				menu = false;
				controlScript.interacting = false;
				GameManager.picked += 1;
				introShown = true;
				introTime = 5.0f;
				introTime2 = 10.0f;
			} else if(GameManager.bossSelect) {
				menu = true;
				GameManager.samuraiInstance.audio.Play();
				bossShow = true;
			}
		}
		else if(Input.GetKeyDown (KeyCode.W) && menu) {
			if(show) {
				ind--;
				if(ind < 0)
					ind = 1;
			} else if(bossShow) {
				bossInd--;
				if(bossInd < 0)
					bossInd = 1;
			}
		} else if(Input.GetKeyDown (KeyCode.S) && menu) {
			if(show) {
				ind++;
				if(ind > 1)
					ind = 0;
			} else if(bossShow) {
				bossInd++;
				if(bossInd > 1)
					bossInd = 0;
			}
		} else if(Input.GetKeyDown (KeyCode.F) && menu) {
			if(show)
				sel = true;
			else if(bossShow && bossQualify)
				bossSel = true;
			else if(bossShow && !bossQualify) {
				bossShow = false;
				menu = false;
			}
		}/*
		if (rotateMode) {
			//Debug.Log ("ROTATEMODE");
			newRot = r_controller.Rotation;
			//Debug.Log ("Rotating with " + rotation);
			if(GameManager.select) {
				//Debug.Log (GameManager.select + " " + GameManager.select2 + " " + GameManager.select3);
				//GameManager.boxInstance.transform.Translate(new Vector3(0,3,0));
				GameManager.boxInstance.transform.rotation = newRot;
				//GameManager.boxInstance.transform.Translate(new Vector3(0,-3,0));
			}
			else if(GameManager.select2){
				//GameManager.boxInstance2.transform.Translate(new Vector3(0,3,0));
				GameManager.boxInstance2.transform.rotation = newRot;
				//GameManager.boxInstance2.transform.Translate(new Vector3(0,-3,0));
			}
			else if(GameManager.select3){
				//GameManager.boxInstance3.transform.Translate(new Vector3(0,3,0));
				GameManager.boxInstance3.transform.rotation = newRot;
				//GameManager.boxInstance3.transform.Translate(new Vector3(0,-3,0));
			}
			oldRot = newRot;
			if (l_controller.GetButtonDown (SixenseButtons.TWO)) {
				if (selectedBox == 0) {
					GameManager.boxInstance.rigidbody.useGravity = true;
					GameManager.boxInstance.rigidbody.isKinematic = false;
				}
				else if (selectedBox == 1) {
					GameManager.boxInstance2.rigidbody.useGravity = true;
					GameManager.boxInstance2.rigidbody.isKinematic = false;
				}
				else if (selectedBox == 2){
					GameManager.boxInstance3.rigidbody.useGravity = true;
					GameManager.boxInstance3.rigidbody.isKinematic = false;
				}
				show = true;
				rotateMode = false;
				selectedBox = -1;
			}
		}
		if (moveMode) {
			Debug.Log ("MoveMODE");
			newPos = r_controller.Position;
			Vector3 pos = newPos - oldPos;
			pos = pos * 0.01f;
			Debug.Log ("Moving with " + pos);
			if(selectedBox == 0) {
				//Debug.Log (GameManager.select + " " + GameManager.select2 + " " + GameManager.select3);
				//GameManager.boxInstance.transform.Translate(new Vector3(0,3,0));
				Debug.Log ("1");
				GameManager.boxInstance.transform.Translate (pos,Space.World);
				//GameManager.boxInstance.transform.Translate(new Vector3(0,-3,0));
			}
			else if(selectedBox == 1){
				//GameManager.boxInstance2.transform.Translate(new Vector3(0,3,0));
				Debug.Log ("2");
				GameManager.boxInstance2.transform.Translate (pos,Space.World);
				//GameManager.boxInstance2.transform.Translate(new Vector3(0,-3,0));
			}
			else if(selectedBox == 2){
				//GameManager.boxInstance3.transform.Translate(new Vector3(0,3,0));
				Debug.Log ("3");
				GameManager.boxInstance3.transform.Translate (pos,Space.World);
				//GameManager.boxInstance3.transform.Translate(new Vector3(0,-3,0));
			}
			oldPos = newPos;
			if (l_controller.GetButtonDown (SixenseButtons.TWO)) {
				if (selectedBox == 0) {
					GameManager.boxInstance.rigidbody.useGravity = true;
					GameManager.boxInstance.rigidbody.isKinematic = false;
				}
				else if (selectedBox == 1) {
					GameManager.boxInstance2.rigidbody.useGravity = true;
					GameManager.boxInstance2.rigidbody.isKinematic = false;
				}
				else if (selectedBox == 2){
					GameManager.boxInstance3.rigidbody.useGravity = true;
					GameManager.boxInstance3.rigidbody.isKinematic = false;
				}
				show = true;
				moveMode = false;
				selectedBox = -1;
			}
		}*/
		if(introShown) {
			if(GameManager.picked == 3) {
				if(introTime2 < 0.0f) {
					introShown = false;
				} else {
					introTime2 -= Time.deltaTime;
				}
			}
			else {
				if(introTime < 0.0f) {
					introShown = false;
				} else {
					introTime -= Time.deltaTime;
				}
			}
		}
	}
	
	void OnGUI() {
		GUI.skin = gskin;
		//GUI.color = Color.yellow;
		//MenuDisplay ();
		if (show) {
			rec = GUI.Window (ID, rec, MenuDisplay, "");
		} else if(bossShow && bossQualify) {
			rec = GUI.Window (ID1, rec, MenuDisplay2, "");
		}
		if (GameManager.picked > 0 && GameManager.picked <3 && introShown) {
			GUIStyle biggerFonts = new GUIStyle("label");
			biggerFonts.fontSize = 40;
			GUI.Label (new Rect(Screen.width/2 - 200, Screen.height/2 - 100, 400, 200),"You have picked up " + GameManager.picked + "/3 paddle piecess", biggerFonts);
		} else if (GameManager.picked == 3 && introShown) {
			GUIStyle biggerFonts = new GUIStyle("label");
			biggerFonts.fontSize = 30;
			GUI.Label (new Rect(Screen.width/2 - 300, Screen.height/2 - 200, 600, 400),"Congratulations! You have collected all 3  LEGENDARY paddle pieces, now go battle the ALMIGHTY Jurgen-sensei-sama-dono!!!",biggerFonts);
		}

		if(bossShow) {
			if(GameManager.picked == 0) {
				GUIStyle biggerFonts = new GUIStyle("label");
				biggerFonts.fontSize = 40;
				GUI.Label (new Rect(Screen.width/2 - 200, Screen.height/2 - 100, 400, 200),"...");
			} else if(GameManager.picked == 1) {
				GUIStyle biggerFonts = new GUIStyle("label");
				biggerFonts.fontSize = 40;
				GUI.Label (new Rect(Screen.width/2 - 200, Screen.height/2 - 100, 400, 200),"You are not strong enough... Come find me when you're ready.");
			} else if(GameManager.picked == 2) {
				GUIStyle biggerFonts = new GUIStyle("label");
				biggerFonts.fontSize = 40;
				GUI.Label (new Rect(Screen.width/2 - 200, Screen.height/2 - 100, 400, 200),"You are almost there... I'll be waiting.");
			} else if(GameManager.picked == 3) {
				controlScript.interacting = true;
				bossQualify = true;
			}
		}
	}
	
	void MenuDisplay(int ID) {
		if(ind == 0) {
			style = "LegendaryText";
			if(sel) {
				show = false;
				sel = false;
				controlScript.interacting = false;
				menu = false;
			}
		}
		GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 - (float)(rec.height * 0.1), 150, 32), "Return To Game", style);
		/*if (GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 - (float)(rec.height * 0.25), 150, 32), "Return To Game", style)) {
			show = false;
		}*/
		style = "CursedText";
		
		if(ind == 1) {
			style = "LegendaryText";
			if(sel) {
				if(GameManager.select) {
					//Debug.Log (GameManager.select + " " + GameManager.select2 + " " + GameManager.select3);
					//GameManager.boxInstance.audio.Play ();
					this.transform.GetChild (2).audio.Play ();
					GameManager.boxInstance.active = false;
				}
				else if(GameManager.select2) {
					//GameManager.boxInstance2.audio.Play ();
					this.transform.GetChild (2).audio.Play ();
					GameManager.boxInstance2.active = false;
				}
				else if(GameManager.select3) {
					//GameManager.boxInstance3.audio.Play ();
					this.transform.GetChild (2).audio.Play ();
					GameManager.boxInstance3.active = false;
				}
				show = false;
				sel = false;
				menu = false;
				controlScript.interacting = false;
				GameManager.picked += 1;
				introShown = true;
				introTime = 5.0f;
				introTime2 = 10.0f;
			}
		}
		GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 - (float)(rec.height * -0.1), 150, 32), "Pickup", style);
		/*if (GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 - (float)(rec.height * 0.10), 150, 32), "TROLOLOLOL", style)) {

		}*/
		/*
		style = "CursedText";
		
		if(ind == 2) {
			style = "LegendaryText";
			if(sel) {

				if(GameManager.select) {
					//Debug.Log (GameManager.select + " " + GameManager.select2 + " " + GameManager.select3);
					GameManager.boxInstance.transform.Rotate(0,45,0);
				}
				else if(GameManager.select2)
					GameManager.boxInstance2.transform.Rotate(0,45,0);
				else if(GameManager.select3)
					GameManager.boxInstance3.transform.Rotate(0,45,0);

				sel = false;
				show = false;
				rotateMode = true;
				if (GameManager.select) {
					GameManager.boxInstance.rigidbody.useGravity = false;
					GameManager.boxInstance.rigidbody.isKinematic = true;
					selectedBox = 0;
				}
				else if (GameManager.select2) {
					GameManager.boxInstance2.rigidbody.useGravity = false;
					GameManager.boxInstance2.rigidbody.isKinematic = true;
					selectedBox = 1;
				}
				else if (GameManager.select3){
					GameManager.boxInstance3.rigidbody.useGravity = false;
					GameManager.boxInstance3.rigidbody.isKinematic = true;
					selectedBox = 2;
				}
			}
		}
		GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 + (float)(rec.height * 0.05), 150, 32), "Rotate", style);
		/*if (GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 + (float)(rec.height * 0.05), 150, 32), "Hubert is so Hot~", style)) {

		}*/
		/*
		style = "CursedText";
		
		if(ind == 3) {
			style = "LegendaryText";
			if(sel) {
				oldPos = r_controller.Position;
				moveMode = true;
				show = false;
				sel = false;
				if (GameManager.select) {
					GameManager.boxInstance.rigidbody.useGravity = false;
					GameManager.boxInstance.rigidbody.isKinematic = true;
					selectedBox = 0;
				}
				else if (GameManager.select2) {
					GameManager.boxInstance2.rigidbody.useGravity = false;
					GameManager.boxInstance2.rigidbody.isKinematic = true;
					selectedBox = 1;
				}
				else if (GameManager.select3){
					GameManager.boxInstance3.rigidbody.useGravity = false;
					GameManager.boxInstance3.rigidbody.isKinematic = true;
					selectedBox = 2;
				}
			}
		}
		GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 + (float)(rec.height * 0.20), 150, 32), "Move", style);
		/*if (GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 + (float)(rec.height * 0.20), 150, 32), "Quit", style)) {
			if(ind == 3)
				style = "LegendaryText";
		}*/
		style = "CursedText";
		//GUI.Box (new Rect(Screen.width / 2 - 85, Screen.height /2 - 200, 170, 400), "");

	}

	void MenuDisplay2(int ID1) {
		GUIStyle biggerFonts = new GUIStyle("label");
		biggerFonts.fontSize = 10;
		GUI.Label (new Rect(rec.width/2 - (float)(rec.width * 0.45), rec.height/2 - 150, 150, 90),"Are you ready to battle me?");
		if(bossInd == 0) {
			style = "LegendaryText";
			if(bossSel) {

				bossShow = false;
				bossSel = false;
				controlScript.interacting = false;
				Application.LoadLevel("pong");

			}
		}
		GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 - (float)(rec.height * 0.1), 150, 32), "Yes", style);
		/*if (GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 - (float)(rec.height * 0.25), 150, 32), "Return To Game", style)) {
			show = false;
		}*/
		style = "CursedText";
		
		if(bossInd == 1) {
			style = "LegendaryText";
			if(bossSel) {
				bossShow = false;
				bossSel = false;
				controlScript.interacting = false;
				menu = false;
			}
		}
		GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 + (float)(rec.height * 0.15), 150, 32), "No", style);
		/*if (GUI.Button (new Rect (rec.width / 2 - (float)(rec.width * 0.25), rec.height / 2 - (float)(rec.height * 0.10), 150, 32), "TROLOLOLOL", style)) {

		}*/
		style = "CursedText";

	}
}