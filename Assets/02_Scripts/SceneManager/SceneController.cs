using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DCS.SceneController
{
    public class SceneController : MonoBehaviour
    {
        #region Singleton
        private static SceneController _instance;
        public static SceneController Instance => _instance;

        protected virtual void Awake()
        {
            SceneController singleton = this;
            if (_instance != null)
            {
                Debug.LogError("A instance already exists");
                Destroy(singleton); //Or GameObject as appropriate
                return;
            }
            _instance = (SceneController)FindObjectOfType(typeof(SceneController));
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        public UnityEvent OnStartSceneLoad = new UnityEvent();
        public UnityEvent OnFinishSceneLoad = new UnityEvent();
        public UnityEvent<float> OnSceneLoading = new UnityEvent<float>();
        public bool IsLoading { get; private set; }
        public float LoadProgress { get; private set; }
        private float delay;
        public void RequestLoadScene(string sceneName, float delay = 0)
        {
            if (!IsLoading)
            {
                OnStartSceneLoad.Invoke();
                this.delay = delay;
                StartCoroutine(LoadScene(sceneName));
            }
        }

        public void RequestReloadCurrentScene(float delay = 0)
        {
            if (!IsLoading)
            {
                OnStartSceneLoad.Invoke();
                this.delay = delay;
                StartCoroutine(LoadScene(SceneManager.GetActiveScene().name));
            }
        }

        public void RequestLoadScene(int sceneIndex, float delay = 0)
        {
            if (!IsLoading)
            {
                OnStartSceneLoad.Invoke();
                this.delay = delay;
                StartCoroutine(LoadScene(sceneIndex));
            }
        }

        void FinishedLoadingScene()
        {
            OnFinishSceneLoad.Invoke();
        }
        IEnumerator LoadScene(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            IsLoading = true;
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            IsLoading = false;
        }
        IEnumerator LoadScene(int sceneIndex)
        {
            IsLoading = true;

            if (delay > 0)
                yield return new WaitForSecondsRealtime(delay);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);

            while (!asyncLoad.isDone)
            {
                OnSceneLoading.Invoke(asyncLoad.progress);
                yield return null;
            }

            IsLoading = false;
            FinishedLoadingScene();
        }

    }
}

