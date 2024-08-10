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
        Debug.Log("Start Position: " + startPosition);
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

        Bounds selectionBounds = new Bounds(selectionBox.position, selectionBox.sizeDelta);

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
