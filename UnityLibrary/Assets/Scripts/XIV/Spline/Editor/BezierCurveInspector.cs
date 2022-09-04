using System;

namespace XIV.Spline.Editor
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(BezierCurve))]
    public class BezierCurveInspector : Editor
    {
        BezierCurve curve;
        Transform handleTransform;
        Quaternion handleRotation;
        const int lineSteps = 10;
        const float directionScale = 0.5f;

        GameObject gameObject;
        
        void OnEnable()
        {
            gameObject = (target as BezierCurve).gameObject;
            gameObject.transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
            Tools.hidden = true;
        }

        void OnDisable()
        {
            if (gameObject != null)
            {
                gameObject.transform.hideFlags = HideFlags.None;
                Tools.hidden = false;
            }
        }

        void OnSceneGUI()
        {
            curve = target as BezierCurve;
            // ReSharper disable once PossibleNullReferenceException
            handleTransform = curve.transform;
            handleRotation = Tools.pivotRotation == PivotRotation.Local ? handleTransform.rotation : Quaternion.identity;

            var p0 = ShowPoint(0);
            var p1 = ShowPoint(1);
            var p2 = ShowPoint(2);
            var p3 = ShowPoint(3);

            Handles.color = Color.gray;
            Handles.DrawLine(p0, p1);
            Handles.DrawLine(p2, p3);
		
            ShowDirections();
            Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
        }

        void ShowDirections()
        {
            Handles.color = Color.green;
            Vector3 point = curve.GetPointCubic(0f);
            Handles.DrawLine(point, point + curve.GetDirectionCubic(0f) * directionScale);
            for (int i = 1; i <= lineSteps; i++)
            {
                point = curve.GetPointCubic(i / (float)lineSteps);
                Handles.DrawLine(point, point + curve.GetDirectionCubic(i / (float)lineSteps) * directionScale);
            }
        }

        Vector3 ShowPoint(int index)
        {
            Vector3 point = handleTransform.TransformPoint(curve.points[index]);
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(curve, "Move Point");
                EditorUtility.SetDirty(curve);
                curve.points[index] = handleTransform.InverseTransformPoint(point);
            }

            return point;
        }
    }
}