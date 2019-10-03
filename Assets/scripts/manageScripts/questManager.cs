using System.Collections.Generic;
using UnityEngine;

public class questManager : MonoBehaviour
{
    public static questManager instance = null;

    public List<questSet> inactiveSideQuest;
    public List<questSet> inactiveMainQuest;

    public List<questSet> activeSideQuest;
    public questSet activeMainQuest;
    public questSet focusedSideQuest = null;

    //public quest focusedQuest = null;


    

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
    public Transform testTransform;
    void onlyForDebug()
    {
        //focusedQuest = new quest();
        //focusedQuest.objType = quest.objectiveType.place;
        //focusedQuest.destination = new Vector3(10, 0, 10);

        placeQuest tmpQuest = new placeQuest(new Vector3(10, 0, 10));
        NPCQuest tmpQuest_0 = new NPCQuest(testTransform);

        activeMainQuest = new questSet();
        //activeMainQuest.questList = new List<quest>();
        activeMainQuest.questList.Add(tmpQuest);
        activeMainQuest.questList.Add(tmpQuest_0);

        questSet tmpMainQuest = new questSet();
        placeQuest tmpQuest_1 = new placeQuest(new Vector3(1, 0, 1));
        tmpMainQuest.questList.Add(tmpQuest_1);
        inactiveMainQuest.Add(tmpMainQuest);
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
        
        if (focusedSideQuest.questList.Count != 0)
        {
            if (focusedSideQuest.checkQuest())
            {
                activeSideQuest.Remove(focusedSideQuest);
            }
        }
        if(activeMainQuest == null)
        {
            return;
        }
        if(activeMainQuest.questList.Count !=0)
        {
            activeMainQuest.questList[activeMainQuest.questIndex].updateQuest();
            if (activeMainQuest.checkQuest())
            {
                questIndex++;
                if (inactiveMainQuest.Count > 0)
                {
                    activeMainQuest = inactiveMainQuest[0];
                    inactiveMainQuest.Remove(activeMainQuest);
                }
                else
                {
                    activeMainQuest = null;
                }
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
//[System.Serializable]

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
    public NPCQuest(Transform trn)
    {
        objType = objectiveType.npc;
        objectiveNPC = trn;
    }
    public override bool checkQuestDone()
    {
        //return base.checkQuestDone();
        if(gameMng.instance.plCont.hitOutput == objectiveNPC)
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
public class itemQuest : quest
{
    
}