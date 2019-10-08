using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
[System.Serializable]
public class itemQuest : quest
{
    public itemBase targetItem = null;
    int checkCount = 0;
    public int targetCount = 0;
    public override bool checkQuestDone()
    {
        if(targetItem == null)
        {
            return false;
        }
        foreach(itemBase b in gameMng.instance.plCont.data.inventory)
        {
            if(b.itemName == targetItem.itemName)
            {
                checkCount += b.count;
            }
        }
        if (checkCount >= targetCount)
        {
            return true;
        }
        else
        {
            checkCount = 0;
        }
        //return base.checkQuestDone();
        //gameMng.instance.coroutineStarter(qEvent.qEvent());
        return false;
    }
}
