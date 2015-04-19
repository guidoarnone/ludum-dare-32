using UnityEngine;
using System.Collections;

public class Plant : MonoBehaviour 
{
	public string type;
	public float growthTime;	//Time required to grow
	public	GameObject[] possibleFruits;	//Positions to place fruit in
	public	Transform[] fruitPosition;	//Positions to place fruit in

	public GameObject[] fruit;	//Actual array of the fruit
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
		Invoke("spawnFruits", 5f);
	}

	private void spawnFruits()
	{
		for (int i = 0; i < activeFruit; i++)
		{
			StartCoroutine(spawnFruit(i));
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

	IEnumerator spawnFruit(int fruitIndex)
	{
		yield return new WaitForSeconds(Random.Range(growthTime / 2, growthTime * 2));
		if (fruit [fruitIndex] == null) 
		{
			GameObject tempFruit = (GameObject)Instantiate(getFruit(), fruitPosition[fruitIndex].position, Quaternion.identity);
			tempFruit.transform.SetParent(fruitPosition[fruitIndex]);
			tempFruit.GetComponent<Fruit>().setParentPlant(gameObject);
			fruit[fruitIndex] = tempFruit;
		}

	}

	public void harvest()
	{
		int tH = Random.Range (0, activeFruit) + activeFruit / 2;
		Debug.Log(tH);

		int h = 0;

		for (int i = 0; i < fruit.Length && h < tH; i++)
		{
			if (fruit[i] != null && !fruit[i].GetComponent<Fruit>().pickUpStatus())
			{
				fruit[i].GetComponent<Fruit>().fall();
				h++;
			}
		}
	}

	public void fruitPicked(GameObject harvestedFruit)
	{
		int index = 0;
		bool done = false;

		while (index < fruit.Length && !done) 
		{
			//we need explicitly the same referenced object in memory,
			//not a logically equal one.
			if( harvestedFruit.Equals(fruit[index]) )
			{
				fruit[index] = null;
				StartCoroutine(spawnFruit(index));
				done = true;
			}
			index++;
		}

	}

}
