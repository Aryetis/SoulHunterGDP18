using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class FastLaunch : MonoBehaviour
{
    //public static UnityEngine.SceneManagement.Scene sceneBeforeFastLoad;
    //public static string foo;

    //static FastLaunch()
    //{
    //    Debug.Log("POUETS");
    //    EditorApplication.playModeStateChanged += ResetSceneAfterPlayMode;
    //}

    [MenuItem("SoulEaterShortcuts/LaunchSplashScene %#t")]
    static void LaunchSplashScene()
    {
        //sceneBeforeFastLoad = EditorSceneManager.GetActiveScene();
        //foo = EditorSceneManager.GetActiveScene().path;
        EditorSceneManager.OpenScene("Assets/InternalAssets/Scenes/SplashScreenScene.unity");
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    [MenuItem("SoulEaterShortcuts/LaunchSplashScene %#i")]
    static void GetBackToInControlTestScene()
    {
        //sceneBeforeFastLoad = EditorSceneManager.GetActiveScene();
        //foo = EditorSceneManager.GetActiveScene().path; 
        EditorSceneManager.OpenScene("Assets/InternalAssets/Scenes/InControlTestScenes.unity");
    }

    //private static void ResetSceneAfterPlayMode(PlayModeStateChange state)
    //{
    //    Debug.Log("SO AM I");
    //    if (state == PlayModeStateChange.EnteredEditMode)
    //    {
    //        EditorSceneManager.OpenScene(foo);
    //        //EditorApplication.playmodeStateChanged -= ResetSceneAfterPlayMode;
    //    }
    //}
}
