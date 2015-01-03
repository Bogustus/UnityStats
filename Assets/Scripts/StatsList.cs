using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Stat {
	public STAT_ENTRY m_Entry;
	public int m_Value;
}

public class StatsList : MonoBehaviour {

	// This is the List that is edited in Unity.  
	// The Unity Editor doesn't play well with Dictionaries.
	// This data is pushed into a Dictionary on Start and then cleared.
	[SerializeField]
	private List<Stat> m_InputList = new List<Stat>();

	private Dictionary<STAT_ENTRY, int> m_Base = new Dictionary<STAT_ENTRY, int>();

	// Use this for initialization
	void Start () {
		foreach (Stat stat in m_InputList) {
			m_Base.Add( stat.m_Entry, stat.m_Value );
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	int GetBaseValue ( STAT_ENTRY entry )
	{
		return m_Base[entry];
	}
}
