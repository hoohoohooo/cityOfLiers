using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu()]
[System.Serializable]
public class placeQuest : quest
{
    public float x;
    public float y;
    public float z;
    const float destDist = 2;
    public placeQuest(Vector3 dest)
    {
        objType = objectiveType.place;
        destination = dest;
        player = gameMng.instance.player;
    }
    public override bool checkQuestDone()
    {
        if (Vector3.Distance(destination, player.position) < destDist)
        {
            //if (qEvent != null)
            //{
            //    gameMng.instance.coroutineStarter(qEvent.qEvent());
            //}
            return true;
        }
        else
        {
            return false;
        }
        //return base.checkQuestDone();
    }
    
}
