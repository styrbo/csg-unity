using UnityEngine;

namespace CSG
{
	public class BooleanSettings
	{
		// Variables.
		public static float Epsilonf = 0.01f;
		public static bool MergeCoplanars = true;
		public static int BspOptimization = 1;

		public static bool DeleteSlaves = true;
		
		internal static void DestroySlaves(CSGObject[] csgs)
		{
            if(!DeleteSlaves)
	            return;

            foreach (var csg in csgs)
            {

#if UNITY_EDITOR
	            if (UnityEditor.EditorApplication.isPlaying == false)
	            {
					Object.DestroyImmediate(csg.gameObject);
	            }
	            else
	            {
#endif

		            Object.Destroy(csg.gameObject);
#if UNITY_EDITOR
	            }
#endif
            }
		}
	}
}