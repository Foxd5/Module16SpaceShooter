using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
//Created this because I want the main scene to open automatically for my grader.
public class OpenMain
{
    static OpenMain()
    {
        EditorApplication.update += LoadDefaultScene;
    }

    private static void LoadDefaultScene()
    {

        string scenePath = "Assets/Scenes/MainScene.unity";

        EditorSceneManager.OpenScene(scenePath);

        EditorApplication.update -= LoadDefaultScene;
    }
}
