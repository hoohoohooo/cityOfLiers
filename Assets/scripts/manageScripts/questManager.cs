using System.Collections.Generic;
using UnityEngine;

public class questManager : MonoBehaviour
{
    public static questManager instance = null;

    public List<questSet> inactiveSideQuest;
    public List<questSet> inactiveMainQuest;

    public List<questSet> activeSideQuest;
    public questSet activeMainQuest;

    public quest focusedQuest = null;
    public questSet focusedSideQuest = null;

    public int questIndex = 0;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        onlyForDebug();
    }

    void onlyForDebug()
    {
        focusedQuest = new quest();
        focusedQuest.objType = quest.objectiveType.place;
        focusedQuest.destination = new Vector3(10, 0, 10);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //foreach(questSet s in activeSideQuest)
        //{
        //    if (s.checkQuest())
        //    {
        //        activeSideQuest.Remove(s);
        //    }
        //    break;
        //}
        if (focusedSideQuest != null)
        {
            if (focusedSideQuest.checkQuest())
            {
                activeSideQuest.Remove(focusedSideQuest);
            }
        }
        if(activeMainQuest != null)
        {
            if (activeMainQuest.checkQuest())
            {
                questIndex++;
                activeMainQuest = inactiveMainQuest[questIndex];
            }
        }
    }
}
[System.Serializable]
public class quest
{
    public enum objectiveType
    {
        place,
        npc,
        item,
    }
    public objectiveType objType;
    public Vector3 destination;
    public Transform objectiveNPC = null;
    public questEvent qEvent = null;
    public itemBase questItem = null;
    public Transform player;
    public bool questDone;
    public virtual bool checkQuestDone()
    {
        return false;
    }
    public virtual void updateQuest()
    {

    }
}
[System.Serializable]
public class questSet
{
    public List<quest> questList;
    public int questIndex = 0;
    public bool checkQuest()
    {
        if (questList[questIndex].checkQuestDone())
        {
            questIndex++;
        }
        if(questIndex == questList.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
public class questEvent
{

}

public class placeQuest:quest
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
public class NPCQuest : quest
{
    public override bool checkQuestDone()
    {
        //return base.checkQuestDone();
        return false;
    }
}