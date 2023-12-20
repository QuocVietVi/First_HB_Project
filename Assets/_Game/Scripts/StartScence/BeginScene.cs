using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BeginScene : MonoBehaviour
{
    [SerializeField] private Button start;
    void Start()
    {
        start.onClick.AddListener(ChangeScence);

    }
    void Update()
    {
    }

    void ChangeScence()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
