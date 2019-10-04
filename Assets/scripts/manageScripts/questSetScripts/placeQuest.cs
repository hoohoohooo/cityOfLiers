using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
[System.Serializable]
public class placeQuest : quest
{
    float destDist = 2;
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
            return true;
        }
        else
        {
            return false;
        }
        //return base.checkQuestDone();
    }
    
}
