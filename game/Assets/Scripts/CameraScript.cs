using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform target;
	private Vector3 offset;

	// Use this for initialization
	void Start () 
	{
		offset = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = target.position + offset;
	}
}
