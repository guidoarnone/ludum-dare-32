using UnityEngine;
using System.Collections;

public class FruitDestroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			gameObject.GetComponent<Fruit>().harvested();
		}
	}
}
