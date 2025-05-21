using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    [Header("Panel References")]
    [SerializeField] private RectTransform _panelMenu;
    [SerializeField] private RectTransform _panelSetting;
    [SerializeField] private RectTransform _panelTutorial;
    [SerializeField] private RectTransform _panelHighScore;

    [Header("Blur Overlay")]
    [SerializeField] private CanvasGroup _blurOverlay;

    [Header("Transition Settings")]
    [SerializeField] private float _transitionTime = 0.5f;
    [SerializeField] private Vector2 _offscreenLeft = new Vector2(-1920f, 0);
    [SerializeField] private Vector2 _offscreenRight = new Vector2(1920f, 0);
    [SerializeField] private Vector2 _center = Vector2.zero;

    private RectTransform _currentPanel;

    private void Start()
    {
        _currentPanel = _panelMenu;
        Time.timeScale = 1;
    }

    public void SwitchTo(RectTransform targetPanel)
    {
        if (_blurOverlay != null)
        {
            _blurOverlay.gameObject.SetActive(true);
            _blurOverlay.alpha = 0;
            _blurOverlay.DOFade(1f, 0.3f);
        }

        RectTransform from = _panelMenu;
        from.DOAnchorPos(_offscreenLeft, _transitionTime).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            from.gameObject.SetActive(false);
        });


        RectTransform to = targetPanel;
        to.gameObject.SetActive(true);
        to.anchoredPosition = _offscreenRight;
        to.DOAnchorPos(_center, _transitionTime).SetEase(Ease.InOutCubic);

        _currentPanel = targetPanel;
    }

    public void BackToMenu()
    {
        if (_blurOverlay != null)
        {
            _blurOverlay.DOFade(0f, 0.3f).OnComplete(() => _blurOverlay.gameObject.SetActive(false));
        }

        _currentPanel.DOAnchorPos(_offscreenRight, _transitionTime).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            _currentPanel.gameObject.SetActive(false);
        });

        _panelMenu.gameObject.SetActive(true);
        _panelMenu.anchoredPosition = _offscreenLeft;
        _panelMenu.DOAnchorPos(_center, _transitionTime).SetEase(Ease.InOutCubic);

    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
