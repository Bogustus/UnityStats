using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class StatsListTest
{
  public StatsList m_BaseList;
  public StatsList m_ModifierList1;
  public StatsList m_ModifierList2;

  [SetUp]
  public void Init()
  {
    StatsList[] arrayOfStatsList = Object.FindObjectsOfType<StatsList>();
    foreach ( StatsList statsList in arrayOfStatsList )
    {
      switch( statsList.name )
      {
      case "StatsList":
        m_BaseList = statsList;
        break;

      case "StatsListModifier1":
        m_ModifierList1 = statsList;
        break;

      case "StatsListModifier2":
        m_ModifierList2 = statsList;
        break;
      }
    }
  }
  
  [Test]
  public void TestBaseNotZero()
  {
    Assert.IsNotNull (m_BaseList);
    
    for (STAT_ENTRY e = STAT_ENTRY.NONE + 1; e != STAT_ENTRY.ENTRY_COUNT; e++) 
    {
      Assert.AreNotEqual( 0, m_BaseList.GetBaseValue( e ), e.ToString() );
    }
  }

  [Test]
  public void TestBaseAccessors()
  {
    Assert.IsNotNull (m_BaseList);

    for (STAT_ENTRY e = STAT_ENTRY.NONE + 1; e != STAT_ENTRY.ENTRY_COUNT; e++) 
    {
      Assert.AreEqual( m_BaseList.GetBaseValue( e ), m_BaseList.GetValue( e ) );
    }
  }
    
  [Test]
  public void TestAddCombineMethod()
  {
    Assert.IsNotNull (m_BaseList);
    Assert.IsNotNull (m_ModifierList1);
    Assert.IsNotNull (m_ModifierList2);

    m_BaseList.AddModifierList( m_ModifierList1 );
    m_BaseList.AddModifierList( m_ModifierList2 );
    
    for (STAT_ENTRY e = STAT_ENTRY.NONE + 1; e != STAT_ENTRY.ENTRY_COUNT; e++) 
    {
      if ( StatsEntryData.instance.GetCombineMethod( e ) == STAT_COMBINE_METHOD.ADD )
      {
        Assert.AreEqual( m_BaseList.GetBaseValue( e ) + m_ModifierList1.GetValue( e ) 
                        + m_ModifierList2.GetValue( e ), 
                        m_BaseList.GetValue( e ) );
      }
    }
    Assert.IsTrue( m_BaseList.RemoveModifierList( m_ModifierList1 ) );
    Assert.IsTrue( m_BaseList.RemoveModifierList( m_ModifierList2 ) );
  }

  [Test]
  public void TestListOfStatsLists()
  {
    Assert.IsNotNull (m_BaseList);
    Assert.IsNotNull (m_ModifierList1);
    Assert.IsNotNull (m_ModifierList2);

    List<StatsList> listOfStatsLists = new List<StatsList>();
    listOfStatsLists.Add( m_ModifierList1 );
    listOfStatsLists.Add( m_ModifierList2 );

    m_BaseList.AddModifierList( listOfStatsLists );
    
    for (STAT_ENTRY e = STAT_ENTRY.NONE + 1; e != STAT_ENTRY.ENTRY_COUNT; e++) 
    {
      if ( StatsEntryData.instance.GetCombineMethod( e ) == STAT_COMBINE_METHOD.ADD )
      {
        Assert.AreEqual( m_BaseList.GetBaseValue( e ) + m_ModifierList1.GetValue( e ) 
                        + m_ModifierList2.GetValue( e ), 
                        m_BaseList.GetValue( e ) );
      }
    }

    Assert.IsTrue( m_BaseList.RemoveModifierList( listOfStatsLists ));
  }
}

