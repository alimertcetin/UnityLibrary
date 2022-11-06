using System;
using UnityEngine;
using XIV.Math;
using XIV.Spline;

namespace Examples
{
	public class SplineFollower : MonoBehaviour
	{
		public BezierSpline spline;

		public float moveSpeed;
		float timePassed;
		float totalMovementDuration;
		
		void Update()
		{
			totalMovementDuration = spline.Length / moveSpeed;
			timePassed = (timePassed + Time.deltaTime) % totalMovementDuration;
			var normalizedTime = timePassed / totalMovementDuration;

			var pos = spline.transform.position + spline.GetPoint(normalizedTime);
			transform.position = pos;
		}
	}
}
