using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLevelButton : MonoBehaviour
{
    [SerializeField] private Text buttonText;
    [SerializeField] private Button button;

    public Text ButtonText { get => buttonText; set => buttonText = value; }

    public void SetData(string id)
    {
        buttonText.text = id;
    }
}
