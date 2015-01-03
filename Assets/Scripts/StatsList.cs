using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Stat {
	public STAT_ENTRY m_Entry;
	public int m_Value;
}

public class StatsList : MonoBehaviour {

	public List<Stat> m_StatsList;
	public Dictionary<STAT_ENTRY, int> m_TestHashTable;

	// Use this for initialization
	void Start () {
		m_StatsList.Sort(delegate(Stat x, Stat y) {
			if (x.m_Entry > y.m_Entry) return 1;
			if (x.m_Entry < y.m_Entry) return -1;
			return 0;
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public enum STAT_ENTRY
{
	STAT_NONE = 0,
	STAT_ATTACK,
	STAT_LIFE,
}