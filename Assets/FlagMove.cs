using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagMove : MonoBehaviour
{
    public GameObject Judge;
    public GameObject Runner;
    public GameObject Flag;
    public Transform start;
    public Transform finish;
    public Transform point1;
    public Transform point2;
    public Transform point3;
    public Transform point4;
    public float Speed;
    public bool Go;

    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        target = point1.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Judge.transform.position == start.position)
            Go = true;

        if (Go)
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * Speed);

        if (transform.position == point1.position)
        {
            target = point2.position;
            Speed = 8f;
        }

        if (transform.position == point2.position)
        {
            target = point3.position;
            Speed = 1f;
        }

        if (Runner.transform.position == finish.position)
        {
            target = point4.position;
            Speed = 6f;
        }

        if (transform.position == point4.position)
        {
            Flag.SetActive(false);
        }
    }
}
