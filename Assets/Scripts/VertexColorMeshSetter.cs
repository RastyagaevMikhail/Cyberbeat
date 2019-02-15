using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[ExecuteInEditMode]
public class VertexColorMeshSetter : MonoBehaviour
{
    Color32 _color = Color.white;
    Mesh mesh = null;
    int countVertex = 0;
    Color32[] colors = null;

    public Color32 color
    {
        get => _color;
        set
        {
            _color = value;
            if (mesh == null) Init ();
            for (int i = 0; i < countVertex; i++)
                colors[i] = _color;
            mesh.colors32 = colors;
        }
    }

    public float alpha { get => color.a; set => color = new Color32 (color.r, color.g, color.b, (byte)((uint)(255*value)) ); }

    private void Awake ()
    {
        Init ();
    }

    public void Init ()
    {
        if (mesh == null)
            mesh = GetComponent<MeshFilter> ().mesh;
        if (countVertex == 0)
            countVertex = mesh.vertices.Length;
        if (colors == null)
            colors = new Color32[countVertex];
    }


}
