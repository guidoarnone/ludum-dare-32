using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour 
{
	public float speed;
	public float deadZone;

	Vector3 move;

	Animator animator;

	// Use this for initialization
	void Start () 
	{
		animator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		getMovement();

		transform.LookAt(transform.position + move);
		transform.Translate(move * Time.deltaTime * speed, Space.World);
	}

	private void getMovement()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		if (Mathf.Abs(x) < deadZone)
		{
			x = 0;
		}

		if (Mathf.Abs(y) < deadZone)
		{
			y = 0;
		}

		animator.SetFloat("xVel", Mathf.Abs(x) );
		animator.SetFloat("zVel", Mathf.Abs(y) );

		animator.SetFloat("vel", (Mathf.Abs(y) + Mathf.Abs(x)) / 2);

		move = new Vector3(x, 0, y).normalized;
	}
}
