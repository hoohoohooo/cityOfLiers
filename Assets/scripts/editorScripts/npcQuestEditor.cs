using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NPCQuest))]
public class npcQuestEditor : Editor
{
    NPCQuest code;
    enum EditorMode
    {
        off, on
    }
    EditorMode mode;
    private void OnEnable()
    {
        code = target as NPCQuest;
        SceneView.duringSceneGui += SceneView_duringSceneGui;
    }


    private void SceneView_duringSceneGui(SceneView obj)
    {
        //throw new System.NotImplementedException();
        OnSceneGUI();
        if(code.objectiveNPC == null)
        {
            return;
        }
        code.questNPCIndex = code.objectiveNPC.GetInstanceID();
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= SceneView_duringSceneGui;
        EditorUtility.SetDirty(code);
    }
    Vector3 cubeSize = new Vector3(0.5f, 0.5f, 0.5f);
    private void OnSceneGUI()
    {
        int controlID = GUIUtility.GetControlID(FocusType.Passive);
        HandleUtility.AddDefaultControl(controlID);
        draw();
        if (mode == EditorMode.on)
        {
            mouseInput();
        }
        //Handles.color = Color.magenta;
        //Handles.DrawWireCube(code.destination, cubeSize);
    }



    //public override void OnInspectorGUI()
    //{
    //    //base.OnInspectorGUI();

    //}

    void draw()
    {
        GUILayout.Window(1, new Rect(0f, 25f, 70f, 80f),
                                                            delegate (int windowID)
                                                            {
                                                                EditorGUILayout.BeginVertical();

                                                                //mode = (EditorMode)GUILayout.SelectionGrid((int)mode, new string[] { "off", "on" }, 1);
                                                                GUI.color = Color.white;
                                                                
                                                                code.objectiveNPC = (GameObject)EditorGUILayout.ObjectField(code.objectiveNPC, typeof(GameObject), true);
                                                                if (code.objectiveNPC != null)
                                                                {
                                                                    code.questNPCIndex = code.objectiveNPC.GetComponent<npcData>().uniqueId;
                                                                }
                                                                //code.questNPCIndex = EditorGUILayout.ObjectField(code.objectiveNPC, typeof(GameObject), true).GetInstanceID();
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
                code.destination = hit.point;
            }
        }
    }
}
