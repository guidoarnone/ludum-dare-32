using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public int damage;
	public float speed;

	public GameObject character;

	public GameObject body;

	public GameObject vest;
	public GameObject hat;

	public GameObject vestReference;
	public GameObject hatReference;

	public float healthPoints;
	public float[] checkpoints;
	private GameObject[] clothing;
	private int currentCheckpoint;

	Animator animator;
	bool isAlive;

	// Use this for initialization
	void Start () 
	{
		currentCheckpoint = 0;
		isAlive = true;
		animator = GetComponent<Animator>();
		clothing = new GameObject[checkpoints.Length];
		instantiateClothing();
	}

	private void instantiateClothing()
	{
		int clothingSize = 0;

		if (vest != null)
		{
			GameObject tempCloth = (GameObject)Instantiate(vest, vestReference.transform.position, Quaternion.identity);
			tempCloth.transform.SetParent(vestReference.transform);
			tempCloth.transform.localScale = Vector3.one;
			clothing[clothingSize] = tempCloth;

			if (name.Substring(0, 4) == "Lord")
			{
				tempCloth.transform.localPosition = new Vector3(-1.1f, 0, 0);
			}

			clothingSize++;
		}

		if (hat != null)
		{
			GameObject tempCloth = (GameObject)Instantiate(hat, hatReference.transform.position, Quaternion.identity);
			tempCloth.transform.SetParent(hatReference.transform);
			tempCloth.transform.localScale = Vector3.one;
			clothing[clothingSize] = tempCloth;
			clothingSize++;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (isAlive)
		{
			Vector3 moveVector = new Vector3(0, 0, -speed * Time.deltaTime);
			transform.LookAt(transform.position + moveVector);
			transform.Translate(moveVector, Space.World);
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag.Length > 6 && c.tag.Substring(0,6) == "attack")
		{
			healthPoints -= weaponDamage(c.tag);

			if(c.tag == "attack_grape")
			{
				c.gameObject.GetComponent<GrapeBullet>().disappear();
			}

			checkDamage();
		}

		if (c.tag == "goal")
		{
			gameObject.layer = 2;
			animator.SetTrigger("goal");
			isAlive = false;
		}

		if(healthPoints <= 0f)
		{
			agonize(c);
		}
	}

	private void checkDamage()
	{
		if (currentCheckpoint < checkpoints.Length)
		{
			if (healthPoints <= checkpoints[currentCheckpoint])
			{
				undress();
				currentCheckpoint++;
				checkDamage();
			}
		}
	}

	private void undress()
	{
		GameObject tempCloth = clothing[currentCheckpoint];
		tempCloth.transform.SetParent(null);
		tempCloth.AddComponent<Rigidbody>();
		tempCloth.GetComponent<Rigidbody>().AddForce(Vector3.one * Random.Range(-500f, 500f));
		tempCloth.AddComponent<AutoDestroy>();
	}

	private void agonize(Collider c)
	{
		transform.LookAt(c.transform);
		Vector3 rot = transform.rotation.eulerAngles;
		transform.rotation = Quaternion.Euler(0, rot.y, rot.z);

		isAlive = false;
		gameObject.layer = 2;
		animator.SetTrigger("death");
		animator.SetInteger("type", weaponId(c.tag));
	}
	          
	private float weaponDamage(string tag)
	{
		switch (tag) 
		{
		case "attack_coconut":
			return 200;
		case "attack_banana":
			return 20;
		case "attack_grape":
			return 20;
		}

		return 0;
	}

	private int weaponId(string tag)
	{
		switch (tag) 
		{
			case "attack_coconut":
				return 0;
			case "attack_banana":
				return 1;
			case "attack_grape":
				return 2;
		}
		return -1;
	}

	public void goal()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().hurt(damage);
		Destroy(gameObject);
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
