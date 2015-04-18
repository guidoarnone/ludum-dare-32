using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform target;
	public 	float min;
	public 	float max;
	public 	float smoothFactor;

	private Vector3 offset;
	
	float desiredSize;


	// Use this for initialization
	void Start () 
	{
		offset = transform.position;

		desiredSize = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = target.position + offset;

		float mousewheel = Input.GetAxis("Mouse ScrollWheel");

		desiredSize += mousewheel * -10;
		desiredSize = Mathf.Clamp(desiredSize, min, max);

		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, desiredSize, 1 / smoothFactor);
	}
}
