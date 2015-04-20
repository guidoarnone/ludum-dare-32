using UnityEngine;
using System.Collections;

public class LevelSpawnerManager : MonoBehaviour {


	public LevelSpawnerManager 	nextLevel;
	public GameObject[] spawnerList;
	
	void Start()
	{
		if (name == "Level 1")
		{
			startLevel();
		}
	}

	public void startLevel()
	{
		foreach (GameObject G in spawnerList)
		{
			G.GetComponent<Spawner>().startWaves();
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (spawnersFinished() && !anyEnemiesAlive()) 
		{
			Invoke("endLevel", 5f);
		}
	}


	public bool anyEnemiesAlive()
	{
		return (GameObject.FindGameObjectsWithTag ("enemies").Length > 0);
	}

	public bool spawnersFinished()
	{

		bool finished = true;

		foreach(GameObject spawner in spawnerList)
		{
			finished &= spawner.GetComponent<Spawner>().hasFinished();
		}

		return finished;
		
	}

	public void endLevel() 
	{
		//TODO: level transition
		nextLevel.GetComponent<LevelSpawnerManager>().startLevel();
	}
}
