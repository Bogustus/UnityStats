using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Stat {

	public STAT_ENTRY m_Entry;
	public int m_Value;

  public Stat( STAT_ENTRY entry, int value )
  {
    m_Entry = entry;
    m_Value = value;
  }

  public Stat() : this ( STAT_ENTRY.NONE, 0 ) {}
}

[System.Serializable]
public class StatsList : MonoBehaviour, ISerializationCallbackReceiver {

	// This is the List that is edited in Unity.  
	// The Unity Editor doesn't play well with Dictionaries.
	// This data is pushed into a Dictionary on Start and then cleared.
	[SerializeField]
	private List<Stat> m_InputList = new List<Stat>();

	// Stats are stored in a dictionary for quick access.
	private Dictionary<STAT_ENTRY, int> m_Base = new Dictionary<STAT_ENTRY, int>();

	// These are the lists of modifiers that can change the base stats.
	// They are managed through AddModifierList() and RemoveModifierList ()
	private List<List<StatsList>> m_ModifierLists = new List<List<StatsList>>();
	
  // Unity doesn't currently support editing Dictionaries directly
  // So we edit a List and serialize it into a Dictionary.
  public void OnBeforeSerialize()
  {
    m_InputList.Clear();

    foreach(KeyValuePair<STAT_ENTRY, int> pair in m_Base)
    {
      m_InputList.Add(new Stat(pair.Key, pair.Value));
    }
  }
  
  // load dictionary from list
  public void OnAfterDeserialize()
  {
    m_Base.Clear();
     
    foreach (Stat stat in m_InputList)
    {
      m_Base.Add(stat.m_Entry, stat.m_Value);
    }

  }

	public int GetBaseValue ( STAT_ENTRY entry )
	{
		return m_Base.ContainsKey( entry ) ? m_Base[entry] : 0;
	}
	
	public int GetValue ( STAT_ENTRY entry )
	{
		STAT_COMBINE_METHOD combineMethod = StatsEntryData.instance.GetCombineMethod (entry);

		switch (combineMethod)
		{
		case STAT_COMBINE_METHOD.ADD:
			{
        int val = GetBaseValue( entry );
				foreach ( List<StatsList> listOfLists in m_ModifierLists )
				{
					foreach ( StatsList statsList in listOfLists )
					{
						val += statsList.GetBaseValue( entry );
					}
				}
				return val;
			}
		case STAT_COMBINE_METHOD.BASE:
		default:
			return GetBaseValue(entry);
		}
	}
	
	public void SetBaseValue ( STAT_ENTRY entry, int value )
	{
		m_Base[entry] = value;
	}

  // Adds a list of StatsList
  // This is so that several StatsLists can be impacted by this list.
  // Also, the contents of that list can be modified without updating this.
  public void AddModifierList ( List<StatsList> modifierList )
  {
    if ( m_ModifierLists.Contains( modifierList ) )
      return;

    m_ModifierLists.Add( modifierList );
  }
  
  public void AddModifierList ( StatsList modifierList )
  {
    foreach ( List<StatsList> listOfStatsList in m_ModifierLists )
    {
      if ( listOfStatsList.Contains( modifierList ) )
        return;
    }
    List<StatsList> listForModifier = new List<StatsList>();
    listForModifier.Add( modifierList );
    m_ModifierLists.Add( listForModifier );
  }

  public bool RemoveModifierList ( List<StatsList> modifierList )
  {
    return m_ModifierLists.Remove( modifierList );
  }

  public bool RemoveModifierList ( StatsList modifierList )
  {
    foreach ( List<StatsList> listOfStatsList in m_ModifierLists )
    {
      if ( listOfStatsList.Remove( modifierList ) )
        return true;
    }
    return false;
  }

  public void ClearModifierLists ()
  {
    m_ModifierLists.Clear();
  }
}
