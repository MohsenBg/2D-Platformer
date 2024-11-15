using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class OptionMenu : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    private UIDocument uiDocument;

    private SliderInt sliderMusic;
    private SliderInt sliderSound;
    private Label soundPercent;
    private Label musicPercent;

    private void OnEnable()
    {
        uiDocument = gameObject.GetComponent<UIDocument>();

        var root = uiDocument.rootVisualElement;

        sliderMusic = root.Q<SliderInt>("SliderMusic");
        sliderSound = root.Q<SliderInt>("SliderSound");

        soundPercent = root.Q<Label>("SoundPercent");
        musicPercent = root.Q<Label>("MusicPercent");

        if (sliderMusic != null)
        {
            sliderMusic.RegisterValueChangedCallback(OnMusicSliderChanged);
            sliderMusic.value = GameManager.Instance.audioManager.GetMusicVolume();
            Debug.Log(GameManager.Instance.audioManager.GetMusicVolume());
        }

        if (sliderSound != null)
        {
            sliderSound.RegisterValueChangedCallback(OnSoundSliderChanged);
            sliderSound.value = GameManager.Instance.audioManager.GetSoundVolume();
        }

        AddEventClickToButton("Back", OnClickBackButton);
    }

    private void AddEventClickToButton(string btnName, EventCallback<ClickEvent> clickEvent)
    {
        if (uiDocument == null) return;
        var root = uiDocument.rootVisualElement;
        var button = root.Query<Button>(btnName).First();
        button?.RegisterCallback(clickEvent);
    }

    private void OnClickBackButton(ClickEvent _)
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnMusicSliderChanged(ChangeEvent<int> evt)
    {
        if (musicPercent != null)
            musicPercent.text = $"{evt.newValue}%";

        GameManager.Instance.audioManager.SetMusicVolume(evt.newValue);
    }

    private void OnSoundSliderChanged(ChangeEvent<int> evt)
    {
        if (soundPercent != null)
            soundPercent.text = $"{evt.newValue}%";


        GameManager.Instance.audioManager.SetSoundVolume(evt.newValue);
    }
}
