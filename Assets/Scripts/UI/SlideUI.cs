using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlideUI : Screen
{
    [Header("Slide Menu Elements")]
    public RectTransform menuPanel;
    public Button toggleButton;
    public float slideDuration = 0.5f;
    public bool startOpened = false;

    private Vector2 offScreenPosition;
    private Vector2 onScreenPosition;
    private bool isOpen = false;

    void Awake()
    {
        pauseGame = false;
        openOnTop = true;

        onScreenPosition = menuPanel.anchoredPosition;
        offScreenPosition = new Vector2(menuPanel.anchoredPosition.x, -0.5f * menuPanel.rect.height);

        toggleButton.onClick.AddListener(ToggleMenu);
    }

    void Start()
    {
        if (!startOpened)
        {
            menuPanel.anchoredPosition = offScreenPosition;
            screen.SetActive(false);
        }
    }

    public void ToggleMenu()
    {
        if (isOpen)
        {
            StartCoroutine(SlideOut());
        }
        else
        {
            StartCoroutine(SlideIn());
        }
    }

    public override void DeactivateScreen()
    {
        StartCoroutine(SlideOut());
    }

    private IEnumerator SlideIn()
    {
        ScreenManager.Instance.OpenScreen(this);
        float elapsedTime = 0;
        Vector2 startingPosition = menuPanel.anchoredPosition;

        while (elapsedTime < slideDuration)
        {
            menuPanel.anchoredPosition = Vector2.Lerp(startingPosition, onScreenPosition, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        menuPanel.anchoredPosition = onScreenPosition;
        isOpen = true;
    }

    private IEnumerator SlideOut()
    {

        float elapsedTime = 0;
        Vector2 startingPosition = menuPanel.anchoredPosition;

        while (elapsedTime < slideDuration)
        {
            menuPanel.anchoredPosition = Vector2.Lerp(startingPosition, offScreenPosition, elapsedTime / slideDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        menuPanel.anchoredPosition = offScreenPosition;
        isOpen = false;
        screen.SetActive(false);
    }
}
