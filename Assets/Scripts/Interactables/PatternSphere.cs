using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PatternSphere : HoloUI
{


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public override void Execute()
	{
		Debug.LogFormat("Interacted: {0}", gameObject);
		GetComponent<Renderer>().material.color = Color.green;
		var parent = transform.parent.gameObject.GetComponent<PatternLock>();
		if (!parent.history.Contains(this))
		{
			PatternLock.SphereHit(this, parent);
		}
	}
}
