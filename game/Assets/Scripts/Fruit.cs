using UnityEngine;
using System.Collections;

public class Fruit : MonoBehaviour 
{

	public GameObject particles;
	public float growthTime;

	private bool isHarvestable;
	
	private Animator animator;
	private float startTime;
	private float elapsedTime;
	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
		animator = transform.GetComponent<Animator>();
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

			if (elapsedTime >= growthTime && isHarvestable != true)
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
}
