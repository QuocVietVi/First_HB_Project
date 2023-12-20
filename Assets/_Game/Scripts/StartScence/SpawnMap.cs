using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnMap : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject map, player,mainPanel;
    void Start()
    {
        button.onClick.AddListener(MapSpawning);
    }
    public void MapSpawning() 
    {
        Instantiate(map);
        Invoke(nameof(SpawnPlayer), 0.5f);
        //if (mainPanel.activeInHierarchy)
        //{
        //}
        //else
        //{
        //    map.SetActive(true);
        //}
    }
    public void SpawnPlayer()
    {
        player.SetActive(true);
    }



}
