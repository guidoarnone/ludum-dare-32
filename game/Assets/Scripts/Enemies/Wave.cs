using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour {


	public GameObject[] enemies;
	public float spawnInterval;
	private Queue<GameObject> enemyQueue;

	// Use this for initialization
	public void execute () {
		enemyQueue = new Queue<GameObject> ();
		startSendingEnemies();
	}

	private void startSendingEnemies()
	{
		foreach(GameObject g in enemies)
		{
			enemyQueue.Enqueue(g);
		}
		StartCoroutine(sendEnemy());
	}

	private IEnumerator sendEnemy() {

		GameObject enemy = (GameObject) enemyQueue.Dequeue ();
		Instantiate (enemy, transform.parent.transform.position, Quaternion.identity);

		yield return new WaitForSeconds(spawnInterval);

		if (enemyQueue.Count > 0) 
		{
			StartCoroutine(sendEnemy());
		}

	}

}
