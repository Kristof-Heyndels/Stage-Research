using UnityEngine;

public class PatternSphere : HoloUI
{
	public override void Execute()
	{
		GetComponent<Renderer>().material.color = Color.green;
		var parent = transform.parent.gameObject.GetComponent<PatternLock>();
		if (!parent.attemptSphere.Contains(this))
		{
			PatternLock.SphereHit(this, parent);
		}
	}
}
