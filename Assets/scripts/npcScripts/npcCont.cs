using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npcCont : MonoBehaviour
{
    public npcAgents agent;
    public List<Vector3> destinationPoints;
    public states.stateType curStateType;
    [SerializeField]
    bool shareDestinationPoints = false;
    [SerializeField]
    npcAgents.npcType type;
    public int instanceID;
    float npcSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
        agent = new npcAgents();
        agent.agentTrn = GetComponent<NavMeshAgent>();
        agent.character = GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>();
        if (!shareDestinationPoints)
        {
            agent.destinationPoints = destinationPoints;
        }
        else
        {
            agent.destinationPoints = FSM.instance.destinationPoints;
        }
        agent.curState = new moveToState(agent, gameMng.instance.player);
        agent.nType = type;
        FSM.instance.agentList.Add(agent);
        instanceID = gameObject.GetInstanceID();
    }
    // Update is called once per frame
    void Update()
    {
        curStateType = agent.curStateType;
    }
    private void OnDrawGizmosSelected()
    {
        if (destinationPoints.Count > 0)
        {
            foreach (Vector3 v in destinationPoints)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(v, 0.2f);
            }
        }
        if (destinationPoints.Count > 1)
        {
            int i = 0;
            foreach(Vector3 v in destinationPoints)
            {
                i++;
                if (i < destinationPoints.Count) {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(v, destinationPoints[i]);
                }
            }
            Gizmos.DrawLine(destinationPoints[0], destinationPoints[destinationPoints.Count - 1]);
        }
    }
}