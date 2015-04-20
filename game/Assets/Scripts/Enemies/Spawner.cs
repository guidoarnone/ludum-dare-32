using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public GameObject[] waves;
	public float waveInterval;
	private bool finishedWaves;
	private int currentWave;

	// Use this for initialization
	void Start () {
		finishedWaves = false;
		currentWave = 0;
		//startWaves();
	}
	
	public void startWaves()
	{
		if (currentWave < waves.Length) // checks for spawners with no waves 
		{	
			StartCoroutine (startWave ());
		}
	}
	
	private IEnumerator startWave()
	{
		waves[currentWave].GetComponent<Wave>().execute();
		currentWave++;

		yield return new WaitForSeconds(waveInterval);
		
		if (currentWave < waves.Length) 
		{
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
