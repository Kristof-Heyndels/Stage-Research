using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PatternLock : MonoBehaviour
{

	public int gridSize;
	public List<GameObject> spheresRaw;
	public List<PatternSphere> history;
	public LineRenderer lineRenderer;

	private int index;

	private static List<PatternLock> Locks;

	// Use this for initialization
	void Start()
	{
		index = 0;
		if (Locks == null) Locks = new List<PatternLock>();
		Locks.Add(this);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public static void SphereHit(PatternSphere g, PatternLock parent)
	{
		parent.history.Add(g);
		parent.index++;
		Debug.Log(parent.history);

		//var positions = new[]
		//{
		//	parent.history[0].gameObject.transform.position,
		//	g.transform.position
		//};

		var positions = new List<Vector3>();
		foreach (var sphere in parent.history)
		{
			var pos = sphere.gameObject.transform.position;
			pos += new Vector3(0.02f, 0, 0);
			positions.Add(pos);
		}

		if (positions.Count > 1)
		{
			parent.lineRenderer.positionCount = positions.Count;
			parent.lineRenderer.SetPositions(positions.ToArray());
		}
		

	}
}
