using CameraTrajector.Client;
using UnityEditor;
using UnityEngine;

public class TrajectoryEditorMenu : EditorWindow
{
    private static MovementTrajectoryData _trajectoryData;

    private TrajectoryModel _lastSelected;

    [MenuItem("CustomEditor/Trajectory Menu")]
    public static void Init()
    {
        GetWindow(typeof(TrajectoryEditorMenu));

        _trajectoryData = JsonUtility.FromJson<MovementTrajectoryData>(PlayerPrefs.GetString(Paths.RecordingsDataPrefs));
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }
    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    public void OnGUI()
    {
        if (_trajectoryData == null || _trajectoryData.TrajectoryModels.Count == 0)
        {
            GUILayout.Label("No any trajectories in PlayerPrefs. Please create some", EditorStyles.boldLabel);
        }

        foreach (TrajectoryModel model in _trajectoryData.TrajectoryModels)
        {
            if (GUILayout.Button(model.Id))
            {
                _lastSelected = model;
            }
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        if (_lastSelected != null)
        {
            DrawGizmoTrajectory();
        }

        SceneView.RepaintAll();
    }

    private void DrawGizmoTrajectory()
    {
        Vector3[] points = new Vector3[_lastSelected.Locations.Count];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = _lastSelected.GetLocation(i);
        }

        Handles.color = Color.green;
        Handles.DrawPolyLine(points);
    }
}