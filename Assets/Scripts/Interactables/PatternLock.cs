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
	private int[] pass;

	private float oldCon1;
	private float oldCon2;

	private static Stopwatch stopwatch = new Stopwatch();
	private static List<PatternLock> Locks;

	// Use this for initialization
	void Start()
	{
		if (Locks == null) Locks = new List<PatternLock>();
		Locks.Add(this);
		// zig zag boven -> onder, links -> rechts
		pass = new int[] { 0, 3, 6, 7, 4, 1, 2, 5, 8 };
	}

	// Update is called once per frame
	void Update()
	{
		var con1 = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
		var con2 = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

		if (inUse && ((con1 < 0.5f && 0.5f < oldCon1) || (con2 < 0.5f && 0.5f < oldCon2)))
		{
			inUse = false;
			ClearPattern();
		}
		if (!inUse && historySphere.Count != 0)
		{

			Debug.Log(historySphere);
			historySphere.Clear();
			historyPosition.Clear();
		}

		oldCon1 = con1;
		oldCon2 = con2;
	}

	void ClearPattern()
	{
		// Note(Lander): inUse has to be false, this is safeguard
		if (inUse) return;

		stopwatch.Stop();
		Debug.LogFormat("Clearing pattern. Time elapsed: {0}", stopwatch.Elapsed);


		// NOTE(Lander): clear the visuals pattern on screen
		historyPosition.Clear();
		lineRenderer.positionCount = 0;
		lineRenderer.SetPositions(historyPosition.ToArray());
		spheresRaw.ForEach((i) => i.GetComponent<Renderer>().material.color = Color.white);
	}

	bool CheckPattern()
	{
		// note(Lander): Abort when still inputting, Do not accept mismatching lengths
		if (inUse || historySphere.Count != pass.Length) return false;

		for (var i = 0; i < pass.Length; i++)
		{
			var attempt = Int32.Parse(historySphere[i].gameObject.name);
			var correct = pass[i];
			Debug.LogFormat("checking: {0}, {1}", attempt, correct);

			if (attempt != correct)
			{
				Debug.Log("pattern mismatch!");
				return false;
			}
		}

		Debug.Log("Pattern match!");
		// note(Lander): reset
		historySphere.Clear();
		ClearPattern();

		return true;
	}

	public static void SphereHit(PatternSphere g, PatternLock parent)
	{

		if (parent.historySphere.Count == 0)
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
