using System.Collections.Generic;
using UnityEngine;

public class ColourCubesContainer : MonoBehaviour
{
	public List<GameObject> colourCubes = new List<GameObject>();

	// Use this for initialization
	void Start () {

		for (var j = 0; j < transform.childCount; j++)
		{
			colourCubes.Add(transform.GetChild(j).gameObject);
			if (j > 0)
			{
				transform.GetChild(j).gameObject.GetComponent<Renderer>().material.color = ColourRGBA.colourList[j - 1];
			}
		}
	}
}
