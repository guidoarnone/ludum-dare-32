using UnityEngine;
using System.Collections;

public class LevelSpawnerManager : MonoBehaviour {

	public GameObject character;

	public float levelDelay;
	public float delay;

	public LevelSpawnerManager 	nextLevel;
	public GameObject[] spawnerList;
	private bool finished; 

	void Start()
	{
		if (name == "Level 1")
		{
			startLevel();
		}
	}

	public void startLevel()
	{
		if (name != "Level 6")
		{
			Invoke("realStartLevel", levelDelay);
		}

		else
		{
			character.GetComponent<Character>().win();
		}
	}

	private void realStartLevel()
	{
		finished = false;
		
		foreach (GameObject G in spawnerList)
		{
			G.GetComponent<Spawner>().startWaves();
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (spawnersFinished() && !anyEnemiesAlive() && !finished) 
		{
			Invoke("endLevel", delay);
			Destroy(gameObject, delay + 1f);
			finished = true;
		}
	}


	public bool anyEnemiesAlive()
	{
		return (GameObject.FindGameObjectsWithTag ("enemies").Length > 0);
	}

	public bool spawnersFinished()
	{
		bool Sfinished = true;

		for (int i = 0; i < spawnerList.Length; i++)
		{
			Sfinished &= spawnerList[i].GetComponent<Spawner>().hasFinished();
		}

		return Sfinished;
		
	}

	public void endLevel() 
	{
		if (name != "Level 6")
		{
			nextLevel.GetComponent<LevelSpawnerManager>().startLevel();
		}
	}
}
