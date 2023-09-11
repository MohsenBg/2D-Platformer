using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject menu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menu != null)
        {
            menu.SetActive(!menu.activeSelf);
            Time.timeScale = menu.activeSelf ? 0 : 1;
        }
    }

    public void OnClickResume()
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnClickQuite()
    {
        Time.timeScale = 1;
        SceneController.LoadFirstScene();
    }
}
