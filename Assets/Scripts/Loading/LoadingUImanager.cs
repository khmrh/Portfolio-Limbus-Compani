using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingController : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject loadingCanvas;

    [Header("Loading Bar")]
    [SerializeField] private Image fillImage;

    [Header("Settings")]
    [SerializeField] private string sceneName;
    [Range(1f, 10f)]
    [SerializeField] private float minimumTime = 3.0f; // 최소 로딩 시간 (초 단위)

    void Start()
    {
        if (loadingCanvas != null)
            loadingCanvas.SetActive(false);
    }

    public void OnClickStartLoading()
    {
        loadingCanvas.SetActive(true);
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        fillImage.fillAmount = 0;

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        float timer = 0f;

        // 실제 로딩과 상관없이 minimumTime 동안 루프를 돕니다.
        while (timer < 1.0f)
        {
            yield return null;

            // 시간에 따른 로딩바 진행 (0에서 1까지 minimumTime에 걸쳐 증가)
            timer += Time.unscaledDeltaTime / minimumTime;

            // 실제 로딩 진행률과 비교하여 더 낮은 값을 표시 (혹은 그냥 timer만 표시)
            // 보통 '연출'이 목적이면 timer 값만 사용해도 무방합니다.
            fillImage.fillAmount = timer;

            // 로딩바가 다 찼고, 실제 씬 로딩도 90% 이상(완료단계) 되었을 때
            if (timer >= 1.0f && op.progress >= 0.9f)
            {
                op.allowSceneActivation = true;
                yield break;
            }
        }
    }
}