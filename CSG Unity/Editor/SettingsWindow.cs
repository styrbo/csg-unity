using UnityEngine;
using UnityEditor;

namespace CSG
{
	class SettingsWindow : EditorWindow
	{
		// Add menu item named "My Window" to the Window menu
		[MenuItem("CSG/Settings")]
		public static void ShowWindow()
		{
			//Show existing window instance. If one doesn't exist, make one.
			EditorWindow.GetWindow(typeof(SettingsWindow), true, "fhCSG Settings");
		}

		void OnGUI()
		{
			GUILayout.Label("Base Settings", EditorStyles.boldLabel);

			// 
			BooleanSettings.Epsilonf = Mathf.Clamp(EditorGUILayout.FloatField("Epsilon", BooleanSettings.Epsilonf),
				0.0001f, 0.2f);
			BooleanSettings.MergeCoplanars = EditorGUILayout.Toggle("Merge Coplanars", BooleanSettings.MergeCoplanars);

			BooleanSettings.BspOptimization = EditorGUILayout.Popup("Bsp Optimization", BooleanSettings.BspOptimization,
				new string[] {"Worse", "Average", "Best"});

			BooleanSettings.DeleteSlaves = EditorGUILayout.Toggle("Delete Slaves", BooleanSettings.DeleteSlaves);

		}
	}
}