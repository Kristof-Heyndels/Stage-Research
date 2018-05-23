using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearButton : HoloUI {

	public float con1;
	public float con2;

	public override void Execute()
	{
		var con1 = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
		var con2 = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

		if (con1 < 0.5f && 0.5f < this.con1 || con2 < 0.5f && 0.5f < this.con2)
		{
			PinLock.Clear(GetComponentInParent<PinLock>());
		}
		this.con1 = con1;
		this.con2 = con2;
	}
}
