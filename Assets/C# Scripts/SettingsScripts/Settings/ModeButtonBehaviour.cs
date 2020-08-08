using UnityEngine;
using UnityEngine.UI;

public class ModeButtonBehaviour : MonoBehaviour
{
    private Button button;

    public string configPath;
    public GameObject contentRoot;

    // Setup button event.
    void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        // Switch to clicked button.
        foreach (Transform transform in button.transform.parent)
        {
            transform.gameObject.GetComponent<Button>().interactable = true;
        }
        button.interactable = false;

        SettingsHelper.ReloadSettings();

        contentRoot.GetComponent<SettingsContentBehaviour>().FillContent(configPath);
    }
}
