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

	private float oldCon1;
	private float oldCon2;

	private static Stopwatch stopwatch = new Stopwatch();
	private static List<PatternLock> Locks;

	// Use this for initialization
	void Start()
	{
		if (Locks == null) Locks = new List<PatternLock>();
		Locks.Add(this);
	}

	// Update is called once per frame
	void Update()
	{
		var con1 = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
		var con2 = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

		if (inUse && ((con1 < 0.5f && 0.5f < oldCon1) || (con2 < 0.5f && 0.5f < oldCon2)))
		{
			Debug.Log("Trigger is released, dropping input");
			inUse = false;
			ClearPattern();
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

	public static void SphereHit(PatternSphere g, PatternLock parent)
	{

		if (parent.historySphere.Count == 0)
		{
			parent.inUse = true;
			stopwatch.Reset();
			stopwatch.Start();
			parent.historySphere.Clear();
			parent.historyPosition.Clear();
		}
		else if (!parent.inUse)
		{
			parent.ClearPattern();
			return;
		}
		Debug.Log("Continuing pattern");


		parent.historySphere.Add(g);
		parent.historyPosition.Add(g.transform.position + new Vector3(0.02f, 0, 0));

		//var positions = new List<Vector3>();
		//foreach (var sphere in parent.history)
		//{
		//	var pos = sphere.gameObject.transform.position;
		//	pos += new Vector3(0.02f, 0, 0);
		//	positions.Add(pos);
		//}

		if (parent.historyPosition.Count > 1)
		{
			parent.lineRenderer.positionCount = parent.historyPosition.Count;
			parent.lineRenderer.SetPositions(parent.historyPosition.ToArray());
		}
	}
}
