using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class FSM : MonoBehaviour
{
    public List<npcAgents> agentList;
    public static FSM instance = null;
    public List<Vector3> destinationPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            print("destroy happened");
        }
        foreach(npcAgents n in agentList)
        {
            if(n.curState == null)
            {
                n.curState = new moveToState(n, gameMng.instance.player);
                //n.curState = new chaseState(gameMng.instance.player,n);
                //n.curState = new idle(gameMng.instance.player, n);
                n.lastState = new moveToState(n, gameMng.instance.player);
            }
        }
    }
    bool firstUpdate = true;
    void firstTimeUpdate()
    {
        firstUpdate = false;
        foreach(npcAgents n in agentList)
        {
            n.startAgent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (firstUpdate)
        {
            firstTimeUpdate();
        }
        if (agentList.Count > 0 || agentList != null) 
        {
            foreach(npcAgents n in agentList)
            {
                n.updateAgent();
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (destinationPoints.Count > 0)
        {
            foreach(Vector3 v in destinationPoints)
            {
                Gizmos.color = Color.magenta;
                Gizmos.DrawSphere(v, 0.2f);
            }
        }
        if (destinationPoints.Count > 1)
        {
            int i = 0;
            foreach (Vector3 v in destinationPoints)
            {
                i++;
                if (i < destinationPoints.Count)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(v, destinationPoints[i]);
                }
            }
            Gizmos.DrawLine(destinationPoints[0], destinationPoints[destinationPoints.Count - 1]);
        }
    }
}

[System.Serializable]
public class npcAgents
{
    public states curState;
    public states lastState;
    public NavMeshAgent agentTrn;
    public Animator agentAnim;
    public List<Vector3> destinationPoints;

    public ThirdPersonCharacter character;
    public states.stateType curStateType;

    public enum npcType
    {
        normal,cop,gang,
    }
    public npcType nType;

    public npcAgents()
    {
        //curState = new moveToState(this);
    }
    public void startAgent()
    {
        //debug only
        if (agentTrn.transform.childCount < 1)
        {
            return;
        }
        //
        agentAnim = agentTrn.transform.GetComponent<Animator>();
        character = agentTrn.transform.GetComponent<ThirdPersonCharacter>();
        agentTrn.updateRotation = false;
        curState.stateStart();
    }
    public void updateAgent()
    {
        curState.stateUpdate();
        curStateType = curState.sType;
    }
}

public class states
{
    public npcAgents agent;
    public Transform plTrn;
    public enum stateType
    {
        idle,
        moveTo,
        chase,
        search,
        hitPlayer,
        combat,
    }
    public stateType sType;
    public virtual void stateStart()
    {

    }
    public virtual void stateUpdate()
    {
        
    }
}

public class idle : states
{
    public idle(Transform trn, npcAgents nav)
    {
        plTrn = trn;
        agent = nav;
        sType = stateType.idle;
    }
    public override void stateUpdate()
    {
        //Vector3 dir = plTrn.position - agent.agentTrn.transform.position;
        //base.stateUpdate();
    }
}

public class moveToState : states
{
    int loopIndex = 0;
    public float checkDist = 1f;
    public List<Vector3> destinationPoints;
    public Vector3 curDestination = Vector3.zero;
    

    public moveToState(npcAgents nav, Transform targ)
    {
        plTrn = targ;
        sType = stateType.moveTo;
        agent = nav;
        agent.agentTrn.speed = 0.7f;
        destinationPoints = nav.destinationPoints;
        curDestination = destinationPoints[0];
        agent.agentTrn.SetDestination(curDestination);
        //agent.character.Move(agent.agentTrn.desiredVelocity, false, false);
    }

    public bool checkArrival(Vector3 curpos,Vector3 dest)
    {
        if (Vector3.Distance(curpos, dest) < checkDist)
        {
            if (loopIndex < destinationPoints.Count)
            {
                loopIndex++;
            }
            if(loopIndex == destinationPoints.Count)
            {
                loopIndex = 0;
            }
            curDestination = destinationPoints[loopIndex];
            agent.agentTrn.SetDestination(curDestination);
            agent.character.Move(agent.agentTrn.desiredVelocity, false, false);
            return true;
        }
        return false;
    }

    Ray ray;
    RaycastHit hit;
    Vector3 dir;

    public void checkPlayer()
    {

        dir = plTrn.position - agent.agentTrn.transform.position;
        if (Vector3.Dot(agent.agentTrn.transform.forward, dir) < 0)
        {
            return;
        }
        else
        {
            ray = new Ray(agent.agentTrn.transform.position, dir);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == plTrn)
                {
                    if (hit.distance < 20)
                    {
                        agent.lastState = this;
                        chaseState tmp = new chaseState(plTrn, agent);
                        agent.curState = tmp;
                    }
                }
                else
                {
                    return;
                }
            }
        }
    }

    public override void stateStart()
    {
        //base.stateStart();
        float dist = 65535;
        int i = 0;
        foreach (Vector3 v in destinationPoints)
        {
            float tmp = Vector3.Distance(agent.agentTrn.transform.position, v);
            if (tmp < dist)
            {
                dist = tmp;
                curDestination = v;
                loopIndex = i;
            }
            i++;
        }
        agent.agentTrn.SetDestination(curDestination);
        agent.character.Move(agent.agentTrn.desiredVelocity, false, false);
        //agent.agentTrn.speed = 3.5f;
    }
    public override void stateUpdate()
    {
        //base.stateUpdate();
        if (agent.nType != npcAgents.npcType.normal)
        {
            checkPlayer();
        }
        checkArrival(agent.agentTrn.transform.position,curDestination);
        
    }
}

public class chaseState : states
{
    public Transform target;
    Ray ray;
    Vector3 dir;
    RaycastHit hit;
    Vector3 lastDestination;
    float destDist = 0.5f;
    float combatDist = 5;
    List<Vector3> playerPosList;
    //float plTrackTimer = 0;
    //float trackTime = 0.5f;
    public chaseState(Transform targ, npcAgents nav)
    {
        target = targ;
        agent = nav;
        playerPosList = new List<Vector3>();
        sType = stateType.chase;
    }
    public bool rayTarget()
    {
        dir = target.position - agent.agentTrn.transform.position;
        ray = new Ray(agent.agentTrn.transform.position, target.position);
        
        if (Physics.Raycast(agent.agentTrn.transform.position,dir,out hit))
        {
            if (hit.transform == target)
            {
                return true;
            }
        }
        return false;
    }
    public void chaseTarget()
    {
        agent.agentTrn.SetDestination(target.position);
        agent.character.Move(agent.agentTrn.desiredVelocity, false, false);
        lastDestination = target.position;
    }
    public override void stateUpdate()
    {
        //base.stateUpdate();
        if (rayTarget())
        {
            chaseTarget();
            if (Vector3.Distance(target.position, agent.agentTrn.transform.position) < combatDist)
            {
                //to combat
                combat state = new combat(target, agent);
            }
        }
        else if (Vector3.Distance(lastDestination, agent.agentTrn.transform.position) < destDist)
        {
            //to searchState
            searchState state = new searchState(target,ref agent);
        }
    }
}

public class searchState : states
{
    
    //bool turn = false;
    float plTrackTimer = 0;
    float trackTime = 0.5f;
    List<Vector3> playerPosList;
    float destDist = 0.5f;

    float stateTimer = 0;
    float stateLimit = 4;

    public searchState(Transform pl, ref npcAgents nav)
    {
        plTrn = pl;
        playerPosList = new List<Vector3>();
        playerPosList.Add(plTrn.position);
        agent = nav;
        agent.curState = this;
        agent.agentTrn.speed = 0.2f;
        sType = stateType.search;
    }
    public void trackPlayer()
    {
        plTrackTimer += Time.deltaTime;
        if (plTrackTimer > trackTime)
        {
            plTrackTimer = 0;
            Vector3 tmp = plTrn.position;
            playerPosList.Add(plTrn.position);
            if (playerPosList.Count > 5)
            {
                playerPosList.Remove(playerPosList[0]);
            }
        }
        if (playerPosList.Count == 0)
        {

        }
        if (Vector3.Distance(agent.agentTrn.transform.position, playerPosList[0]) < destDist)
        {
            if (playerPosList.Count > 0)
            {
                playerPosList.Remove(playerPosList[0]);
            }
        }
        else
        {
            agent.agentTrn.SetDestination(playerPosList[0]);
            agent.character.Move(agent.agentTrn.desiredVelocity, false, false);
        }
    }
    public override void stateUpdate()
    {
        //base.stateUpdate();
        trackPlayer();
        stateTimer += Time.deltaTime;
        if (stateTimer > stateLimit)
        {
            moveToState tmp = new moveToState(agent, plTrn);
            agent.curState = tmp;
            Debug.Log(stateTimer);
        }
    }
}
public class hitPlayer : states
{
    public hitPlayer(Transform target, npcAgents nav)
    {
        sType = stateType.hitPlayer;
        plTrn = target;
        agent = nav;
        agent.curState = this;
        agent.agentTrn.speed = 0.7f;
    }
}
public class combat : states
{
    float moveTime = 0;
    float stayTime = 0;
    bool dir = false;
    enum combatState
    {
        idle,
        move,
        hit
    }
    combatState cs = combatState.move;
    public combat(Transform target, npcAgents nav)
    {
        sType = stateType.combat;
        plTrn = target;
        agent = nav;
        agent.curState = this;
        agent.agentTrn.speed = 0.7f;
        agent.agentAnim.SetBool("toCombat", true);
        //agent.agentAnim.SetBool("OnGround", false);
    }
    public void idleUpdate()
    {

    }
    public void moveUpdate()
    {
        if (moveTime <= 0)
        {
            moveTime = Random.Range(1.5f, 3);
            if (Random.value >= 0.5f)
            {
                agent.agentAnim.SetBool("toCombatMoveRight", false);
                agent.agentAnim.SetBool("toCombatIdle", false);
                agent.agentAnim.SetBool("toCombatMoveLeft", true);
                dir = false;
            }
            else
            {
                agent.agentAnim.SetBool("toCombatMoveRight", true);
                agent.agentAnim.SetBool("toCombatIdle", false);
                agent.agentAnim.SetBool("toCombatMoveLeft", false);
                dir = true;
            }
        }
        else
        {
            moveTime -= Time.deltaTime;
            if (dir)
            {
                agent.agentTrn.Move(agent.agentTrn.transform.right * Time.deltaTime);
                agent.agentTrn.transform.LookAt(plTrn);
            }
            else
            {
                agent.agentTrn.Move(agent.agentTrn.transform.right * -1 * Time.deltaTime);
                agent.agentTrn.transform.LookAt(plTrn);
            }
        }
    }
    public override void stateUpdate()
    {
        if(cs == combatState.move)
        {
            moveUpdate();
        }else if(cs == combatState.idle)
        {
            //agent.agentAnim.SetBool("toCombatMoveRight", false);
            //agent.agentAnim.SetBool("toCombatIdle", true);
            //agent.agentAnim.SetBool("toCombatMoveLeft", false);
        }else if(cs == combatState.hit)
        {
            //agent.agentAnim.SetBool("toCombat", false);
            //to hit state;
            //agent.agentAnim.SetBool("toHit", true);
        }
        //base.stateUpdate();
        //placeNPC...
    }
}