namespace XIV.Spline
{
    using UnityEngine;

    public class BezierCurve : MonoBehaviour
    {
        public Vector3[] points;
        
        public void Reset()
        {
            points = new Vector3[]
            {
                new Vector3(1f, 0f, 0f),
                new Vector3(2f, 0f, 0f),
                new Vector3(3f, 0f, 0f),
                new Vector3(4f, 0f, 0f)
            };
        }

        public Vector3 GetPointQuadratic(float t)
        {
            return transform.TransformPoint(Bezier.GetPointQuadratic(points[0], points[1], points[2], t));
        }

        public Vector3 GetVelocityQuadratic(float t)
        {
            return transform.TransformPoint(Bezier.GetFirstDerivativeQuadratic(points[0], points[1], points[2], t)) - transform.position;
        }

        public Vector3 GetDirectionQuadratic(float t)
        {
            return GetVelocityQuadratic(t).normalized;
        }
        
        public Vector3 GetDirectionCubic(float t)
        {
            return GetVelocityCubic(t).normalized;
        }

        public Vector3 GetPointCubic(float t)
        {
            return transform.TransformPoint(Bezier.GetPointCubic(points[0], points[1], points[2], points[3], t));
        }

        public Vector3 GetVelocityCubic(float t)
        {
            return transform.TransformPoint(Bezier.GetFirstDerivativeCubic(points[0], points[1], points[2], points[3], t)) - transform.position;
        }

    }
}