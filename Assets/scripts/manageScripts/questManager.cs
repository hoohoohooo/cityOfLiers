using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class questManager : MonoBehaviour
{
    public static questManager instance = null;

    public List<questSet> inactiveSideQuest;
    public List<questSet> inactiveMainQuest;

    public List<questSet> activeSideQuest;
    public questSet activeMainQuest = null;
    public questSet focusedSideQuest = null;

    public List<Transform> mainQuestObjectiveNPC;

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
        initializeAllQuest();
    }

    void initializeAllQuest()
    {
        int questNpcIndex = 0;
        foreach(questSet qs in inactiveMainQuest)
        {
            foreach(quest q in qs.questList)
            {
                q.player = gameMng.instance.player;
                if(q.objType == quest.objectiveType.npc)
                {
                    q.objectiveNPC = mainQuestObjectiveNPC[questNpcIndex];
                    questNpcIndex++;
                }
            }
            qs.questIndex = 0;
        }
    }

    public Transform testTransform;
    void onlyForDebug()
    {
        //focusedQuest = new quest();
        //focusedQuest.objType = quest.objectiveType.place;
        //focusedQuest.destination = new Vector3(10, 0, 10);

        placeQuest tmpQuest = new placeQuest(new Vector3(10, 0, 10));
        NPCQuest tmpQuest_0 = new NPCQuest(testTransform);

        //activeMainQuest = new questSet();
        ////activeMainQuest.questList = new List<quest>();
        //activeMainQuest.questList.Add(tmpQuest);
        //activeMainQuest.questList.Add(tmpQuest_0);

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
        if (focusedSideQuest != null)
        {
            if (focusedSideQuest.questList.Count != 0)
            {
                if (focusedSideQuest.checkQuest())
                {
                    activeSideQuest.Remove(focusedSideQuest);
                }
                focusedSideQuest = activeSideQuest[0];
            }
        }
        if(activeMainQuest == null)
        {
            if (inactiveMainQuest.Count > 0)
            {
                activeMainQuest = inactiveMainQuest[0];
                inactiveMainQuest.Remove(activeMainQuest);
            }
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
public class quest : ScriptableObject
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
    public questEvent afterEvent;
    public virtual bool checkQuestDone()
    {
        return false;
    }
    public virtual void updateQuest()
    {

    }
    
}

public class itemQuest : quest
{
    public override bool checkQuestDone()
    {

        //return base.checkQuestDone();
        gameMng.instance.coroutineStarter(qEvent.qEvent());
        return false;
    }
}