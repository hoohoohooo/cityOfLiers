using System.Collections.Generic;
using UnityEngine;

public class questManager : MonoBehaviour
{
    public static questManager instance = null;

    public List<questSet> inactiveSideQuest;
    public List<questSet> inactiveMainQuest;

    public List<questSet> activeSideQuest;
    public questSet activeMainQuest;

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
    enum objectiveType
    {
        place,
        npc,
        item,
    }
    objectiveType objType;
    public Vector3 destination;
    public Transform objectiveNPC = null;
    public questEvent qEvent = null;
    public itemBase questItem;
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