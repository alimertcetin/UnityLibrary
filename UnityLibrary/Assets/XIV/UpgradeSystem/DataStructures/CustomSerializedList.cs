using System.Collections.Generic;
using UnityEngine;

namespace XIV.UpgradeSystem.DataStructures
{
    [System.Serializable]
    public class CustomSerializedList<T> : List<T>, ISerializationCallbackReceiver
    {
        [SerializeField]
        List<T> values = new List<T>();
        
        public void OnBeforeSerialize()
        {
            values.Clear();

            foreach (var lv in this)
            {
                values.Add(lv);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            for(int i=0; i<values.Count; i++) 
                Add(values[i]);
            
            values.Clear();
        }
    }
}