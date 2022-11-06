namespace XIV.Spline.Editor
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(BezierSpline))]
    public class BezierSplineInspector : Editor
    {
        const float directionScale = 0.5f;
        const int stepsPerCurve = 10;

        const float handleSize = 0.06f;
        const float pickSize = 0.1f;

        int selectedIndex = -1;

        BezierSpline bezierSpline;
        Transform bezierSplineTransform;
        Quaternion handleRotation;

        void OnEnable()
        {
            bezierSpline = target as BezierSpline;
            bezierSplineTransform = bezierSpline.transform;
            bezierSplineTransform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;

            if (bezierSplineTransform.position != Vector3.zero)
            {
                Undo.RecordObject(bezierSplineTransform, "Initialize Spline");
                bezierSplineTransform.position = Vector3.zero;
                EditorUtility.SetDirty(bezierSplineTransform);
            }

            Tools.hidden = true;
        }

        void OnDisable()
        {
            if (bezierSplineTransform != null)
            {
                bezierSplineTransform.hideFlags = HideFlags.None;
                Tools.hidden = false;
            }
        }

        public override void OnInspectorGUI()
        {
            if (selectedIndex >= 0 && selectedIndex < bezierSpline.ControlPointCount)
            {
                DrawSelectedPointInspector();
            }

            if (GUILayout.Button("Add Curve"))
            {
                Undo.RecordObject(bezierSpline, "Add Curve");
                bezierSpline.AddCurve();
                EditorUtility.SetDirty(bezierSpline);
            }

            if (GUILayout.Button("Remove Curve"))
            {
                Undo.RecordObject(bezierSpline, "Remove Curve");
                bezierSpline.RemoveCurve(selectedIndex);
                EditorUtility.SetDirty(bezierSpline);
            }
        }


        void DrawSelectedPointInspector()
        {
            GUILayout.Label("Selected Point : " + selectedIndex);
            EditorGUI.BeginChangeCheck();
            Vector3 point = EditorGUILayout.Vector3Field("Position", bezierSpline.GetPoint(selectedIndex));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(bezierSpline, "Move Point");
                EditorUtility.SetDirty(bezierSpline);
                bezierSpline.SetPoint(selectedIndex, point);
            }
        }

        void OnSceneGUI()
        {
            handleRotation = Tools.pivotRotation == PivotRotation.Local ? bezierSplineTransform.rotation : Quaternion.identity;

            Vector3 p0 = ShowPoint(0, false);
            for (int i = 1; i < bezierSpline.ControlPointCount; i += 3)
            {
                Vector3 p1 = ShowPoint(i, true);
                Vector3 p2 = ShowPoint(i + 1, true);
                Vector3 p3 = ShowPoint(i + 2, false);

                Handles.color = Color.gray;
                Handles.DrawLine(p0, p1);
                Handles.DrawLine(p2, p3);

                Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);
                p0 = p3;
            }

            ShowDirections();
        }

        void ShowDirections()
        {
            Handles.color = Color.green;
            Vector3 point = bezierSpline.GetPointCubic(0f);
            Handles.DrawLine(point, point + bezierSpline.GetDirection(0f) * directionScale);
            int steps = stepsPerCurve * bezierSpline.CurveCount;
            for (int i = 1; i <= steps; i++)
            {
                float t = i / (float)steps;
                point = bezierSpline.GetPointCubic(t);
                Handles.DrawLine(point, point + bezierSpline.GetDirection(t) * directionScale);
            }
        }

        Vector3 ShowPoint(int index, bool isAnchor)
        {
            Vector3 point = bezierSplineTransform.TransformPoint(bezierSpline.GetPoint(index));

            float size = HandleUtility.GetHandleSize(point);
            Handles.color = isAnchor ? Color.blue : Color.white;
            if (Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.DotHandleCap))
            {
                selectedIndex = index;
                Repaint();
            }

            if (selectedIndex == index)
            {
                if (isAnchor == false)
                {
                    MoveWithAnchors(index);
                    return bezierSpline.GetPoint(index);
                }
                EditorGUI.BeginChangeCheck();
                point = Handles.DoPositionHandle(point, handleRotation);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(bezierSpline, "Move Point");
                    EditorUtility.SetDirty(bezierSpline);
                    bezierSpline.SetPoint(index, bezierSplineTransform.InverseTransformPoint(point));
                }
            }

            return point;
        }


        void MoveWithAnchors(int controlIndex)
        {
            int previousIndex = controlIndex - 1;
            int nextIndex = controlIndex + 1;
            bool isPreviousAvailable = previousIndex > 0;
            bool isNextAvailable = nextIndex < bezierSpline.ControlPointCount;
            
            Vector3 controlPoint = bezierSplineTransform.TransformPoint(bezierSpline.GetPoint(controlIndex));

            Vector3 diffPrevious = Vector3.zero;
            if (isPreviousAvailable)
            {
                Vector3 previousAnchor = bezierSplineTransform.TransformPoint(bezierSpline.GetPoint(previousIndex));
                diffPrevious = previousAnchor - controlPoint;
            }

            Vector3 diffNext = Vector3.zero;
            if (isNextAvailable)
            {
                Vector3 nextAnchor = bezierSplineTransform.TransformPoint(bezierSpline.GetPoint(nextIndex));
                diffNext = nextAnchor - controlPoint;
            }

            EditorGUI.BeginChangeCheck();
            controlPoint = Handles.DoPositionHandle(controlPoint, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(bezierSpline, "Move Point");
                EditorUtility.SetDirty(bezierSpline);

                if (isPreviousAvailable)
                {
                    var previousAnchorPos = controlPoint + diffPrevious;
                    bezierSpline.SetPoint(previousIndex, bezierSplineTransform.InverseTransformPoint(previousAnchorPos));
                }

                if (isNextAvailable)
                {
                    var nextAnchorPos = controlPoint + diffNext;
                    bezierSpline.SetPoint(nextIndex, bezierSplineTransform.InverseTransformPoint(nextAnchorPos));
                }
                
                bezierSpline.SetPoint(controlIndex, bezierSplineTransform.InverseTransformPoint(controlPoint));
            }
        }
    }
}