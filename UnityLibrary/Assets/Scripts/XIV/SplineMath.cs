using System.Collections.Generic;
using UnityEngine;

namespace XIV.Spline
{
	public static class SplineMath
	{
		public static Vector3 GetPointCubic(IList<Vector3> points, float t)
		{
			int curveCount = (points.Count - 1) / 3;
			int index;
			if (t >= 1f)
			{
				t = 1f;
				index = points.Count - 4;
			}
			else
			{
				t = Mathf.Clamp01(t) * curveCount;
				index = (int)t;
				t -= index;
				index *= 3;
			}

			return Bezier.GetPointCubic(points[index], points[index + 1], points[index + 2], points[index + 3], t);
		}
        
		public static Vector3 GetVelocityCubic(IList<Vector3> points, float t)
		{
			int curveCount = (points.Count - 1) / 3;
			int index;
			if (t >= 1f)
			{
				t = 1f;
				index = points.Count - 4;
			}
			else
			{
				t = Mathf.Clamp01(t) * curveCount;
				index = (int)t;
				t -= index;
				index *= 3;
			}

			return Bezier.GetFirstDerivativeCubic(points[index], points[index + 1], points[index + 2], points[index + 3], t);
		}
        
		/// <summary>
		/// Returns control point index of anchor
		/// </summary>
		/// <param name="anchorIndex">Anchor index</param>
		/// <returns>Control point index of anchor</returns>
		public static int IndexOfControlPoint(int anchorIndex)
		{
			int mod = anchorIndex % 3;
			if (mod == 0) return anchorIndex;
            
			// mod == 2 previous anchor, mod == 1 next anchor
			return mod == 2 ? anchorIndex + 1 : anchorIndex - 1;
		}

		/// <summary>
		/// Returns true if index is an anchor point, false otherwise
		/// </summary>
		/// <param name="index">The index of point</param>
		/// <returns>True if index is anchor point, false otherwise</returns>
		public static bool IsAnchorPoint(int index)
		{
			return IndexOfControlPoint(index) != index;
		}
	}
}