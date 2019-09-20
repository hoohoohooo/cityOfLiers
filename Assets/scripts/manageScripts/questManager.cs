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
    public bool questDone;
    public bool checkQuestDone()
    {
        return false;
    }
}
[System.Serializable]
public class questSet
{
    public List<quest> questList;
    public int questIndex = 0;
}
public class questEvent
{

}