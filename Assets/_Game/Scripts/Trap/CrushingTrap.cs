using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushingTrap : MonoBehaviour
{
    [SerializeField] private float downSpeed;
    [SerializeField] private float upSpeed;
    [SerializeField] private Transform aPoint;
    [SerializeField] private Transform bPoint;
    private bool stampDown;

    void Update()
    {
        if (transform.position.y >= aPoint.position.y)
        {
            stampDown = true;
        }
        if (transform.position.y < bPoint.position.y)
        {
            stampDown = false;
        }
        if (stampDown= true)
        {
            transform.position = Vector2.MoveTowards(transform.position, aPoint.position, downSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, bPoint.position, upSpeed * Time.deltaTime);
        }
    }

    
}
