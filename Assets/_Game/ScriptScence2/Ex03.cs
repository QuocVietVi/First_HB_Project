using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ex03 : MonoBehaviour
{
    [SerializeField] private Transform startPoint,aPoint, bPoint, endPoint;
    [SerializeField] private float speed;

    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPoint.position;
        target = aPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector2.Distance(transform.position, startPoint.position) < 0.1f)
        {
            target = aPoint.position;
        }
        else if (Vector2.Distance(transform.position, aPoint.position) < 0.1f)
        {
            target = bPoint.position;
        }
        else if (Vector2.Distance(transform.position, bPoint.position) < 0.1f)
        {
            target = endPoint.position;
        }

        else if (Vector2.Distance(transform.position, endPoint.position) < 0.1f)
        {
            transform.position = startPoint.position;
        }
        
    }
}
