using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
using Tools = GameCore.Tools;
#endif
[ExecuteInEditMode]
public class VertexColorMeshSetter : MonoBehaviour
{
    Color32 _color = Color.white;
    Mesh mesh = null;
    int countVertex = 0;
    Color32[] colors = null;
    [SerializeField] Mesh originalMesh;
    [SerializeField] MeshFilter mf;
#if UNITY_EDITOR
    private void OnValidate ()
    {
        mf = GetComponent<MeshFilter> ();
        originalMesh = Tools.GetAssetAtPath<Mesh> ("Assets/Models/Quad.asset");
    }
    [ContextMenu ("CreateQuad")]
    void CreateQuad ()
    {
        Tools.CreateAsset (mf.mesh, "Assets/Models/Quad.asset");
    }
#endif

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

    public float alpha { get => color.a; set => color = new Color32 (color.r, color.g, color.b, (byte) ((uint) (255 * value))); }

    private void Awake ()
    {
        Init ();
    }

    public void Init ()
    {
        if (mesh == null)
        {
            mesh = Instantiate (originalMesh);
            mf.mesh = mesh;
        }
        if (countVertex == 0)
            countVertex = mesh.vertices.Length;
        if (colors == null)
            colors = new Color32[countVertex];
    }

}
