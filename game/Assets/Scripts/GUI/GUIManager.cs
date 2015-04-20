using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public Camera GUICamera;

	public GameObject[] fruitModels;
	public Material[] fruitMaterials;

	public GameObject[] numbersModels;

	public GameObject rotator;

	public GameObject units;
	public GameObject tens;
	public GameObject hundreds;

	private Transform oldModel;

	public void updatePosition()
	{
		transform.Translate(-Screen.width / 1000f * 2.5f / GUICamera.orthographicSize, Screen.height / 1000f * 2.25f / GUICamera.orthographicSize, 5);
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
}
