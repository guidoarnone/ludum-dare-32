using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	public float time;

	// Use this for initialization
	void Start () 
	{
		if (time == 0f)
		{
			time = 5f;
		}

		Destroy(gameObject, time);
	}
}
