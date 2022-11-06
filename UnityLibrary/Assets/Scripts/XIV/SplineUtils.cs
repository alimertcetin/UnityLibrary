using System.Collections.Generic;
using UnityEngine;

namespace XIV.Spline
{
	public static class SplineUtils
	{
		public static Vector3[] NewCurve()
		{
			return NewCurveAtPosition(Vector3.zero);
		}
		
		public static Vector3[] NewCurveAtPosition(Vector3 point)
		{
			Vector3[] points = new Vector3[3];
			point.x += 2f;
			point.z += 2f;
			points[0] = point;
			point.x += 1f;
			point.z -= 4f;
			points[1] = point;
			point.x += 2f;
			point.z += 2f;
			points[2] = point;
			return points;
		}
		
		public static bool RemoveCurve(IList<Vector3> points, int index, out Vector3[] newPoints)
		{
			if (index <= 1 || index >= points.Count - 2)
			{
#if UNITY_EDITOR
				Debug.LogWarning("Removing first and last curves isnt allowed");
#endif
				newPoints = default;
				return false;
			}

			var controlPointIndex = SplineMath.IndexOfControlPoint(index);
            
			var pointList = new List<Vector3>(points);
			pointList.RemoveRange(controlPointIndex - 1, 3);
			newPoints = pointList.ToArray();
			return true;
		}
	}
}