using UnityEngine;
using System.Collections;

public class GrapeBullet : MonoBehaviour {

	public float speed;
	private Vector3 direction;

	void Update () 
	{
		float deltaX = speed * Time.deltaTime;
		transform.Translate (direction*deltaX, Space.World);
	}

	public void setDirection(Vector3 d)
	{
		direction = d;
		transform.LookAt(transform.position + d);
	}

}
