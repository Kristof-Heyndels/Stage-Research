using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternLock : MonoBehaviour
{

	public int gridSize;
	public List<GameObject> spheresRaw;
	public List<PatternSphere> history;

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

	}

	public static void SphereHit(PatternSphere g, PatternLock parent)
	{
		if (!parent.spheresRaw.Contains(g.gameObject))
		{
			parent.history.Add(g);

			Debug.Log(parent.history);
		}
	}
}
