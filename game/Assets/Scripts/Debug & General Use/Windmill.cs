using UnityEngine;
using System.Collections;

public class Windmill : MonoBehaviour {

	public float speed;

	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(transform.up, speed * Time.deltaTime, Space.World);
	}
}
