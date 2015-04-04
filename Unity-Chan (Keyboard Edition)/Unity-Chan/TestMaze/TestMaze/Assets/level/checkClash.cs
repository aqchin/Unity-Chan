using UnityEngine;
using System.Collections;

public class checkClash : MonoBehaviour {
	public static int difficulty =0;
	void OnTriggerEnter (Collider col)
	{

		difficulty = int.Parse(col.gameObject.name);
		
		Debug.Log(difficulty);

	}
}
