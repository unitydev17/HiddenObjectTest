using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

#endif

namespace Code.Editor
{
    public class MenuScript : MonoBehaviour
    {
        private const string SplashScene = "Assets/_Project/Scenes/Splash.unity";
        private const string MenuScene = "Assets/_Project/Scenes/Menu.unity";
        private const string GamePlayScene = "Assets/_Project/Scenes/GamePlay.unity";

#if UNITY_EDITOR
        [MenuItem("Scenes/Splash - RUN #_1")]
        public static void SplashSceneRun()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                return;
            }

            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(SplashScene);
            EditorApplication.isPlaying = true;
        }

        [MenuItem("Scenes/Menu - Edit #2")]
        public static void MenuSceneEdit()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                return;
            }

            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(MenuScene);
        }

        [MenuItem("Scenes/GamePlay - Edit #3")]
        public static void GamePlaySceneEdit()
        {
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                return;
            }

            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(GamePlayScene);
        }

#endif
    }
}