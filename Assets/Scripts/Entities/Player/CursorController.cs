using UnityEngine;
using System.Collections.Generic;

public class CursorController : MonoBehaviour
{
    public RectTransform selectionBox; // Reference to the selection box as a 2D sprite in world space
    private Vector2 startPosition; // Fixed starting point of the selection box
    private Vector2 endPosition; // Dynamic endpoint based on mouse movement
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
        startPosition = GetWorldPosition(input);

        selectionBox.gameObject.SetActive(true);
        selectionBox.localScale = Vector3.zero; // Reset the size
        selectionBox.position = startPosition;  // Set initial position
    }

    private void HandleDrag(Vector2 input)
    {
        if (!isSelecting) return;

        endPosition = GetWorldPosition(input);
        UpdateSelectionBox();
    }

    private void HandleRelease(Vector2 input)
    {
        if (!isSelecting) return;

        SelectHeroes();
        isSelecting = false;

        selectionBox.gameObject.SetActive(false);
    }

    private void UpdateSelectionBox()
    {
        if (selectionBox == null) return;
        float widht = endPosition.x - startPosition.x;
        float height = endPosition.y - startPosition.y;
        
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(widht), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPosition + new Vector2(widht / 2, height / 2);

    }

    private void SelectHeroes()
    {
        selectedHeroes.Clear();

        // Define the bounds of the selection box
        Bounds selectionBounds = new Bounds(selectionBox.position, selectionBox.localScale);

        foreach (var hero in FindObjectsOfType<Hero>())
        {
            if (selectionBounds.Contains(hero.transform.position))
            {
                hero.isSelected = true;
                selectedHeroes.Add(hero);
            }
            else
            {
                hero.isSelected = false;
            }
        }
    }

    private Vector2 GetWorldPosition(Vector2 screenPosition)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        return new Vector2(worldPosition.x, worldPosition.y);
    }

    private void OnDestroy()
    {
        InputManager.OnClick -= HandleClick;
        InputManager.OnClickHold -= HandleDrag;
        InputManager.OnRelease -= HandleRelease;
    }
}
