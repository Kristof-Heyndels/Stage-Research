using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PatternLock : MonoBehaviour
{

	public int gridSize;
	public bool inUse;
	public List<GameObject> spheresRaw;
	public List<PatternSphere> historySphere;
	public List<Vector3> historyPosition;
	public LineRenderer lineRenderer;

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
		if (inUse && !(
			OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) < 0.5 ||
			OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < 0.5))
		{
			Debug.Log("Trigger is released, dropping input");
			inUse = false;
		}
	}
	void ClearPattern()
	{
		Debug.Log("Clearing pattern");

		// NOTE(Lander): clear the visuals pattern on screen
		lineRenderer.SetPositions(historyPosition.ToArray());
		lineRenderer.positionCount = 0;
		spheresRaw.ForEach((i) => i.GetComponent<Renderer>().material.color = Color.white);
	}

	public static void SphereHit(PatternSphere g, PatternLock parent)
	{

		if (parent.historySphere.Count == 0)
		{
			parent.inUse = true;
			parent.historySphere.Clear();
			parent.historyPosition.Clear();
		}
		else if (!parent.inUse)
		{
			parent.ClearPattern();
			return;
		}
		Debug.Log("Continueing pattern");


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
