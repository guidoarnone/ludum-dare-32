using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public Camera GUICamera;

	public GameObject[] fruitModels;
	public Material[] fruitMaterials;

	public GameObject[] numbersModels;

	public GameObject rotator;
	public GameObject lostText;
	public GameObject wonText;

	public GameObject heart;

	public GameObject liveUnits;
	public GameObject liveTens;

	public GameObject units;
	public GameObject tens;
	public GameObject hundreds;

	private bool lost;
	private bool won;

	private Transform oldModel;

	void Update()
	{
		if (lost)
		{
			lostText.transform.localPosition = Vector3.Lerp(lostText.transform.localPosition, new Vector3(lostText.transform.position.x, 0, lostText.transform.position.z), 0.8f);
		}

		if (won)
		{
			wonText.transform.localPosition = Vector3.Lerp(wonText.transform.localPosition, new Vector3(wonText.transform.position.x, 0, lostText.transform.position.z), 0.8f);
		}
	}

	void Start()
	{
		lost = false;
	}

	public void updatePosition()
	{
		transform.localPosition = new Vector3(-Screen.width / 1000f * 2.5f / GUICamera.orthographicSize, Screen.height / 1000f * 2.25f / GUICamera.orthographicSize, 5);
		heart.transform.localPosition = new Vector3 (Screen.width / 1000f * 2.5f / GUICamera.orthographicSize, Screen.height / 1000f * 2.25f / GUICamera.orthographicSize, 5);
	}

	public void updateLives(int i)
	{
		i = Mathf.Clamp(i, 0, 99);
		
		int u = i % 10;
		int t = (i / 10) % 10;
		
		liveUnits.GetComponent<MeshFilter>().sharedMesh = numbersModels[u].GetComponent<MeshFilter>().sharedMesh;
		liveTens.GetComponent<MeshFilter>().sharedMesh = numbersModels[t].GetComponent<MeshFilter>().sharedMesh;
	}

	public void updateAmmo(int i)
	{
		i = Mathf.Clamp(i, 0, 999);

		int u = i % 10;
		int t = (i / 10) % 10;
		int h = i / 100;

		units.GetComponent<MeshFilter>().sharedMesh = numbersModels[u].GetComponent<MeshFilter>().sharedMesh;
		tens.GetComponent<MeshFilter>().sharedMesh = numbersModels[t].GetComponent<MeshFilter>().sharedMesh;
		hundreds.GetComponent<MeshFilter>().sharedMesh = numbersModels[h].GetComponent<MeshFilter>().sharedMesh;
	}

	public void updateWeapon(int i)
	{
		if (oldModel != null)
		{
			Destroy(oldModel.gameObject);
		}

		GameObject tempO = (GameObject)Instantiate(fruitModels[i], rotator.transform.position, Quaternion.identity);

		units.GetComponent<Renderer>().material = fruitMaterials[i];
		tens.GetComponent<Renderer>().material = fruitMaterials[i];
		hundreds.GetComponent<Renderer>().material = fruitMaterials[i];

		tempO.transform.SetParent(rotator.transform);
		tempO.transform.localPosition = Vector3.zero;

		oldModel = tempO.transform;
	}

	public void losing ()
	{
		lost = true;
	}

	public void winning ()
	{
		won = true;
	}
}
