using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.ResourcesPathes.UI;
using View.Root;

namespace EntryPoint.Game
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;

        private readonly UIRootView _uiRoot;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutoRun()
        {
            _instance = new GameEntryPoint();
            _instance.RunGame().Forget();
        }

        private GameEntryPoint()
        {
            var prefabUIRoot = Resources.Load<UIRootView>(UIResourcesPathProvider.ROOT);
            _uiRoot = Object.Instantiate(prefabUIRoot);
            Object.DontDestroyOnLoad(_uiRoot.gameObject);
        }
        
        private async UniTaskVoid RunGame()
        {
#if UNITY_EDITOR
            var sceneName = SceneManager.GetActiveScene().name;
            
            if (sceneName == Utils.Scenes.Scenes.MAIN_MENU)
            {
                await LoadMainMenu();
                return;
            }
            
            if (sceneName == Utils.Scenes.Scenes.GAMEPLAY)
            {
                await LoadGameplay();
                return;
            }

            if (sceneName != Utils.Scenes.Scenes.BOOT)
                return;
#endif
            await LoadMainMenu();
        }
        
        
        private async UniTask LoadMainMenu()
        {
            _uiRoot.ShowLoadingScreen();
            
            await SceneManager.LoadSceneAsync(Utils.Scenes.Scenes.BOOT);
            await SceneManager.LoadSceneAsync(Utils.Scenes.Scenes.MAIN_MENU);

            var root = Object.FindFirstObjectByType<UIMainMenuRootView>();
            root.PlayButtonPressed += async () => await LoadGameplay();
                
            _uiRoot.HideLoadingScreen();
        }
        
        private async UniTask LoadGameplay()
        {
            _uiRoot.ShowLoadingScreen();
            
            await SceneManager.LoadSceneAsync(Utils.Scenes.Scenes.BOOT);
            await SceneManager.LoadSceneAsync(Utils.Scenes.Scenes.GAMEPLAY);
            
             var root = Object.FindFirstObjectByType<UIGameplayRootView>();
             root.HomeButtonPressed += async () => await LoadMainMenu();
            
            _uiRoot.HideLoadingScreen();
        }
    }
}