using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLauncher : MonoBehaviour
{
    public GameObject canvas;
    public GameObject windowPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject window = Instantiate(windowPrefab);
        window.transform.SetParent(canvas.transform);
        window.transform.localPosition = new Vector3(0, 0, 0);
        window.transform.localScale = new Vector3(1, 1, 1);
    }
}
