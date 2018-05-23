using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


// DONE(Lander): check if the line renderer is drawn correctly /!\ IT IS NOT
public class PatternLock : MonoBehaviour
{

	public int gridSize;
	public bool inUse;
	public List<GameObject> spheresRaw;
	public List<PatternSphere> attemptSphere;
	public List<Vector3> attemptPosition;
	public Text info;
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
			//transform.localEulerAngles = new Vector3(0, 180, 0);
			//transform.localPosition += new Vector3(0, 0, -1);
		}
		if (Locks == null) Locks = new List<PatternLock>();
		Locks.Add(this);
		info = transform.parent.GetComponentInChildren<Text>();
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

		// Only log if stopwatch was set 
		if (stopwatch.Elapsed.Milliseconds > 0)
			World.Record("pattern:elapsed:{0}", stopwatch.Elapsed);

		stopwatch.Reset();

		// NOTE(Lander): clear the visuals pattern on screen
		attemptPosition.Clear();
		attemptSphere.Clear();
		lineRenderer.positionCount = 0;
		lineRenderer.SetPositions(attemptPosition.ToArray());
		spheresRaw.ForEach((i) => i.GetComponent<Renderer>().material.color = Color.white);
	}

	// DONE(Lander): 1) pattern a, 2) pattern b, 2) pattern b fails
	// TODO(Lander): disable teleport when inUse

	private bool CheckPattern()
	{
		World.Record("pattern:attempt:[{0}]", string.Join(",", attemptSphere.Select(sphere => sphere.gameObject.name).ToArray()));

		if (patternSetter && !confirmationNeeded)
		{
			pass.Clear();
			foreach (var sphere in attemptSphere)
			{
				pass.Add(Int32.Parse(sphere.name));
			}
			World.Record("pattern:set:step-1:[{0}]", string.Join(",", pass.Select(i => i.ToString()).ToArray()));
			if(info) info.text = "PLEASE CONFIRM YOUR PERSONAL PATTERN ON THE GRID";
			confirmationNeeded = true;
			return false;
		}

		// note(Lander): Abort when still inputting, Do not accept mismatching lengths
		if (!patternSetter && confirmationNeeded && attemptSphere.Count != pass.Count) return false;

		for (var i = 0; i < pass.Count; i++)
		{
			var attempt = Int32.Parse(attemptSphere[i].gameObject.name); // TODO(Lander): prevent ArgumentOutOfBoundsException
			var correct = pass[i];

			if (attempt != correct)
			{
				if (patternSetter && confirmationNeeded)
				{
					pass.Clear();
					foreach (var sphere in attemptSphere)
					{
						pass.Add(Int32.Parse(sphere.name));
					}
				}

				if(info) info.text = confirmationNeeded ? "PLEASE REDRAW THE PATTERN TWICE TO CONFIRM" : "[ACCESS DENIED]";
				return false;
			}
		}

		if (confirmationNeeded)
		{
			confirmationNeeded = false;

			if (pass.Count < attemptSphere.Count)
			{
				pass.Clear();
				return false;
			}

			patternSetter = false;

			if(info) info.text = "YOUR PERSONAL PATTERN IS NOW SET";
			World.Record("pattern:set:step-2:[{0}]", string.Join(",", pass.Select(i => i.ToString()).ToArray()));
			return true;
		}

		if (info) info.text = "[ACCESS GRANTED]";
		return true;
	}

	public static void SphereHit(PatternSphere g, PatternLock parent)
	{
		// DONE(Lander): test behaviour of connecting two dots from different parents.
		// Note(Lander): It's impossible to see two patternLocks at the same time.

		parent.lineRenderer.enabled = true;

		if (parent.attemptSphere.Count == 0 && (parent.Con1 > 0.5f || (parent.Con2 > 0.5f)))
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

		parent.attemptSphere.Add(g);
		parent.attemptPosition.Add(g.transform.position + new Vector3(0.02f, 0, 0));

		if (parent.attemptPosition.Count > 1)
		{
			parent.lineRenderer.positionCount = parent.attemptPosition.Count;
			parent.lineRenderer.SetPositions(parent.attemptPosition.ToArray());
		}

	}
}
