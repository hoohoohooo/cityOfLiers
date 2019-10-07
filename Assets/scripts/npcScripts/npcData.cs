using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class npcData : MonoBehaviour
{
    // Start is called before the first frame update
    public npcManager manager;
    public int uniqueId = 0;
    
    void Start()
    {
        if (EditorApplication.isPlaying)
        {

        }
        else
        {
            print("startHappened");
            if(uniqueId == 0)
            {
                EditorUtility.SetDirty(manager);
                EditorUtility.SetDirty(this);
                manager.npcCount++;
                uniqueId = manager.npcCount;
            }
        }
    }
    private void OnDestroy()
    {
        if (EditorApplication.isPlaying)
        {

        }
        else
        {
            print("destroyHappened");
        }
    }
    
}
