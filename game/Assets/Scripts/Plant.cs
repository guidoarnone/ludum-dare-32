using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plant : MonoBehaviour 
{

	private GameObject[] Fruit;	//Actual array of the fruit
	private float growthTime;	//Time required to grow
	private bool isProducing;	//Is it currently producing fruit

	private Animator animator;
	private float startTime;
	private float elapsedTime;

	// Use this for initialization
	void Start () 
	{
		startTime = Time.time;
		animator = transform.GetComponent<Animator>();	//Get animator component
	}
	
	// Update is called once per frame
	void Update () 
	{
		elapsedTime = Time.time - startTime;

		if (elapsedTime >= growthTime && isProducing != true)
		{
			prime ();	//Display a small animation, followed by the production of fruit
		}
	}

	protected virtual void prime()
	{
		//isProducing = true;
	}

	public void setGrowthTime(float t)
	{
		growthTime = t;
	}
}
