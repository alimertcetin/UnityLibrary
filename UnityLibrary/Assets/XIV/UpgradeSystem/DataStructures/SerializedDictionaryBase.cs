using System.Collections.Generic;
using UnityEngine;

namespace XIV.UpgradeSystem.DataStructures
{
    [System.Serializable]
    public class SerializedDictionaryBase<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver
    {
        [SerializeField]
        List<K> keys = new List<K>();

        [SerializeField]
        List<V> values = new List<V>();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (var kvp in this)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            for (int i = 0; i < keys.Count; i++)
                Add(keys[i], values[i]);

            keys.Clear();
            values.Clear();
        }
    }
}