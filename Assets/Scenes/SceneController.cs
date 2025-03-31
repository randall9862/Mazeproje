using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneController : MonoBehaviour//��������
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
        // �Ы�Canvas
        CreateCanvas();

        // �ЫزH�J�H�X�Ϊ��¦�Ϥ�
        CreateFadeImage();
    }

    void Start()
    {
        // ��l�Ƭ��H�J�ĪG
        fadeImage.color = new Color(0, 0, 0, 1);
        fadeAlpha = 1f;
        isFading = false;
    }

    void Update()
    {
        // �I����������
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
        canvas.sortingOrder = 999; // �T�O�b�̤W�h

        // �K�[CanvasScaler�ե�
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        canvasObj.AddComponent<GraphicRaycaster>();
    }

    private void CreateFadeImage()
    {
        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(canvas.transform, false);

        // �K�[Image�ե�
        fadeImage = imageObj.AddComponent<Image>();
        fadeImage.color = new Color(0, 0, 0, 0);

        // �]�mRectTransform�H�л\��ӫ̹�
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
                // �H�X
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
            // �H�J
            fadeAlpha -= Time.deltaTime * fadeSpeed;
            fadeAlpha = Mathf.Max(0f, fadeAlpha);
            fadeImage.color = new Color(0, 0, 0, fadeAlpha);
        }
    }

    private void HandleTextBlinking()
    {
        if (blinkingText != null)
        {
            // �ϥΥ����i��{���ƪ��{�{�ĪG
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