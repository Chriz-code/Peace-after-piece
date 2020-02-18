using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BouncyText : MonoBehaviour, IMeshModifier
{
    public Mesh newMesh = null;
    public int verts;

    public void ModifyMesh(Mesh mesh)
    {
        Debug.Log("DAD");
        /*Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        int i = 0;
        while (i < vertices.Length)
        {
            vertices[i] += normals[i] * Mathf.Sin(Time.time);
            i++;
        }
        mesh.vertices = vertices;*/
    }
    public void ModifyMesh(VertexHelper verts)
    {
        Debug.Log("MOM");

        //this.verts = verts.currentVertCount;
        // verts.AddVert(Vector3.zero, new Color32(1, 1, 1, 1), Vector2.zero);
        verts.Clear();
        for (int i = 0; i < 3; i++)
        {
            verts.AddVert(new Vector3(1*i, 1*i, 1*i), new Color32(1, 1, 1, 1), Vector2.zero);
        }
        verts.AddTriangle(0, 2, 1);
        //verts.SetUIVertex(new UIVertex(),)
    }
}
