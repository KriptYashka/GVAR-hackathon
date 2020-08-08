using UnityEngine;

public class CloseButton : MonoBehaviour
{
    public GameObject window;

    public void OnClick()
    {
        window.SetActive(false);
    }
}
