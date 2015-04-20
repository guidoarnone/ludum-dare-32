using UnityEngine;
using System.Collections;

public class LevelSpawnerManager : MonoBehaviour {


	public GameObject[] spawnerList;
	

	// Update is called once per frame
	void Update () 
	{
		
		if (spawnersFinished() && !anyEnemiesAlive()) 
		{
			endLevel();
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
		Debug.Log ("finished");
	}
}
