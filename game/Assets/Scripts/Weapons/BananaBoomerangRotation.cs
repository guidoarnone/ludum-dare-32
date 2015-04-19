using UnityEngine;
using System.Collections;

public class BananaBoomerangRotation : MonoBehaviour {

	private Vector3 rotationAxis;
	public float rotationAngle;

	void Start()
	{
		rotationAxis = new Vector3 (0,1,0);
	}

	void Update ()
	{
		transform.Rotate (rotationAxis, rotationAngle, Space.World);
	}
}
