using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace gamedev.utilities.debug
{
    [CreateAssetMenu(menuName = "Utilities/Debug/Debug Commands")]
    public class DebugCommandCollection : ScriptableObject
    {
        public List<DebugCommandSO> Collection;

    #if UNITY_EDITOR
        private void OnValidate()
        {
            Refresh();               
        }

        private void Refresh() {
            Collection.Clear();
            string[] assetGuids = UnityEditor.AssetDatabase.FindAssets("t:DebugCommandSO", null);

            foreach (string assetGuid in assetGuids)
            {
                var path = UnityEditor.AssetDatabase.GUIDToAssetPath(assetGuid);
                DebugCommandSO t = (DebugCommandSO) UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(DebugCommandSO));
                Collection.Add(t);
            }
        }
    #endif

    }
}