using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private UIDocument uiDocument;
    private VisualElement root;

    void Start()
    {
        SetupMenu();
    }

    private void SetupMenu()
    {
        if (uiDocument == null) return;

        root = uiDocument.rootVisualElement;
        RegisterButton("Continue", OnClickContinue);
        RegisterButton("Restart", OnClickRestart);
        RegisterButton("Exit", OnClickExit);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menu != null)
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
        SetupMenu(); // Re-register buttons in case menu was toggled
        Time.timeScale = menu.activeSelf ? 0 : 1;
    }

    public void OnClickContinue(ClickEvent _)
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnClickRestart(ClickEvent _)
    {
        Time.timeScale = 1;
        SceneController.LoadCurrentScene();
    }

    public void OnClickExit(ClickEvent _)
    {
        Time.timeScale = 1;
        SceneController.LoadFirstScene();
    }

    private void RegisterButton(string buttonName, EventCallback<ClickEvent> clickEvent)
    {
        var button = root?.Query<Button>(buttonName).First();
        if (button != null)
        {
            button.RegisterCallback(clickEvent);
        }
    }
}
