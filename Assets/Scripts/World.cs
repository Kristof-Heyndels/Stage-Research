using System;
using UnityEngine;

public class World : MonoBehaviour
{

	public GameObject localAvatar;

	private float oldPrimaryStick;
	private float oldSecondaryStick;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		var turnAngle = 0f;

		if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x > 0.8f && oldPrimaryStick < 0.8f)
		{
			turnAngle = 30;
		}else if (oldPrimaryStick > -0.8f && OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x < -0.8f)
		{
			turnAngle = -30;
		}

		if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x > 0.8f && oldSecondaryStick < 0.8f)
		{
			turnAngle = 30;
		}
		else if (oldSecondaryStick > -0.8f && OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x < -0.8f)
		{
			turnAngle = -30;
		}

		oldPrimaryStick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x;
		oldSecondaryStick = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;

		localAvatar.transform.localEulerAngles = new Vector3(localAvatar.transform.localEulerAngles.x, localAvatar.transform.localEulerAngles.y + turnAngle, localAvatar.transform.localEulerAngles.z);
	}
}
