using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomPropertyDrawer(typeof(Stat))]
public class StatDrawer : PropertyDrawer {
	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) 
	{
		Rect entryRect = position;
		entryRect.xMax -= entryRect.width / 2;
		EditorGUI.PropertyField(entryRect, property.FindPropertyRelative("m_Entry"), GUIContent.none);

		Rect valueRect = position;
		valueRect.xMin = entryRect.xMax;
		EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("m_Value"), GUIContent.none);
	}
}

public class StatsListEditor : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
