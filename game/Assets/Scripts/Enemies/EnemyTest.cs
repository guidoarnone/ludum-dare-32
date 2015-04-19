using UnityEngine;
using System.Collections;

public class EnemyTest : MonoBehaviour {

	public GameObject body;

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
		StartCoroutine(flicker(0.25f));
	}

	private IEnumerator flicker(float i)
	{
		if (i < 0.1f)
		{
			Destroy(gameObject);
		}

		else
		{
			yield return new WaitForSeconds(i);
			body.GetComponent<SkinnedMeshRenderer>().enabled = !body.GetComponent<SkinnedMeshRenderer>().enabled;
			StartCoroutine(flicker(i * 0.95f));
		}
	}
}
