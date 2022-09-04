namespace XIV.Spline.Editor
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(Line))]
    public class LineInspector : Editor
    {
        void OnSceneGUI()
        {
            Line line = target as Line;
            // ReSharper disable once PossibleNullReferenceException
            Transform lineTransform = line.transform;
            Vector3 p0 = lineTransform.TransformPoint(line.p0);
            Vector3 p1 = lineTransform.TransformPoint(line.p1);

            var rotation = Tools.pivotRotation == PivotRotation.Local ? lineTransform.localRotation : Quaternion.identity;

            Handles.color = Color.white;

            EditorGUI.BeginChangeCheck();
            p0 = Handles.DoPositionHandle(p0, rotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(line, "Move Point");
                EditorUtility.SetDirty(line);
                line.p0 = lineTransform.InverseTransformPoint(p0);
            }

            EditorGUI.BeginChangeCheck();
            p1 = Handles.DoPositionHandle(p1, rotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(line, "Move Point");
                EditorUtility.SetDirty(line);
                line.p1 = lineTransform.InverseTransformPoint(p1);
            }

            Handles.DrawLine(p0, p1);
        }
    }
}