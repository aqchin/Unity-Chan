using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {
	private MazeCell currentCell;
	//public SixenseInput.Controller m_controller = null;
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
	}

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

	}
}
