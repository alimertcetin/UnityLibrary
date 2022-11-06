using System;

namespace XIV.Spline
{
    using UnityEngine;

    public class BezierSpline : MonoBehaviour
    {
        public int CurveCount => (points.Length - 1) / 3;

        public int ControlPointCount => points.Length;

        [SerializeField] Vector3[] points;

        public Vector3 GetPoint(int index)
        {
            return points[index];
        }

        public void SetPoint(int index, Vector3 point)
        {
            points[index] = point;
        }

        public void AddCurve()
        {
            Vector3[] newPoints = SplineUtils.NewCurveAtPosition(points[^1]);
            int pointsLength = points.Length;
            Array.Resize(ref points, pointsLength + newPoints.Length);
            for (int i = 0; i < newPoints.Length; i++)
            {
                points[pointsLength + i] = newPoints[i];
            }
        }

        public bool RemoveCurve(int index)
        {
            var isRemoved = SplineUtils.RemoveCurve(points, index, out var newPoints);
            if (isRemoved)
            {
                int newSize = newPoints.Length;
                Array.Resize(ref points, newSize);
                for (int i = 0; i < newSize; i++)
                {
                    points[i] = newPoints[i];
                }
                return true;
            }

            return false;
        }

        public Vector3 GetPointCubic(float t)
        {
            return transform.TransformPoint(SplineMath.GetPointCubic(points, t));
        }

        public Vector3 GetVelocityCubic(float t)
        {
            return transform.TransformPoint(SplineMath.GetVelocityCubic(points, t)) - transform.position;
        }

        public Vector3 GetDirection(float t)
        {
            return GetVelocityCubic(t).normalized;
        }

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
    }
}