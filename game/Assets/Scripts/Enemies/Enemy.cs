﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public GameObject body;
	public float healthPoints;

	Animator animator;
	bool isAlive;

	// Use this for initialization
	void Start () 
	{
		healthPoints = 100f;
		isAlive = true;
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isAlive)
		{
			Vector3 moveVector = new Vector3(0, 0, -2 * Time.deltaTime);
			transform.LookAt(transform.position + moveVector);
			transform.Translate(moveVector, Space.World);
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag.Substring(0,6) == "attack")
		{
			healthPoints -= weaponDamage(c.tag);

			if(c.tag == "attack_grape")
			{
				c.gameObject.GetComponent<GrapeBullet>().disappear();
			}
		}

		if(healthPoints <= 0f)
		{
			agonize(c);
		}
	}

	private void agonize(Collider c)
	{
		transform.LookAt(c.transform);
		Vector3 rot = transform.rotation.eulerAngles;
		transform.rotation = Quaternion.Euler(0, rot.y, rot.z);

		isAlive = false;
		gameObject.layer = 2;
		animator.SetTrigger("death");
		animator.SetInteger("type", weaponId(c.tag));
		Debug.Log(weaponId(c.tag));
	}
	          
	private float weaponDamage(string tag)
	{
		switch (tag) 
		{
		case "attack_coconut":
			return 1000;
		case "attack_banana":
			return 50;
		case "attack_grape":
			return 20;
		}

		return 0;
	}

	private int weaponId(string tag)
	{
		switch (tag) 
		{
			case "attack_coconut":
				return 0;
			case "attack_banana":
				return 1;
			case "attack_grape":
				return 2;
		}
		return -1;
	}

	public void death()
	{
		StartCoroutine(flicker(0.25f));
	}

	private IEnumerator flicker(float i)
	{
		if (i < 0.1f)
		{
			Destroy(gameObject);
		}

		else
		{
			yield return new WaitForSeconds(i);
			body.GetComponent<SkinnedMeshRenderer>().enabled = !body.GetComponent<SkinnedMeshRenderer>().enabled;
			StartCoroutine(flicker(i * 0.95f));
		}
	}
}