using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GameObject[] fruitModels;
	public Material[] fruitMaterials;

	public GameObject[] numbersModels;

	public GameObject rotator;

	public GameObject units;
	public GameObject tens;
	public GameObject hundreds;

	private Transform oldModel;

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

		GameObject tempO = (GameObject)Instantiate(fruitModels[i], transform.position, Quaternion.identity);

		units.GetComponent<Renderer>().material = fruitMaterials[i];
		tens.GetComponent<Renderer>().material = fruitMaterials[i];
		hundreds.GetComponent<Renderer>().material = fruitMaterials[i];

		tempO.transform.SetParent(rotator.transform);
		oldModel = tempO.transform;
	}
}
