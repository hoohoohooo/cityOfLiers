using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(questSet))]
public class questSedEditor : Editor
{
    questSet code;
    enum EditorMode
    {
        off, on
    }
    EditorMode mode;
    private void OnEnable()
    {
        code = target as questSet;
    }

    private void OnSceneGUI()
    {
        int controlID = GUIUtility.GetControlID(FocusType.Passive);
        HandleUtility.AddDefaultControl(controlID);
        draw();
        if (mode == EditorMode.on)
        {
            mouseInput();
        }
    }

    void draw()
    {
        GUILayout.Window(1, new Rect(0f, 25f, 70f, 80f),
                                                            delegate (int windowID)
                                                            {
                                                                EditorGUILayout.BeginVertical();

                                                                mode = (EditorMode)GUILayout.SelectionGrid((int)mode, new string[] { "off", "on" }, 1);
                                                                if (GUILayout.Button("clear all destination"))
                                                                {
                                                                    //code.destinationPoints.Clear();
                                                                }
                                                                GUI.color = Color.white;

                                                                EditorGUILayout.EndVertical();
                                                            }
                            , "Mode");
    }
    void mouseInput()
    {
        Event e = Event.current;
        if (e.type == EventType.MouseDown)
        {
            if (e.button == 0)
            {
                //ray;
                rayToScreen(e.mousePosition);
            }
        }
    }
    void rayToScreen(Vector2 input)
    {
        Ray ray = HandleUtility.GUIPointToWorldRay(input);
        RaycastHit hit;
        if (mode == EditorMode.on)
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //code.agentList
                //code.destinationPoints.Add(hit.point);
            }
        }
    }

}