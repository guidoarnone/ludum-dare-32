using UnityEngine;
using System.Collections;

public class CoconutBall : MonoBehaviour
{

	public float speed;
	public float maxRadius;
	private Vector3 direction;
	private float radius;

	void Start()
	{
		radius = 0.25f;
	}
	
	void Update () 
	{
		if (radius < maxRadius)
		{
			radius += 0.01f;
		}

		transform.localScale = Vector3.one * radius * 4;

		Vector3 pos = transform.position;
		transform.position = new Vector3 (pos.x, radius, pos.z);
		transform.Translate(direction * speed * Time.deltaTime, Space.World);
		transform.Rotate(transform.right, Mathf.Pow(speed, 2.5f) / radius * Time.deltaTime, Space.World);
	}

	public void setDirection(Vector3 d)
	{
		direction = d;
		transform.LookAt(transform.position + d);
	}
}
