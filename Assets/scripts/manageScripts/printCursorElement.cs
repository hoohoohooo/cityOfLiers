using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class printCursorElement : MonoBehaviour
{
    public static printCursorElement instance = null;
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    cursorElementCollection col;
    public Material mat = null;
    bool request = false;
    private void OnPostRender()
    {
        if (questManager.instance.focusedQuest != null)
        {
            if (request == true)
            {
                //print();
                GL.PushMatrix();
                mat.SetPass(0);
                GL.LoadOrtho();

                for (int i = 0; i < 4; i++)
                {
                    printLine(col.square[i]);
                    //GL.Begin(GL.LINES);
                    //GL.Color(Color.red);
                    //GL.Vertex(col.square[i].start);
                    //GL.Vertex(col.square[i].end);
                    //GL.End();

                }
                GL.Begin(GL.LINES);
                GL.Color(Color.red);
                GL.Vertex(col.lineToPoint.start);
                //GL.Vertex(new Vector3(0.5f, 0.5f, 0));
                GL.Color(Color.clear);
                GL.Vertex(col.lineToPoint.end);
                //GL.Vertex(new Vector3(1, 1, 0));
                GL.End();
                GL.PopMatrix();
            }
        }
    }
    public void getRequest(bool req,Material m,cursorElementCollection c)
    {
        request = req;
        //mat = m;
        col = c;
    }
    void print()
    {
        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadOrtho();
        
        //for(int i = 0; i < 4; i++)
        //{
        //    printLine(col.square[i]);
        //}
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(col.lineToPoint.start);
        GL.Color(Color.clear);
        GL.Vertex(col.lineToPoint.end);
        GL.End();
        GL.PopMatrix();

    }
    void printLine(lineElement ln)
    {
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(ln.start);
        GL.Vertex(ln.end);
        GL.End();
    }
}
