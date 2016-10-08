using UnityEngine;
using System.Collections;

[RequireComponent ( typeof(MeshFilter), typeof(MeshRenderer) )]
public class GenerateMesh : MonoBehaviour {
	Mesh mesh;
	MeshFilter meshFilter;
	float width = 1f;
	float height = 1f;
	float stepSize = 0.01f;
	bool isGrowing = false;

	void Start()
	{
		mesh = new Mesh();
		mesh.name = "GeneratedMesh";
		meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh = mesh;
		
	}
	void Update()
	{
		if( isGrowing && width > 1.5f )
		{
			isGrowing = false;
		}
		else if( !isGrowing && width < 0.5f )
		{
			isGrowing = true;
		}

		if( isGrowing )
		{
			width += stepSize;
		} 
		else
		{
			width -= stepSize;
		}

		UpdateMesh();
	}
	void UpdateMesh()
	{
		Vector3[] vertices = new [] {
			new Vector3( 0f, 0f, 0f ),
			new Vector3( width, 0f, 0f ),
			new Vector3( 0f, height, 0f ),
			new Vector3( width, height, 0f )
		};
		
		int[] triangles = new [] {
			0, 2, 1,
			2, 3, 1
		};
		
		Vector3[] normals = new [] {
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward
		};

		Vector2[] uv = new [] {
			new Vector2( 0, 0 ),
			new Vector2( 1, 0 ),
			new Vector2( 0, 1 ),
			new Vector2( 1, 1 )
		};
		
		mesh.vertices = vertices;	
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
	}

}
