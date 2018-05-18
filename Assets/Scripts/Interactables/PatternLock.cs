using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PatternLock : MonoBehaviour
{

	public int gridSize;
	public bool inUse;
	public List<GameObject> spheresRaw;
	public List<PatternSphere> historySphere;
	public List<Vector3> historyPosition;
	public LineRenderer lineRenderer;

	private float Con1;
	private float Con2;
	private float oldCon1;
	private float oldCon2;
	public bool patternSetter;

	private static Stopwatch stopwatch = new Stopwatch();
	private static List<PatternLock> Locks;
	private static List<int> pass = new List<int>();
	private static bool confirmationNeeded;

	// Use this for initialization
	void Start()
	{
		if (transform.parent.tag == "PatternSetter")
		{
			patternSetter = true;
			transform.localEulerAngles = new Vector3(0, 180, 0);
			transform.localPosition += new Vector3(0, 0, -1);

		}
		if (Locks == null) Locks = new List<PatternLock>();
		Locks.Add(this);
	}

	// Update is called once per frame
	void Update()
	{
		Con1 = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
		Con2 = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

		if (inUse && ((Con1 < 0.5f && 0.5f < oldCon1) || (Con2 < 0.5f && 0.5f < oldCon2)))
		{
			inUse = false;
			if (CheckPattern())
			{
				transform.parent.GetComponent<Door>().Open();
			}
			else
			{
				spheresRaw.ForEach(sphere => sphere.GetComponent<Renderer>().material.color = Color.red);

			}
			ClearPattern();
		}

		oldCon1 = Con1;
		oldCon2 = Con2;
	}

	void ClearPattern()
	{
		// Note(Lander): inUse has to be false, this is safeguard
		if (inUse) return;

		stopwatch.Stop();
		Debug.LogFormat("Clearing pattern. Time elapsed: {0}", stopwatch.Elapsed);


		// NOTE(Lander): clear the visuals pattern on screen
		historyPosition.Clear();
		historySphere.Clear();
		lineRenderer.positionCount = 0;
		lineRenderer.SetPositions(historyPosition.ToArray());
		spheresRaw.ForEach((i) => i.GetComponent<Renderer>().material.color = Color.white);
	}

	private bool CheckPattern()
	{
		if (patternSetter && !confirmationNeeded)
		{
			pass.Clear();
			foreach (var sphere in historySphere)
			{
				pass.Add(Int32.Parse(sphere.name));
			}

			confirmationNeeded = true;
			return false;
		}

		// note(Lander): Abort when still inputting, Do not accept mismatching lengths
		if (historySphere.Count != pass.Count) return false;

		for (var i = 0; i < pass.Count; i++)
		{
			var attempt = Int32.Parse(historySphere[i].gameObject.name);
			var correct = pass[i];

			if (attempt != correct)
			{

				return false;
			}
		}

		if (confirmationNeeded)
		{
			confirmationNeeded = false;
			patternSetter = false;
			return true;
		}

		return true;
	}

	public static void SphereHit(PatternSphere g, PatternLock parent)
	{

		if (parent.historySphere.Count == 0 && (parent.Con1 > 0.5f || (parent.Con2 > 0.5f)))
		{
			parent.inUse = true;
			stopwatch.Reset();
			stopwatch.Start();
		}
		else if (!parent.inUse)
		{
			parent.ClearPattern();
			return;
		}

		parent.historySphere.Add(g);
		parent.historyPosition.Add(g.transform.position + new Vector3(0.02f, 0, 0));

		if (parent.historyPosition.Count > 1)
		{
			parent.lineRenderer.positionCount = parent.historyPosition.Count;
			parent.lineRenderer.SetPositions(parent.historyPosition.ToArray());
		}

	}
}
