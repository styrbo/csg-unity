using UnityEngine;
using System.Collections;

namespace CSG
{
	public class CubeBuilder : BrushBuilder
	{

		public static Material sharedMaterial;


		public Vector3 min;
		public Vector3 max;

		// Use this for initialization
		public override void Build()
		{
			min = new Vector3(-32.0f, -32.0f, -32.0f);
			max = new Vector3(32.0f, 32.0f, 32.0f);

			// 
			GameObject go = new GameObject();
			go.name = "CubeGeo";
			MeshFilter mf = go.AddComponent<MeshFilter>();
			MeshRenderer mr = go.AddComponent<MeshRenderer>();
			CSGObject csg = go.AddComponent<CSGObject>();

			if (mf.sharedMesh == null)
			{
				mf.sharedMesh = new Mesh();
			}

			Mesh mesh = mf.sharedMesh;

			Vector3 p0 = new Vector3(min.x, min.y, min.z);
			Vector3 p1 = new Vector3(min.x, min.y, max.z);
			Vector3 p2 = new Vector3(min.x, max.y, min.z);
			Vector3 p3 = new Vector3(min.x, max.y, max.z);
			Vector3 p4 = new Vector3(max.x, min.y, min.z);
			Vector3 p5 = new Vector3(max.x, min.y, max.z);
			Vector3 p6 = new Vector3(max.x, max.y, min.z);
			Vector3 p7 = new Vector3(max.x, max.y, max.z);

			mesh.Clear();

			mesh.vertices = new[]
			{
				// left
				p0, p1, p2, p3,
				// right
				p4, p5, p6, p7,
				// front
				p1, p3, p5, p7,
				// back
				p0, p2, p4, p6,
				// top
				p2, p3, p6, p7,
				// bottom
				p0, p1, p4, p5
			};

			mesh.uv = new[]
			{
				// left 
				Vector2.zero, Vector2.right, Vector2.up, Vector2.one, 
				// right
				Vector2.zero, Vector2.right, Vector2.up, Vector2.one, 
				// front
				Vector2.zero, Vector2.right, Vector2.up, Vector2.one, 
				// back
				Vector2.zero, Vector2.right, Vector2.up, Vector2.one, 
				// top
				Vector2.zero, Vector2.right, Vector2.up, Vector2.one, 
				// bottom
				Vector2.zero, Vector2.right, Vector2.up, Vector2.one
 			};

			mesh.triangles = new[]
			{
				// left
				0, 1, 2, 2, 1, 3,
				// right
				4, 6, 5, 5, 6, 7,
				// front
				8, 10, 9, 9, 10, 11,
				// back
				12, 13, 14, 14, 13, 15,
				// top
				16, 17, 18, 18, 17, 19,
				// bottom
				20, 22, 21, 21, 22, 23
			};

			mesh.RecalculateNormals();
			mesh.RecalculateBounds();

			if (sharedMaterial == null)
				sharedMaterial = new Material(Shader.Find("Unlit/Texture"));

			mr.sharedMaterial = sharedMaterial;
			//	mr.sharedMaterial.shader = Shader.Find("Unlit/Texture");

			Debug.Log(csg);
			Debug.Log(mesh);
			Debug.Log(sharedMaterial);
			
			mesh.Optimize();
		}
	}
}