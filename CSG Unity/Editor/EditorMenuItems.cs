using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CSG
{
#if UNITY_EDITOR
    static class EditorMenuItems
    {
        [MenuItem("CSG/Create Cube")]
        static void CreateCube()
        {
            BrushBuilder builder = new CubeBuilder();
            builder.Build();
        }

        [MenuItem("CSG/Union")]
        static void Union()
        {
            if (Selection.gameObjects.Length <= 1)
                return;

            List<GameObject> sortObj = new List<GameObject>(Selection.gameObjects);
            sortObj.Remove(Selection.activeGameObject);
            
            Boolean.Union(Selection.activeGameObject, sortObj.ToArray());
        }

        [MenuItem("CSG/Subtract")]
        static void Subtract()
        {
            if (Selection.gameObjects.Length <= 1)
                return;

            List<GameObject> sortObj = new List<GameObject>(Selection.gameObjects);
            sortObj.Remove(Selection.activeGameObject);
            
            Boolean.Subtract(Selection.activeGameObject, sortObj.ToArray());
        }

        [MenuItem("CSG/Add Component")]
        static void AddComponent()
        {
            for (int i = 0; i < Selection.gameObjects.Length; ++i)
            {
                Boolean.AddComponent(Selection.gameObjects[i]);
            }
        }


        [MenuItem("CSG/Debug/Build BSP Tree")]
        static void BuildTree()
        {
            for (int i = 0; i < Selection.gameObjects.Length; ++i)
            {
                GameObject go = Selection.gameObjects[i];
                CSGObject obj = go.GetComponent<CSGObject>();

                if (obj)
                {
                    obj.CreateFromMesh();

                    BspGen gen = new BspGen(BooleanSettings.BspOptimization);

                    obj.rootNode = gen.GenerateBspTree(obj.faces);
                }
            }
        }

        [MenuItem("CSG/Debug/BSP Faces to Mesh")]
        static void FacesToMesh()
        {
            for (int i = 0; i < Selection.gameObjects.Length; ++i)
            {
                GameObject go = Selection.gameObjects[i];
                CSGObject obj = go.GetComponent<CSGObject>();

                if (obj)
                {
                    obj.TransferFacesToMesh();
                }
            }
        }

        [MenuItem("CSG/Debug/Merge BSP Faces")]
        static void MergeBspFaces()
        {
            for (int i = 0; i < Selection.gameObjects.Length; ++i)
            {
                GameObject go = Selection.gameObjects[i];
                CSGObject obj = go.GetComponent<CSGObject>();

                if (obj)
                {
                    obj.MergeFaces();
                }
            }
        }


        [MenuItem("CSG/Debug/Mesh to BSP Faces")]
        static void MeshToFaces()
        {
            for (int i = 0; i < Selection.gameObjects.Length; ++i)
            {
                GameObject go = Selection.gameObjects[i];
                CSGObject obj = go.GetComponent<CSGObject>();

                if (obj)
                {
                    obj.TransferFacesToMesh();
                }
            }
        }


        [MenuItem("CSG/Debug/Mesh Optimize")]
        static void MeshOptimize()
        {
            for (int i = 0; i < Selection.gameObjects.Length; ++i)
            {
                GameObject go = Selection.gameObjects[i];
                CSGObject obj = go.GetComponent<CSGObject>();

                if (obj)
                {
                    obj.Optimize();
                }
            }
        }


        [MenuItem("CSG/Debug/Draw Debug")]
        static void DrawDebug()
        {
            for (int i = 0; i < Selection.gameObjects.Length; ++i)
            {
                GameObject go = Selection.gameObjects[i];
                CSGObject obj = go.GetComponent<CSGObject>();

                if (obj)
                {
                    obj.DrawDebug();
                }
            }
        }
    }
#endif
}