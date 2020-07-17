/* this repository was forked from https://github.com/luho383/csg-unity
 * and modify by https://github.com/styrbo
 * https://github.com/styrbo/csg-unity
 */

using UnityEngine;

namespace CSG
{
    public static class Boolean
    {
        public static void Union(GameObject from, params GameObject[] to)
        {
            FromGameObjectToCSGObject(from, to, out CSGObject csgForm, out CSGObject[] csgTo);

            Union(csgForm, csgTo);
        }

        public static void Union(CSGObject from, params CSGObject[] to)
        {
            from.PerformCSG(CsgOperation.ECsgOperation.CsgOper_Additive, to);

            BooleanSettings.DestroySlaves(to);
        }

        public static void Subtract(GameObject from, params GameObject[] to)
        {
            FromGameObjectToCSGObject(from, to, out CSGObject csgForm, out CSGObject[] csgTo);

            Subtract(csgForm, csgTo);
        }

        public static void Subtract(CSGObject from, params CSGObject[] to)
        {
            from.PerformCSG(CsgOperation.ECsgOperation.CsgOper_Subtractive, to);

            BooleanSettings.DestroySlaves(to);
        }

        public static void FromGameObjectToCSGObject(GameObject from, GameObject[] to, out CSGObject csgForm,
            out CSGObject[] csgTo)
        {
            csgForm = from.GetComponent<CSGObject>();
            csgTo = new CSGObject[to.Length];

            if (!csgForm)
                csgForm = AddComponent(from);

            for (var i = 0; i < to.Length; i++)
            {
                var temp = to[i].GetComponent<CSGObject>();

                if (!temp)
                    csgTo[i] = AddComponent(to[i]);
            }
        }

        public static CSGObject FromGameObjectToCSGObject(GameObject from)
        {
            var to = from.GetComponent<CSGObject>();

            if (!to)
                to = AddComponent(from);

            return to;
        }

        public static CSGObject AddComponent(GameObject obj)
        {
            Mesh mesh = obj.GetComponent<MeshFilter>() != null ? obj.GetComponent<MeshFilter>().sharedMesh : null;

            if (mesh)
            {
                // TODO: add bsp tree generation on add... 
                CSGObject csg = obj.AddComponent<CSGObject>();
                MeshFilter filter = obj.GetComponent<MeshFilter>();

                // clone mesh as every csg object needs his own mesh
                filter.sharedMesh = ObjectCloner.CloneMesh(filter.sharedMesh);
                // 
                csg.CreateFromMesh();

                return csg;
            }
            else
            {
                Debug.Log("No Mesh on GameObject");
                return null;
            }
        }
    }
}