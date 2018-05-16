using System.Collections;
using System.Collections.Generic;
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

		parent.lineRenderer.SetPosition(parent.index, g.transform.position );
		

	}
}
