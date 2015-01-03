using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StatsEntryData : MonoBehaviour
{
	[System.Serializable]
	public class PerStatEntryData 
	{
		public STAT_ENTRY m_Entry;
		public STAT_COMBINE_METHOD m_Method;
	}

	public List<PerStatEntryData> m_PerStatData = new List<PerStatEntryData>();

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}


public enum STAT_ENTRY
{
	NONE = 0,
	ATTACK,
	LIFE,
	ENTRY_COUNT
}

public enum STAT_COMBINE_METHOD
{
	ADD,
	ADD_PERCENT,
}