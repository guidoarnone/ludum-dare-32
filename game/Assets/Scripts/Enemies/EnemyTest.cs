using UnityEngine;
using System.Collections;

public class EnemyTest : MonoBehaviour {

	Animator animator;
	bool isAlive;

	// Use this for initialization
	void Start () 
	{
		isAlive = true;
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{


		if (isAlive)
		{
			Vector3 moveVector = new Vector3(0, 0, -2 * Time.deltaTime);
			transform.LookAt(transform.position + moveVector);
			transform.Translate(moveVector, Space.World);
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "attack")
		{
			transform.LookAt(new Vector3(c.transform.position.x, 0, c.transform.position.z));
			isAlive = false;
			animator.SetTrigger("death");
		}
	}

	public void death()
	{
		//Particles or smth
		Destroy(gameObject);
	}
}
