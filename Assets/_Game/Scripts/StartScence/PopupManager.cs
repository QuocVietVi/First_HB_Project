using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject popup;
    [SerializeField] private Button show, close;
    private void Start()
    {
        show.onClick.AddListener(ShowPopup);
        close.onClick.AddListener(ClosePopUp);
    }
    public void ShowPopup()
    {
        popup.SetActive(true);
    }
    public void ClosePopUp()
    {
        popup.SetActive(false);
    }
}
