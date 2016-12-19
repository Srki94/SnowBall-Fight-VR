using UnityEngine;
using System.Collections;

public class MaterialChange : MonoBehaviour 
{
	public Material[] mats;
	public GameObject dragonNightmare;



	void Awake () 
	{
		dragonNightmare.GetComponent<Renderer>().material = mats[0];
	}





	public void MatChangeToAlbino ()
	{
		dragonNightmare.GetComponent<Renderer>().material = mats[0];
	}

	public void MatChangeToGreen ()
	{
		dragonNightmare.GetComponent<Renderer>().material = mats[1];
	}

	public void MatChangeToBlue ()
	{
		dragonNightmare.GetComponent<Renderer>().material = mats[2];
	}

	public void MatChangeToDarkblue ()
	{
		dragonNightmare.GetComponent<Renderer>().material = mats[3];
	}
	
}
