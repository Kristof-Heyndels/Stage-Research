﻿using UnityEngine;

public class World : MonoBehaviour
{

	public enum Handedness
	{
		Right,
		Left
	}


	public Handedness handedness;
	public GameObject cameraRig;
	public GameObject playArea;
	public GameObject holoPanel;

	public GameObject avatarGrabberLeft;
	public GameObject avatarGrabberRight;
	public GameObject settings;

	public static int scannerCounter = 0;
	public static int buttonCounter = 0;

	private float oldPrimaryStick;
	private float oldSecondaryStick;

	// Use this for initialization
	void Start()
	{
		settings.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		//NOTE(Kristof): Rotating with the joysticks
		if (!settings.activeSelf)
		{
			var turnAngle = 0f;

			if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x > 0.8f && oldPrimaryStick < 0.8f)
			{
				turnAngle = 30;
			}
			else if (oldPrimaryStick > -0.8f && OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).x < -0.8f)
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
			cameraRig.transform.localEulerAngles = new Vector3(cameraRig.transform.localEulerAngles.x,
				cameraRig.transform.localEulerAngles.y + turnAngle, cameraRig.transform.localEulerAngles.z);
		}

		//NOTE(Kristof): Grabbing the holoPanel
		{
			if (handedness == Handedness.Right)
			{
				//NOTE(Kristof): holoPanel child left hand
				holoPanel.transform.parent = avatarGrabberLeft.transform.GetChild(0).transform;
				holoPanel.transform.localPosition = new Vector3(0.03f, 0.09f, -0.04f);
				holoPanel.transform.localEulerAngles = new Vector3(-30, -40, 17);
			}
			else
			{
				//NOTE(Kristof): holoPanel child right hand
				holoPanel.transform.parent = avatarGrabberRight.transform.GetChild(0).transform;
				holoPanel.transform.localPosition = new Vector3(-0.03f, 0.09f, -0.04f);
				holoPanel.transform.localEulerAngles = new Vector3(-20, 55, -27);
			}

			if (OVRInput.GetUp(OVRInput.Button.Start))
			{
				holoPanel.SetActive(!holoPanel.activeSelf);
			}
		}
	}
}