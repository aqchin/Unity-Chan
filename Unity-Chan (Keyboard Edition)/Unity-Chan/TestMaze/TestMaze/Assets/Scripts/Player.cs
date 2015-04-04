using UnityEngine;

public class Player : MonoBehaviour {

	private MazeCell currentCell;
	public SixenseInput.Controller m_controller = null;
	private MazeDirection currentDirection;
	private bool start = true;
	private float cx,cy,cz,ix,iy,iz;

	public void SetLocation (MazeCell cell) {
		if (currentCell != null) {
			currentCell.OnPlayerExited();
		}
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
		currentCell.OnPlayerEntered();
	}/*

	private void Move (MazeDirection direction) {
		MazeCellEdge edge = currentCell.GetEdge(direction);
		if (edge is MazePassage) {
			SetLocation(edge.otherCell);
		}
	}

	private void Look (MazeDirection direction) {
		transform.localRotation = direction.ToRotation();
		currentDirection = direction;
	}

	private void Update () {
		/*
		if ( m_controller == null )
		{
			m_controller = SixenseInput.GetController( SixenseHands.RIGHT );
		}
		if (start) {
			start = false;
			ix  = m_controller.Position.x;
			iy  = m_controller.Position.y;
			iz  = m_controller.Position.z;
			Debug.Log (ix +" " +iy+" " +iz);
		}
		cx  = m_controller.Position.x;
		cy  = m_controller.Position.y;
		cz  = m_controller.Position.z;
		if (Mathf.Abs (cx - ix) > 90)
				Debug.Log ("x");
		if (Mathf.Abs(cy-iy)>90)
			Debug.Log ("y");
		if (Mathf.Abs(cz-iz)>90)
			Debug.Log ("z");
		if (m_controller.GetButton (SixenseButtons.ONE)) {
			Move(currentDirection);
		}
		if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
			Move(currentDirection);
		}
		else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
			Move(currentDirection.GetNextClockwise());
		}
		else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
			Move(currentDirection.GetOpposite());
		}
		else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
			Move(currentDirection.GetNextCounterclockwise());
		}
		else if (Input.GetKeyDown(KeyCode.Q)) {
			Look(currentDirection.GetNextCounterclockwise());
		}
		else if (Input.GetKeyDown(KeyCode.E)) {
			Look(currentDirection.GetNextClockwise());
			
		}


	}
	*/
}