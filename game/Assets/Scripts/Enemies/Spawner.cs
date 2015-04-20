using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

	public int lane;
	public GameObject[] waves;
	public float waveInterval;
	private bool finishedWaves;
	private int currentWave;

	public void startWaves()
	{
		finishedWaves = false;
		currentWave = 0;

		if (currentWave < waves.Length) // checks for spawners with no waves 
		{	
			startWave();
		}
	}

	public void nextWave()
	{
		Invoke("startWave", waveInterval);
	}

	public void startWave()
	{
		if (!(currentWave < waves.Length)) 
		{
			finishedWaves = true;
		}
		else
		{
			waves[currentWave].GetComponent<Wave>().execute();
			currentWave++;
		}
	}

	public bool hasFinished()
	{
		return finishedWaves;
	}

}
