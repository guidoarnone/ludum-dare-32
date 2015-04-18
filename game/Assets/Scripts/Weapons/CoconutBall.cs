using UnityEngine;
using System.Collections;

public class CoconutBall : MonoBehaviour {

	public float speed;
	private Vector3 direction;
	private float radius;

	void Start()
	{
		radius = 0.25f;
	}

	// Update is called once per frame
	void Update () 
	{
		if (radius < 1f)
		{
			radius += 0.01f;
		}

		transform.localScale = Vector3.one * radius * 4;

		Vector3 pos = transform.position;
		transform.position = new Vector3 (pos.x, radius, pos.z);
		transform.Translate(direction * speed * Time.deltaTime);
	}

	public void setDirection(Vector3 d)
	{
		direction = d;
	}
}
