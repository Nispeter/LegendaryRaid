using UnityEngine;
using System.Collections.Generic;

public class CursorController : MonoBehaviour
{
    public RectTransform selectionBox;
    public HeroSpawner heroSpawner;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool isSelecting = false;
    private List<Hero> selectedHeroes = new List<Hero>();

    void Start()
    {
        InputManager.OnClick += HandleClick;
        InputManager.OnClickHold += HandleDrag;
        InputManager.OnRelease += HandleRelease;
    }

    private void HandleClick(Vector2 input)
    {
        isSelecting = true;
        startPosition = input;
        selectionBox.gameObject.SetActive(true);
    }

    private void HandleDrag(Vector2 input)
    {
        if (!isSelecting) return;

        endPosition = input;
        UpdateSelectionBox();
    }

    private void HandleRelease(Vector2 input)
    {
        if (!isSelecting) return;

        SelectHeroes();
        isSelecting = false;
        selectionBox.sizeDelta = Vector2.zero;
        selectionBox.anchoredPosition = startPosition;
        selectionBox.gameObject.SetActive(false);
    }

    private void UpdateSelectionBox()
    {
        if (selectionBox == null) return;

        float width = endPosition.x - startPosition.x;
        float height = endPosition.y - startPosition.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPosition + new Vector2(width / 2, height / 2);

    }

    private void SelectHeroes()
    {
        selectedHeroes.Clear();

        // Convert the corners of the selection box from screen space to world space
        Vector2 minScreenPosition = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 maxScreenPosition = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

        Vector3 minWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(minScreenPosition.x, minScreenPosition.y, Camera.main.nearClipPlane));
        Vector3 maxWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(maxScreenPosition.x, maxScreenPosition.y, Camera.main.nearClipPlane));

        // Adjust z position to be at the same depth as the heroes (assuming z=0)
        minWorldPosition.z = 0f;
        maxWorldPosition.z = 0f;

        // Create the bounds in world space
        Bounds selectionBounds = new Bounds();
        selectionBounds.SetMinMax(minWorldPosition, maxWorldPosition);

        // Debug log for min and max positions
        Debug.Log($"Min World Position: {minWorldPosition}, Max World Position: {maxWorldPosition}");

        foreach (var hero in heroSpawner.heroes)
        {
            // Use the hero's world position to check if it's within the selection bounds
            Vector3 heroPosition = hero.transform.position;
            if (selectionBounds.Contains(heroPosition))
            {
                hero.Select();
                selectedHeroes.Add(hero);
                Debug.Log($"Hero {hero.name} selected at position {heroPosition}");
            }
            else
            {
                hero.DeSelect();
                Debug.Log($"Hero {hero.name} deselected at position {heroPosition}");
            }
        }
    }

    private void OnDestroy()
    {
        InputManager.OnClick -= HandleClick;
        InputManager.OnClickHold -= HandleDrag;
        InputManager.OnRelease -= HandleRelease;
    }
}
