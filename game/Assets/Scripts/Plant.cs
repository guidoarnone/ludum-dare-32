using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour 
{
	public string type;
	public float growthTime;	//Time required to grow
	public	GameObject[] possibleFruits;	//Positions to place fruit in
	public	Transform[] fruitPosition;	//Positions to place fruit in

	private GameObject[] fruit;	//Actual array of the fruit
	private int	activeFruit;
	private bool isProducing;	//Is it currently producing fruit

	private Animator animator;
	private float startTime;
	private float elapsedTime;

	// Use this for initialization
	void Start () 
	{
		//initialize
		fruit = new GameObject[fruitPosition.Length];

		//getGrowthTime()
		//getactiveSlots()
		//Testing

		activeFruit = 3;

		startTime = Time.time;
		animator = transform.GetComponent<Animator>();	//Get animator component
		animator.speed = 2.5f/growthTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isProducing == false)
		{
			elapsedTime = Time.time - startTime;

			if (elapsedTime >= growthTime && isProducing != true)
			{
				prime ();
			}
		}
	}

	private void prime()
	{
		animator.speed = 1f;
		animator.SetTrigger("Prime");
		isProducing = true;

		//Start fruit producing routine
		Invoke("spawnFruit", 5f);
	}

	private void spawnFruit()
	{
		for (int i = 0; i < activeFruit; i++)
		{
			if(fruit[i] == null)
			{
				GameObject tempFruit = (GameObject)Instantiate(getFruit(), fruitPosition[i].position, Quaternion.identity);
				tempFruit.transform.SetParent(fruitPosition[i]);
				fruit[i] = tempFruit;
			}
		}
	}

	private GameObject getFruit()
	{
		int i = Random.Range(0, possibleFruits.Length);
		return possibleFruits[i];
	}

	public void setGrowthTime(float t)
	{
		growthTime = t;
	}

	public string getType()
	{
		return type;
	}
}
