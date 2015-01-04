using UnityEngine;
using System.Collections;
using System.Collections.Generic;



[System.Serializable]
public class StatsEntryData : MonoBehaviour
{
	// Singleton Stuff
	//Here is a private reference only this class can access
	private static StatsEntryData _instance;
	
	//This is the public reference that other classes will use
	public static StatsEntryData instance
	{
		get
		{
			//If _instance hasn't been set yet, we grab it from the scene!
			//This will only happen the first time this reference is used.
			if(_instance == null)
				_instance = GameObject.FindObjectOfType<StatsEntryData>();
			return _instance;
		}
	}

	[System.Serializable]
	public class EntryData 
	{
		public STAT_ENTRY m_Entry = STAT_ENTRY.NONE;
		public STAT_COMBINE_METHOD m_Method = STAT_COMBINE_METHOD.ADD;
	}

	public List<EntryData> m_PerStatData = new List<EntryData>();

	private Dictionary<STAT_ENTRY, EntryData> m_Dict = new Dictionary<STAT_ENTRY, EntryData>();
  
  private EntryData m_Default = new EntryData();

	// Use this for initialization
	void Start ()
	{
		foreach (EntryData entry in m_PerStatData) {
			m_Dict.Add( entry.m_Entry, entry );
		}
	}

  private EntryData GetEntryData( STAT_ENTRY entry )
  {
    return m_Dict.ContainsKey( entry ) ? m_Dict[ entry ] : m_Default;
  }

	public STAT_COMBINE_METHOD GetCombineMethod( STAT_ENTRY entry )
	{
		// If the entry isn't in the dictionary, it should use the default method
		EntryData entryData = GetEntryData( entry );
		return entryData.m_Method;
	}
}


public enum STAT_ENTRY
{
	NONE = 0,
	ATTACK,
	LIFE,
	LIFE_MAX,  
	ENTRY_COUNT
}

public enum STAT_COMBINE_METHOD
{
	BASE,
	ADD,
	ADD_PERCENT,
}