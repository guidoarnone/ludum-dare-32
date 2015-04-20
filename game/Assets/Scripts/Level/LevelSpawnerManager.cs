using UnityEngine;
using System.Collections;

public class LevelSpawnerManager : MonoBehaviour {

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
		nextLevel.GetComponent<LevelSpawnerManager>().startLevel();
	}
}
