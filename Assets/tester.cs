using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tester : MonoBehaviour
{
    public Material mat;
    private Vector3 startVertex;
    private Vector3 mousePos;
    void Update()
    {
        mousePos = Input.mousePosition;
        if (Input.GetKeyDown(KeyCode.Space))
            startVertex = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0);

    }
    void OnPostRender()
    {
        if (!mat)
        {
            Debug.LogError("Please Assign a material on the inspector");
            return;
        }
        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadOrtho();
        GL.Begin(GL.LINES);
        GL.Color(Color.red);
        GL.Vertex(startVertex);
        GL.Vertex(new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, 0));
        GL.End();
        GL.PopMatrix();
    }
}
