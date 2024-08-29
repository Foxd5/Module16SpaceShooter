using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class OpenMain
{
    static OpenMain()
    {
        EditorApplication.playModeStateChanged += LoadDefaultScene;
    }

    private static void LoadDefaultScene(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            string scenePath = "Assets/Scenes/MainScene.unity";
            if (!EditorSceneManager.GetActiveScene().path.Equals(scenePath))
            {
                EditorSceneManager.OpenScene(scenePath);
            }
        }
    }
}

