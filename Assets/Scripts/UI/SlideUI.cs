using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlideUI : Page
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
            page.SetActive(false);
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

    public override void ActivatePage()
    {
        page.SetActive(true);
        if (pauseGame)
            SceneManager.Instance.ChangeGameState(GameState.Pause);
        SceneManager.Instance.ChangeGameState(GameState.Spawning);
    }

    public override void DeactivatePage()
    {
        StartCoroutine(SlideOut());
        SceneManager.Instance.ChangeGameState(GameState.Playing);
    }

    private IEnumerator SlideIn()
    {
        PageManager.Instance.OpenPage(this);
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
        page.SetActive(false);
    }
}
