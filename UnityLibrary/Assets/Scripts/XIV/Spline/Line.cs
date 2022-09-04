using System;
using UnityEngine;

namespace XIV.Spline
{
    public class Line : MonoBehaviour
    {
        public Vector3 p0;
        public Vector3 p1;

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            Gizmos.DrawIcon(transform.position, "lineIcon.ico", true);
        }
#endif
    }
}
