using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class EndDemo : MonoBehaviour
{
	private float oldPosY;
	private float posY;
	private bool finished;

	// Use this for initialization
	void Start()
	{
		finished = false;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void FinishDemo()
	{
		if (finished) return;
		var newLine = string.Format("\n{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12}",
			World.testID,
			World.buttonCounter,
			World.scannerCounter,
			World.dropdownTimers[0],
			World.dropdownTimers[1],
			World.dropdownTimers[2],
			World.dropdownTimers[3],
			World.dropdownTimers[4],
			World.colourCubeTimers[0],
			World.colourCubeTimers[1],
			World.colourCubeTimers[2],
			World.colourCubeTimers[3],
			World.colourCubeTimers[4]
			);
		File.AppendAllText(World.results, newLine);

		finished = true;
	}
}
