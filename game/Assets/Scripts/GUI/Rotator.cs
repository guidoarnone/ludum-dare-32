using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	
	public float rotationSpeed;

	// Update is called once per frame
	void Update ()
	{
		transform.Rotate(Vector3.up ,rotationSpeed * Time.deltaTime);
	}
}
