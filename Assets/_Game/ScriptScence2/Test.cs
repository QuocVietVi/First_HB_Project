using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private Transform aPoint;
    [SerializeField] private Transform bPoint;
    [SerializeField] private float delayTime;



    private bool isEndPoint = false;
    private bool canMove = false;
    private float progress = 0f;
    private float deltaTime = 0f;


    private void Start()
    {
        //StartCoroutine(Testing());
    }

    private void Update()
    {
        //horizontal = Input.GetAxisRaw("Horizontal");

        //if (Mathf.Abs(horizontal) > 0.1f) // khi bấm phím
        //{
        //    //rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        //    transform.Translate(speed * Time.deltaTime ,0,0);
        //    //horizontal > 0 -> trả về 0, nếu horizontal <= 0 -> trả về 180
        //    transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        //}
        //else if (isGrounded)
        //{
        //    rb.velocity = Vector2.zero;
        //}


        if(!canMove)
        {
            Moving();
        }
        DoDelayTime();
    }

    private void DoDelayTime()
    {
        if(deltaTime !=0 && deltaTime >= delayTime)
        {
            deltaTime = 0;
            canMove = !canMove;
        }
        deltaTime += Time.deltaTime;
    }

    private void Moving()
    {
        Debug.Log("Moving");
        Vector3 startPoint;
        Vector3 endPoint;
        if(progress >= 1f)
        {
            progress = 0f;
            isEndPoint = !isEndPoint;
        }
        if(isEndPoint)
        {
            startPoint = aPoint.position;
            endPoint = bPoint.position;
        }
        else
        {
            startPoint = bPoint.position;
            endPoint = aPoint.position;
        }
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPoint, endPoint, progress);

        //rb.velocity = transform.right * speed;

    }

    //private IEnumerator Testing()
    //{
    //    for (float i = 1f; i >= 0; i -= 0.1f)
    //    {
    //        StopMoving();
    //        yield return new WaitForSeconds(1f);
    //        Moving();
    //        yield return new WaitForSeconds(1f);
    //    }
    //}
   
    
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "DeathZone")
    //    {
    //        transform.position = aPoint.transform.position;
    //    }
    //    if (collision.tag == "SavePoint")
    //    {
    //    }

    //}

}
