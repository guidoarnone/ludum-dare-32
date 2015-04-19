using UnityEngine;
using System.Collections;

public class Bouncy : MonoBehaviour 
{
	public float offset;

	private Vector3 startPosition;

	void Start()
	{
		startPosition = transform.position;
	}

	// Update is called once per frame
	void Update () 
	{
		Quaternion rot = new Quaternion();
		rot.eulerAngles = new Vector3(0, Mathf.Sin(Time.time) * 35, 0);
		transform.rotation  = rot;
	}
}
