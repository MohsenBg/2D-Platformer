using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;

    [SerializeField] private GameObject levelMenu;
    [SerializeField] private GameObject optionMenu;

    private void OnEnable()
    {
        AddEventClickToButton("Play", OnClickPlayButton);
        AddEventClickToButton("Exit", OnClickQuitButton);
        AddEventClickToButton("Option", OnClickOptionButton);
    }

    private void OnClickOptionButton(ClickEvent evt)
    {
        optionMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    private void AddEventClickToButton(string btnName, EventCallback<ClickEvent> clickEvent)
    {
        if (uiDocument == null) return;

        var root = uiDocument.rootVisualElement;
        var button = root.Query<Button>(btnName).First();
        button?.RegisterCallback(clickEvent);
    }

    private void OnClickPlayButton(ClickEvent _)
    {
        levelMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnClickQuitButton(ClickEvent _)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Exit Game");
#else
        Application.Quit();
#endif
    }
}
