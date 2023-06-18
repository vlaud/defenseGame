using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Defense
{
    public static class GameObjectExtensions
    {
        public static void SetLayerInChildren(this GameObject parent, int layer)
        {
            parent.layer = layer;
            if (parent.transform.childCount <= 0)
            {
                return;
            }

            for (int i = 0; i < parent.transform.childCount; i++)
            {
                var obj = parent.transform.GetChild(i).gameObject;
                obj.layer = layer;
                if (obj.transform.childCount > 0)
                {
                    obj.SetLayerInChildren(layer);
                }
            }
        }
    }
}
