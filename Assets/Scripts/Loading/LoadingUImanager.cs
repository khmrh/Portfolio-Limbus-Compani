using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUIManager : MonoBehaviour
{
    private static LoadingUIManager _instance;

    [Header("UI Groups")]
    [SerializeField] private CanvasGroup _mainCanvas;
    [SerializeField] private CanvasGroup _loadingCanvas;

    [Header("Progress Bar")]
    [SerializeField] private Image _progressBar;

    private float _targetDuration = 10.0f;
    private bool _isProcessing = false;

    public static LoadingUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = FindObjectOfType<LoadingUIManager>();
                if (obj != null) _instance = obj;
                else _instance = Create();
            }
            return _instance;
        }
    }

    private static LoadingUIManager Create()
    {
        return Instantiate(Resources.Load<LoadingUIManager>("LoadingUI"));
    }

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void OnClickToStart()
    {
        if (_isProcessing) return;
        StartCoroutine(StartLoadingSequence());
    }

    private IEnumerator StartLoadingSequence()
    {
        _isProcessing = true;
        if (_mainCanvas != null)
        {
            float fTimer = 0f;
            while (fTimer <= 1.0f)
            {
                fTimer += Time.unscaledDeltaTime * 4.0f;
                _mainCanvas.alpha = Mathf.Lerp(1.0f, 0.0f, fTimer);
                yield return null;
            }
            _mainCanvas.gameObject.SetActive(false);
        }

        if (_loadingCanvas != null)
        {
            _loadingCanvas.gameObject.SetActive(true);
            _loadingCanvas.alpha = 1.0f;
        }

        yield return StartCoroutine(LoadSceneProgress());
    }

    private IEnumerator LoadSceneProgress()
    {
        _progressBar.fillAmount = 0.0f;
        float timer = 0.0f;

        while (timer < _targetDuration)
        {
            timer += Time.unscaledDeltaTime;
            float timeRatio = timer / _targetDuration;
            _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, timeRatio, Time.unscaledDeltaTime * 5.0f);

            yield return null;
        }

        _progressBar.fillAmount = 1.0f;
        yield return new WaitForSecondsRealtime(0.5f);

        _isProcessing = false;
    }
}