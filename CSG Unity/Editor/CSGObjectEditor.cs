using System.Linq;
using UnityEngine;
using UnityEditor;

namespace CSG
{

    [CustomEditor(typeof(CSGObject))]
    class CSGObjectEditor : Editor
    {
        // Use this for initialization
        public override void OnInspectorGUI()
        {
            CSGObject obj = (CSGObject) target;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Intersect"))
            {
                // find game objects (TODO: check if they are touching us)
                Object[] others = FindObjectsOfType(typeof(GameObject));
                GameObject[] gos = new GameObject[others.Length];
                int i = 0;
                foreach (GameObject gameObj in others)
                {
                    gos[i] = gameObj;
                    ++i;
                }

                var gosCSG = gos.Select(Boolean.FromGameObjectToCSGObject).ToArray();

                obj.PerformCSG(CsgOperation.ECsgOperation.CsgOper_Intersect, gosCSG);
            }

            if (GUILayout.Button("DeIntersect"))
            {
                // find game objects (TODO: check if they are touching us)
                Object[] others = FindObjectsOfType(typeof(GameObject));
                GameObject[] gos = new GameObject[others.Length];
                int i = 0;
                foreach (GameObject gameObj in others)
                {
                    gos[i] = gameObj;
                    ++i;
                }
                
                var gosCSG = gos.Select(Boolean.FromGameObjectToCSGObject).ToArray();

                obj.PerformCSG(CsgOperation.ECsgOperation.CsgOper_DeIntersect, gosCSG);
            }

            GUILayout.Button("...");
            GUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Texturing");
            obj.texMode = EditorGUILayout.Popup(obj.texMode, new string[] {"Original", "Planar Mapping"});

            EditorGUILayout.EndHorizontal();

            // add 
            if (obj.texMode == CSGObject.TexMode_Planar)
            {
                // offset u
                EditorGUILayout.BeginHorizontal();
                float newTexOffsetU = EditorGUILayout.FloatField("OffsetU", obj.globalTexOffsetU);

                if (obj.globalTexOffsetU != newTexOffsetU)
                {
                    obj.globalTexOffsetU = newTexOffsetU;
                    obj.TransferFacesToMesh();
                }

                EditorGUILayout.EndHorizontal();

                // offset v
                EditorGUILayout.BeginHorizontal();

                float newTexOffsetV = EditorGUILayout.FloatField("OffsetV", obj.globalTexOffsetV);

                if (obj.globalTexOffsetV != newTexOffsetV)
                {
                    obj.globalTexOffsetV = newTexOffsetV;
                    obj.TransferFacesToMesh();
                }

                EditorGUILayout.EndHorizontal();


                // scale u
                EditorGUILayout.BeginHorizontal();
                float newTexScaleU = EditorGUILayout.FloatField("ScaleU", obj.globalTexScaleU);
                newTexScaleU = Mathf.Clamp(newTexScaleU, 0.001f, 16.0f);

                if (obj.globalTexScaleU != newTexScaleU)
                {
                    obj.globalTexScaleU = newTexScaleU;
                    obj.TransferFacesToMesh();
                }

                EditorGUILayout.EndHorizontal();

                // scale v
                EditorGUILayout.BeginHorizontal();
                float newTexScaleV = EditorGUILayout.FloatField("ScaleV", obj.globalTexScaleV);
                newTexScaleV = Mathf.Clamp(newTexScaleV, 0.001f, 16.0f);

                if (obj.globalTexScaleV != newTexScaleV)
                {
                    obj.globalTexScaleV = newTexScaleV;
                    obj.TransferFacesToMesh();
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
        }
    }
}