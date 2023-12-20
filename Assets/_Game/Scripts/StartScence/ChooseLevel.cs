using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    [SerializeField] private Button close,start;
    [SerializeField] public GameObject mainPanel, chooseLvPanel;

    void Start()
    {
        close.onClick.AddListener(delegate { ClosePanel(chooseLvPanel); });
        start.onClick.AddListener(delegate { ShowPanel(chooseLvPanel); });
    }

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
