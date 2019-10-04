using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
[System.Serializable]
public class NPCQuest : quest
{
    public NPCQuest(Transform trn)
    {
        objType = objectiveType.npc;
        objectiveNPC = trn;
    }
    public override bool checkQuestDone()
    {
        //return base.checkQuestDone();
        if (gameMng.instance.plCont.hitOutput == objectiveNPC)
        {
            return true;
        }
        return false;
    }
    public override void updateQuest()
    {
        //base.updateQuest();
        destination = objectiveNPC.position;
    }
}
