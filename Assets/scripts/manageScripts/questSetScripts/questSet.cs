using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
[System.Serializable]
public class questSet : ScriptableObject
{
    public List<quest> questList;
    public int questIndex = 0;
    public questSet()
    {
        questList = new List<quest>();
    }
    public bool checkQuest()
    {
        if (questList[questIndex].checkQuestDone())
        {
            questIndex++;
        }
        if (questIndex == questList.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}