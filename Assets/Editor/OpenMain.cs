using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class OpenMain
{
    static OpenMain()
    {
        EditorApplication.update += LoadDefaultScene;
    }

    private static void LoadDefaultScene()
    {
        // check if unity in play mode, return if it is
        if (Application.isPlaying) return;

        // make sure only runs once
        EditorApplication.update -= LoadDefaultScene;

        string scenePath = "Assets/Scenes/MainScene.unity";

        // open scene in the editor (only in edit mode)
        if (!EditorSceneManager.GetActiveScene().path.Equals(scenePath))
        {
            EditorSceneManager.OpenScene(scenePath);
        }

        // switch to 2D mode
        if (SceneView.lastActiveSceneView != null)
        {
            SceneView.lastActiveSceneView.in2DMode = true;
        }
    }
}
