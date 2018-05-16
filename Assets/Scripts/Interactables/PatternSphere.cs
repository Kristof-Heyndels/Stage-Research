using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PatternSphere : HoloUI
{
	public override void Execute()
	{
		GetComponent<Renderer>().material.color = Color.green;
		var parent = transform.parent.gameObject.GetComponent<PatternLock>();
		if (!parent.historySphere.Contains(this))
		{
			PatternLock.SphereHit(this, parent);
		}
	}
}
