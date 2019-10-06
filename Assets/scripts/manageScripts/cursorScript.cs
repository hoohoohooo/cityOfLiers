using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;


public struct lineElement
{
    public Vector3 start;
    public Vector3 end;
}
public struct cursorElementCollection
{
    public lineElement lineToPoint;
    public lineElement[] square;
}

public class cursorScript : MonoBehaviour
{
    public Material mat;
    private Vector3 startVertex;
    private Vector3 mousePos;

    public cursorElementCollection questCursor;
    public printCursorElement printer;
    public Camera cam;

    public Transform MainCursor;

    public float lineAmountScaler = 30;

    Vector3 pointOrigin;
    Vector3 dir;
    float sz;
    public void placeMainCursor()
    {
        //dir = questManager.instance.focusedQuest.destination - gameMng.instance.player.position;
        dir = questManager.instance.activeMainQuest.questList[questManager.instance.activeMainQuest.questIndex].destination - gameMng.instance.player.position;
        if (Vector3.Dot(cam.transform.forward, dir) < 0)
        {
            pointOrigin = new Vector3(-100, -1000, 0);
        }
        else
        {
            pointOrigin = cam.WorldToScreenPoint(questManager.instance.activeMainQuest.questList[questManager.instance.activeMainQuest.questIndex].destination);
        }
        if (questManager.instance.activeMainQuest.questList[questManager.instance.activeMainQuest.questIndex].objType == quest.objectiveType.place)
        {
            sz = lineAmountScaler / (Vector3.Distance(questManager.instance.activeMainQuest.questList[questManager.instance.activeMainQuest.questIndex].destination, gameMng.instance.player.position) / 2);
            if (sz > 200)
            {
                sz = 200;
            }
            pointOrigin.y += sz;
        }else if(questManager.instance.activeMainQuest.questList[questManager.instance.activeMainQuest.questIndex].objType == quest.objectiveType.npc)
        {
            sz = lineAmountScaler / (Vector3.Distance(questManager.instance.activeMainQuest.questList[questManager.instance.activeMainQuest.questIndex].objectiveNPC.transform.position, gameMng.instance.player.position) / 2);
            if (sz > 200)
            {
                sz = 200;
            }
            pointOrigin.y += sz;
        }
        MainCursor.position = pointOrigin;
        drawCursorSquare(pointOrigin, sz);
    }
    int minusAmount = 15;
    void drawCursorSquare(Vector3 org, float sz)
    {
        questCursor.square[0].start = new Vector3((pointOrigin.x - minusAmount) / Screen.width, pointOrigin.y / Screen.height, 0);
        questCursor.square[0].end = new Vector3(pointOrigin.x / Screen.width, (pointOrigin.y - minusAmount) / Screen.height, 0);
        questCursor.square[1].start = new Vector3(pointOrigin.x / Screen.width, (pointOrigin.y - minusAmount) / Screen.height, 0);
        questCursor.square[1].end = new Vector3((pointOrigin.x + minusAmount) / Screen.width, pointOrigin.y / Screen.height, 0);
        questCursor.square[2].start = new Vector3((pointOrigin.x + minusAmount) / Screen.width, pointOrigin.y / Screen.height, 0);
        questCursor.square[2].end = new Vector3(pointOrigin.x / Screen.width, (pointOrigin.y + minusAmount) / Screen.height, 0);
        questCursor.square[3].start = new Vector3(pointOrigin.x / Screen.width, (pointOrigin.y + minusAmount) / Screen.height, 0);
        questCursor.square[3].end = new Vector3((pointOrigin.x - minusAmount) / Screen.width, pointOrigin.y / Screen.height, 0);
        questCursor.lineToPoint.start = new Vector3(pointOrigin.x / Screen.width, (pointOrigin.y - minusAmount) / Screen.height, 0);
        questCursor.lineToPoint.end = new Vector3(pointOrigin.x / Screen.width, (pointOrigin.y - sz) / Screen.height, 0);
        //printRequest;
        printer.getRequest(true, mat, questCursor);
    }

    private void Start()
    {
        questCursor = new cursorElementCollection();
        questCursor.lineToPoint = new lineElement();
        questCursor.square = new lineElement[4];
    }


    void Update()
    {
        //mousePos = Input.mousePosition;
        //if (Input.GetKeyDown(KeyCode.Space))
        //    startVertex = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0);
        //if(questManager.instance.focusedQuest != null)
        //{
        //    placeMainCursor();
        //}
        //else
        //{
        //    printer.getRequest(false, mat, questCursor);
        //}
        if (questManager.instance.activeMainQuest != null)
        {
            if (questManager.instance.activeMainQuest.questList.Count > 0)
            {
                placeMainCursor();
            }
            else
            {
                MainCursor.position = new Vector3(-100, -1000, 0);
            }
        }
        else
        {
            MainCursor.position = new Vector3(-100, -1000, 0);
        }
    }




    ////void OnPostRender()
    ////{
    ////    if (!mat)
    ////    {
    ////        Debug.LogError("Please Assign a material on the inspector");
    ////        return;
    ////    }
    ////    
    ////}
    //private void OnEnable()
    //{
    //    RenderPipeline.beginFrameRendering += RenderPipeline_beginFrameRendering;
    //}

    //private void OnDisable()
    //{
    //    RenderPipeline.beginFrameRendering -= RenderPipeline_beginFrameRendering;
    //}

    //private void RenderPipeline_beginFrameRendering(Camera[] obj)
    //{
    //    OnPostRender();
    //}

    //private void OnPostRender()
    //{
    //    GL.PushMatrix();
    //    mat.SetPass(0);
    //    GL.LoadOrtho();
    //    GL.Begin(GL.LINES);
    //    GL.Color(Color.red);
    //    GL.Vertex(startVertex);
    //    GL.Color(Color.clear);
    //    GL.Vertex(new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0));
    //    GL.End();
    //    GL.PopMatrix();
    //}
    void Example()
    {
        startVertex = new Vector3(0, 0, 0);
    }
}
