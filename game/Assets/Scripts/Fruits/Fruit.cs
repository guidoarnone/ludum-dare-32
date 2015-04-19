using UnityEngine;
using System.Collections;

public class Fruit : MonoBehaviour 
{
	public int fruitAmmunitionType;
	public float radius;
	public float fallSpeed;

	public GameObject parentPlant;
	public GameObject particles;
	public float growthTime;

	private bool isHarvestable;
	private bool canPickUp;

	private float speed;
	
	private float startTime;
	private float elapsedTime;

	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;

		canPickUp = false;
		isHarvestable = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isHarvestable == false)
		{
			elapsedTime = Time.time - startTime;
			float scale =  elapsedTime / growthTime;	//Set scale, working as the growth animation
			scale = Mathf.Clamp(scale, 0.1f, 1f);
			transform.localScale = new Vector3(scale, scale, scale);

			if (elapsedTime >= growthTime)
			{
				transform.localScale = Vector3.one;
				prime (); //Display a small particle, enable ability to harvest
			}
		}
	}

	private void prime()
	{
		Instantiate(particles, transform.position, Quaternion.identity);
		isHarvestable = true;
	}

	public void setGrowthTime(float t)
	{
		growthTime = t;
	}

	public void setParentPlant(GameObject g)
	{
		parentPlant = g;
	}

	public void fall()
	{
		if (isHarvestable)
		{
			speed = fallSpeed;
			StartCoroutine(fallCo());
		}
	}

	IEnumerator fallCo()
	{
		speed += fallSpeed;

		if (transform.position.y < 0 + radius)
		{
			Vector3 p = transform.position;
			transform.position = new Vector3(p.x, 0 + radius, p.z);
			canPickUp = true;
			yield return new WaitForSeconds(Time.deltaTime);
		}
		else
		{
			transform.Translate(0, -speed * Time.deltaTime, 0);
			yield return new WaitForSeconds(Time.deltaTime);
			StartCoroutine(fallCo());
		}

	}

	public int pickedUp()
	{
		if (canPickUp)
		{
			parentPlant.GetComponent<Plant>().fruitPicked(gameObject);
			Destroy(gameObject);
			return fruitAmmunitionType;
		}
		else
		{
			return -1;
		}
	}

	public bool pickUpStatus()
	{
		return canPickUp;
	}
}
