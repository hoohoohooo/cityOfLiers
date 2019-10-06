using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class npcData : MonoBehaviour
{
    // Start is called before the first frame update
    static int idCount = 0;
    public int uniqueId;
    public npcData()
    {
        uniqueId = idCount;
        idCount++;        
    }
    void Start()
    {
        if (EditorApplication.isPlaying)
        {

        }
        else
        {
            print("startHappened");
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
