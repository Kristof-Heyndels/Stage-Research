using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PatternSphere : MonoBehaviour
{


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartUsing(VRTK_InteractUse currentUsingObject = null)
	{
		Debug.LogFormat("Interacted: {0}", currentUsingObject);
	}
}
