using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour {


	public GameObject[] enemies;
	public float spawnInterval;
	private int currentEnemy;

	// Use this for initialization
	public void execute () 
	{
		currentEnemy = 0;
		startSendingEnemies();
	}

	private void startSendingEnemies()
	{
		StartCoroutine(sendEnemy());
	}

	private IEnumerator sendEnemy() 
	{
		GameObject enemy = enemies[currentEnemy];
		Instantiate (enemy, transform.position, Quaternion.identity);

		currentEnemy++;

		yield return new WaitForSeconds(spawnInterval);

		if (currentEnemy < enemies.Length) 
		{
			StartCoroutine(sendEnemy());
		}

		else
		{
			StartCoroutine(finishedSpawning());
		}

	}

	private IEnumerator finishedSpawning()
	{
		yield return new WaitForSeconds(Time.deltaTime);

		if (anyEnemiesAlive())
		{
			StartCoroutine(finishedSpawning());
		}
		else
		{
			transform.parent.GetComponent<Spawner>().nextWave();
		}
	}

	public bool anyEnemiesAlive()
	{
		return (GameObject.FindGameObjectsWithTag ("enemies").Length > 0);
	}

}
