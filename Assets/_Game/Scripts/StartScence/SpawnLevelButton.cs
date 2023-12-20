using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnLevelButton : MonoBehaviour
{
    [SerializeField] private int numberOfButton;
    [SerializeField] private Button buttonLv;

    private TextLevelButton textLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        SpawnButton();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnButton() 
    {
        for (int i = 0; i <= numberOfButton; i++)
        {
            Instantiate(buttonLv, this.transform);
            textLevelButton.SetData(i.ToString());
        }

    }
}
