using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InteractionTrigger))]
public class InteractionTriggerEditor : Editor
{
    private InteractionTrigger source;

    private SerializedObject serializedTarget;
    private SerializedProperty interactionsProp;

    private void OnEnable()
    {
        source = (InteractionTrigger)target;
        serializedTarget = new SerializedObject(source);
        interactionsProp = serializedTarget.FindProperty("interactions");
    }

    public override void OnInspectorGUI()
    {
       // base.OnInspectorGUI();
        serializedTarget.Update();

        GUILayout.Space(10f);
        var style = new GUIStyle(GUI.skin.button);

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("ADD INTERACTION"))
        {
            if (source.Interactions == null)
                source.Interactions = new List<Interaction>();
            EditorGUI.BeginChangeCheck();
            source.Interactions.Add(new Interaction());
            serializedTarget.ApplyModifiedProperties();
        }
        GUILayout.Space(25f);

        if (source.Interactions != null && source.Interactions.Count > 0)
        {
            for (int i = 0; i < interactionsProp.arraySize; ++i)
            {
                DisplayInteractionField(interactionsProp.GetArrayElementAtIndex(i), i);
            }
            GUILayout.Space(10f);
        }

        GUI.backgroundColor = Color.white;

        serializedTarget.ApplyModifiedProperties();
    }

    private void DisplayInteractionField(SerializedProperty currentInteractionProp, int index)
    {
        EditorGUI.BeginChangeCheck();
        Debug.Log(index);
        SerializedProperty concernedObjectRef = currentInteractionProp.FindPropertyRelative("concernedObject");
        SerializedProperty triggerTypeRef = currentInteractionProp.FindPropertyRelative("triggerType");
        SerializedProperty triggeredEventRef = currentInteractionProp.FindPropertyRelative("triggeredEvent");
        GUI.backgroundColor = Color.blue;
        using (new EditorGUILayout.VerticalScope("HelpBox"))
        {
            GUI.backgroundColor = Color.white;
            AddPopup(ref concernedObjectRef, "Object concerned", typeof(ObjectTag));
            AddPopup(ref triggerTypeRef, "Event trigger type", typeof(TriggerType));
            GUILayout.Space(15f);
            EditorGUILayout.PropertyField(triggeredEventRef);
            GUILayout.Space(15f);
            GUI.backgroundColor = new Color(0.9f, 0.4f, 0.4f);
            GUI.contentColor = Color.white;
            if (GUILayout.Button("REMOVE"))
            {
                interactionsProp.DeleteArrayElementAtIndex(index);
            }
            serializedTarget.ApplyModifiedProperties();
        }
        GUILayout.Space(15f);
    }

    private void AddPopup(ref SerializedProperty ourSerializedProperty, string nameOfLabel, System.Type typeOfEnum)
    {
        //ENUM POPUP

        Rect ourRect = EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginProperty(ourRect, GUIContent.none, ourSerializedProperty);
        EditorGUI.BeginChangeCheck();

        int actualSelected = 1;
        int selectionFromInspector = ourSerializedProperty.intValue;
        string[] enumNamesList = System.Enum.GetNames(typeOfEnum);
        actualSelected = EditorGUILayout.Popup(nameOfLabel, selectionFromInspector, enumNamesList);
        ourSerializedProperty.intValue = actualSelected;

        EditorGUI.EndProperty();
        EditorGUILayout.EndHorizontal();
    }
}
