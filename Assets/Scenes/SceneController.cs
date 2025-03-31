using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneController : MonoBehaviour//介面切換
{
    [SerializeField]
    private TextMeshProUGUI blinkingText;

    [Header("Fade Settings")]
    [SerializeField]
    private float fadeSpeed = 1.5f;

    [Header("Text Blink Settings")]
    [SerializeField]
    private float blinkSpeed = 2f;
    [SerializeField]
    private float minAlpha = 0.2f;
    [SerializeField]
    private float maxAlpha = 1f;

    private Image fadeImage;
    private bool isFading = false;
    private float fadeAlpha = 0f;
    private float textTimer = 0f;
    private Canvas canvas;

    void Awake()
    {
        // 創建Canvas
        CreateCanvas();

        // 創建淡入淡出用的黑色圖片
        CreateFadeImage();
    }

    void Start()
    {
        // 初始化為淡入效果
        fadeImage.color = new Color(0, 0, 0, 1);
        fadeAlpha = 1f;
        isFading = false;
    }

    void Update()
    {
        // 點擊切換場景
        if (Input.GetMouseButtonDown(0) && !isFading)
        {
            StartFadeOut();
        }

        HandleFading();
        HandleTextBlinking();
    }

    private void CreateCanvas()
    {
        GameObject canvasObj = new GameObject("TransitionCanvas");
        canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999; // 確保在最上層

        // 添加CanvasScaler組件
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        canvasObj.AddComponent<GraphicRaycaster>();
    }

    private void CreateFadeImage()
    {
        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(canvas.transform, false);

        // 添加Image組件
        fadeImage = imageObj.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0);

        // 設置RectTransform以覆蓋整個屏幕
        RectTransform rect = imageObj.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;
        rect.anchoredPosition = Vector2.zero;
    }

    private void HandleFading()
    {
        if (isFading)
        {
            if (fadeAlpha < 1f)
            {
                // 淡出
                fadeAlpha += Time.deltaTime * fadeSpeed;
                fadeImage.color = new Color(0, 0, 0, fadeAlpha);

                if (fadeAlpha >= 1f)
                {
                    LoadNextScene();
                }
            }
        }
        else if (fadeAlpha > 0f)
        {
            // 淡入
            fadeAlpha -= Time.deltaTime * fadeSpeed;
            fadeAlpha = Mathf.Max(0f, fadeAlpha);
            fadeImage.color = new Color(0, 0, 0, fadeAlpha);
        }
    }

    private void HandleTextBlinking()
    {
        if (blinkingText != null)
        {
            // 使用正弦波實現平滑的閃爍效果
            textTimer += Time.deltaTime * blinkSpeed;
            float alpha = Mathf.Lerp(minAlpha, maxAlpha,
                (Mathf.Sin(textTimer) + 1f) * 0.5f);

            Color textColor = blinkingText.color;
            textColor.a = alpha;
            blinkingText.color = textColor;
        }
    }

    private void StartFadeOut()
    {
        isFading = true;
        fadeAlpha = 0f;
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
}