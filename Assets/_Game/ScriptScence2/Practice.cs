using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Practice : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private Transform aPoint;
    [SerializeField] private Transform bPoint;
    Vector3 target;


    private void Start()
    {
        target = bPoint.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position,bPoint.position) < 0.1f)
        {
            transform.position = aPoint.position;
        }

    }
}
