﻿using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public Transform target;
	public Camera guiCamera;

	public 	float min;
	public 	float max;
	public 	float smoothFactor;

	public 	float shakeFactor;

	private Vector3 offset;
	private Vector3 shakeVector;
	
	float desiredSize;


	// Use this for initialization
	void Start () 
	{
		offset = transform.position;
		shakeVector = Vector3.zero;

		desiredSize = Camera.main.orthographicSize;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = target.position + offset + shakeVector;

		float mousewheel = Input.GetAxis("Mouse ScrollWheel");

		desiredSize += mousewheel * -10;
		desiredSize = Mathf.Clamp(desiredSize, min, max);

		Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, desiredSize, 1 / smoothFactor);
	}

	void OnGUI()
	{
		guiCamera.Render();
	}

	public void shake(float i)
	{
		StartCoroutine(continueShaking(i));
	}

	IEnumerator continueShaking(float i)
	{
		if (i > 0.1f)
		{
			i *= shakeFactor;
			shakeVector = new Vector3(Random.Range(-i / 10, i / 10), Random.Range(-i / 10, i / 10), Random.Range(-i / 10, i / 10));

			yield return new WaitForSeconds(Time.deltaTime);
			StartCoroutine(continueShaking(i));
		}

		yield return new WaitForSeconds(Time.deltaTime);
	}
}
