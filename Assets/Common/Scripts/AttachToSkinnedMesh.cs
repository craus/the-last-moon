using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class AttachToSkinnedMesh : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMesh;
    [SerializeField] private int attachedVertexIndex;
    [SerializeField] private Vector3 attachedVertexPosition;
    [SerializeField] private List<SkinnedMeshBone> attachedVertexBones;
    [SerializeField] private Transform meshVertexTransform; 
    [SerializeField] private Transform attachedTransform;

    [ContextMenu("Attach")] 
    public void Attach() 
    {
        var mesh = new Mesh();
        skinnedMesh.BakeMesh(mesh);
        attachedVertexIndex = mesh.vertices.IndexOfMin(v => Vector3.SqrMagnitude(skinnedMesh.transform.TransformPoint(v) - transform.position));
        //attachedVertexPosition = skinnedMesh.transform.TransformPoint(mesh.vertices[attachedVertexIndex]);

        attachedVertexBones = SkinnedVertices.VertexBones(skinnedMesh, attachedVertexIndex);

        DeleteVertexTransform();
        CreateVertexTransform();

        //Debug.LogFormat("closestVertexIndex = {0}", attachedVertexIndex);
        //var localAttachedVertexPosition = 
        //transform.position = ;
    }

    private void DeleteVertexTransform()
    {
        if (meshVertexTransform != null)
        {
            Extensions.Destroy(meshVertexTransform.gameObject);
        }
    }

    private void CreateVertexTransform()
    {
        if (meshVertexTransform == null)
        {
            meshVertexTransform = new GameObject(string.Format("Vertex Transform [{0}]", gameObject.name)).transform;
            UpdateVertexTransform();
            attachedTransform = new GameObject(string.Format("Attached Transform [{0}]", gameObject.name)).transform;
            attachedTransform.SetParent(transform, worldPositionStays: false);
            attachedTransform.SetParent(meshVertexTransform);
        }
    }

    public void Update()
    {
        UpdateVertexTransform();
        UpdateAttachedTransform();
    }

    private void UpdateVertexTransform()
    {
        if (meshVertexTransform != null)
        {
            SkinnedVertices.SetTransform(meshVertexTransform, skinnedMesh, attachedVertexBones, attachedVertexIndex);
        }
    }

    private void UpdateAttachedTransform()
    {
        if (attachedTransform != null)
        {
            transform.position = attachedTransform.position;
            transform.rotation = attachedTransform.rotation;
        }
    }

    private void OnDrawGizmos()
    {
        if (meshVertexTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(meshVertexTransform.position, 0.003f);
        }
    }
}