using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class ParticleSystemCustom : MonoBehaviour
{
    [SerializeField]
    private int MaxQuads = 1000;

    private Mesh m_mesh;

    private Vector3[] m_vertices;
    private Vector2[] m_uvs;
    private int[]     m_indices;

    private int m_index = 0;

    private bool m_updateVerts = true;
    private bool m_updateMesh = true;

    private void Awake()
    {
        m_mesh = new();

        m_vertices = new Vector3[MaxQuads * 4];
        m_uvs = new Vector2[MaxQuads * 4];
        m_indices = new int[MaxQuads * 6];

        m_mesh.vertices = m_vertices;
        m_mesh.uv = m_uvs;
        m_mesh.triangles = m_indices;
        m_mesh.bounds = new Bounds(Vector3.zero, Vector3.one * 1000.0f);

        GetComponent<MeshFilter>().mesh = m_mesh;
    }

    private void LateUpdate()
    {
        if(m_updateVerts)
        {
            m_mesh.vertices = m_vertices;
        }
        if(m_updateMesh)
        {
            m_mesh.uv = m_uvs;
            m_mesh.triangles = m_indices;
        }

        m_updateVerts = false;
        m_updateMesh = false;
    }

    public int AddQuad(Vector2 position, Vector2 size, float rotation = 0.0f, int uvIndex = 0, int uvSize = 1)
    {
        int index = m_index;

        UpdateQuad(m_index, position, size, rotation, uvIndex, uvSize);

        m_index = (m_index + 1) % MaxQuads;
        
        return index;
    }

    public void UpdateQuad(int index, Vector2 position, Vector2 size, float rotation, int uvIndex, int uvSize)
    {
        Vector3 pos = (Vector3)position;
        Vector3 s = (Vector3)size;

        if(size.x != size.y)
        {
            m_vertices[4 * index + 0] = pos + Quaternion.Euler(0.0f, 0.0f, rotation) * (-s);
            m_vertices[4 * index + 1] = pos + Quaternion.Euler(0.0f, 0.0f, rotation) * new Vector3(-s.x, s.y);
            m_vertices[4 * index + 2] = pos + Quaternion.Euler(0.0f, 0.0f, rotation) * s;
            m_vertices[4 * index + 3] = pos + Quaternion.Euler(0.0f, 0.0f, rotation) * new Vector3(s.x, -s.y);
        }
        else
        {
            m_vertices[4 * index + 0] = pos + Quaternion.Euler(0.0f, 0.0f, rotation - 180.0f) * s;
            m_vertices[4 * index + 1] = pos + Quaternion.Euler(0.0f, 0.0f, rotation - 270.0f) * s;
            m_vertices[4 * index + 2] = pos + Quaternion.Euler(0.0f, 0.0f, rotation) * s;
            m_vertices[4 * index + 3] = pos + Quaternion.Euler(0.0f, 0.0f, rotation - 90.0f) * s;
        }
        

        Vector2 uvDist = Vector2.one / uvSize;
        m_uvs[4 * index + 0] = new Vector2(0.0f, 0.0f) + uvIndex * uvDist;
        m_uvs[4 * index + 1] = new Vector2(0.0f, uvDist.y) + uvIndex * uvDist;
        m_uvs[4 * index + 2] = new Vector2(uvDist.x, uvDist.y) + uvIndex * uvDist;
        m_uvs[4 * index + 3] = new Vector2(uvDist.x, 0.0f) + uvIndex * uvDist;

        m_indices[6 * index + 0] = index * 4 + 0;
        m_indices[6 * index + 1] = index * 4 + 1;
        m_indices[6 * index + 2] = index * 4 + 2;
        m_indices[6 * index + 3] = index * 4 + 0;
        m_indices[6 * index + 4] = index * 4 + 2;
        m_indices[6 * index + 5] = index * 4 + 3;

        m_updateVerts = true;
        m_updateMesh = true;
    }

    public void UpdateQuadVertices(int index, Vector2 position, Vector2 size, float rotation)
    {
        Vector3 pos = (Vector3)position;
        Vector3 s = (Vector3)size;

        if (size.x != size.y)
        {
            m_vertices[4 * index + 0] = pos + Quaternion.Euler(0.0f, 0.0f, rotation) * (-s);
            m_vertices[4 * index + 1] = pos + Quaternion.Euler(0.0f, 0.0f, rotation) * new Vector3(-s.x, s.y);
            m_vertices[4 * index + 2] = pos + Quaternion.Euler(0.0f, 0.0f, rotation) * s;
            m_vertices[4 * index + 3] = pos + Quaternion.Euler(0.0f, 0.0f, rotation) * new Vector3(s.x, -s.y);
        }
        else
        {
            m_vertices[4 * index + 0] = pos + Quaternion.Euler(0.0f, 0.0f, rotation - 180.0f) * s;
            m_vertices[4 * index + 1] = pos + Quaternion.Euler(0.0f, 0.0f, rotation - 270.0f) * s;
            m_vertices[4 * index + 2] = pos + Quaternion.Euler(0.0f, 0.0f, rotation) * s;
            m_vertices[4 * index + 3] = pos + Quaternion.Euler(0.0f, 0.0f, rotation - 90.0f) * s;
        }

        m_updateVerts = true;
    }

    public void AddToQuadPosition(int index, Vector2 positionAddition)
    {
        Vector3 addition = (Vector3)positionAddition;

        m_vertices[4 * index + 0] += addition;
        m_vertices[4 * index + 1] += addition;
        m_vertices[4 * index + 2] += addition;
        m_vertices[4 * index + 3] += addition;

        m_updateVerts = true;
    }
}
