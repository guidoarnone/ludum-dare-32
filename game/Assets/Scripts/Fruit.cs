using UnityEngine;
using System.Collections;

public class Fruit : MonoBehaviour 
{

	private float growthTime;
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
		elapsedTime = Time.time - startTime;
		
		if (elapsedTime >= growthTime && isHarvestable != true)
		{
			prime (); //Display a small animation, followed by the ability to harvest
		}
	}

	protected virtual void prime()
	{
		//isHarvestable = true;
	}

	public void setGrowthTime(float t)
	{
		growthTime = t;
	}
}
