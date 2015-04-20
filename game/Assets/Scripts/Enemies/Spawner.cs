using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public GameObject[] waves;
	public float waveInterval;
	private Queue<GameObject> waveQueue;
	private bool finishedWaves;
	
	// Use this for initialization
	public void Start () {
		finishedWaves = false;
		waveQueue = new Queue<GameObject> ();
		startWaves();
	}
	
	private void startWaves()
	{
		foreach(GameObject w in waves)
		{
			waveQueue.Enqueue(w);
		}
		if (waveQueue.Count > 0) // checks for spawners with no waves 
		{	
			StartCoroutine (startWave ());
		}
	}
	
	private IEnumerator startWave() 
	{

		waveQueue.Dequeue().GetComponent<Wave>().execute();

		yield return new WaitForSeconds(waveInterval);
		
		if (waveQueue.Count > 0) {
			StartCoroutine (startWave ());
		}
		else 
		{
			finishedWaves = true;
		}
		
	}

	public bool hasFinished()
	{
		return finishedWaves;
	}

}
