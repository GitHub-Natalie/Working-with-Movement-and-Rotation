using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWayRunners: MonoBehaviour
{
    public GameObject Judge; // �����
    public GameObject FlagOrRunner; // ���� ��� �����
    public Transform runPoint; // ����� ������ ����� ��� ����������� � ���������� ������
    public Transform preparationPoint; // ����� ���������� � ������
    public Transform target; // ����, �� 
    public MovementWayRunners MyPath; // ������������ ����
    public MovementWayRunners MyPath2;
    public MovementWayRunners MyPath3;
    public float speedUp; // ���������� �������� ������� (��� ������)
    public float speed = 1; // �������� ��������

    private bool Change;
    private IEnumerator<Transform> pointInPath; // �������� �����

    void Start()
    {
        if (MyPath == null) // ��������, ���������� �� �� ����
        {
            Debug.Log("�� ������ ����");
            return;
        }

        pointInPath = MyPath.GetNextPathPoint(); // ��������� � �������� GetNextPathPoint
        pointInPath.MoveNext(); // ��������� ��������� ����� � ����
        
        if (pointInPath.Current == null) // ��������, ���� �� ����� � ������� ���������
        {
            Debug.Log("� ��������� ���� ����������� ����� ��������");
            return;
        }

        Change = false;
        transform.position = pointInPath.Current.position; // ������ ������ ������ �� ��������� ����� ����
        transform.LookAt(target.position); // ������ ������ �������� � ������� ����������� ��������
    }

    void Update()
    {
        if (pointInPath == null || pointInPath.Current == null) // �������� ���������� ����
        {
            return; // �����, ������ ��� ���� ���
        }
            
        transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed); // ������� ������ � ��������� �����

        if (Change == false)
        {
            transform.LookAt(target.position);
        }
              
        if (transform.position == pointInPath.Current.position) // ���� ������ ����� �� �����, � ������� ��������, ��
        {
            pointInPath.MoveNext(); // ����� �������� � ��������� ����� � ����
        }

        if (Judge.transform.position == preparationPoint.position) // ����� ����� ����� �� �������������, ��
        {
            pointInPath = MyPath2.GetNextPathPoint(); // ����� ������������ ������ ���� (�������� �� ����� ������)
            pointInPath.MoveNext();
            speed += 1;
            Change = true;
        }

        if (Change == true)
        {
            transform.LookAt(pointInPath.Current.position);
        }

        if (FlagOrRunner.transform.position == runPoint.position) // ���� ����/����� ������, ���� �����/����� ����������� � ���������� ������
        {
            pointInPath = MyPath3.GetNextPathPoint(); // ����� ������������ ������ ���� (�������� �� ��������) 
            pointInPath.MoveNext();
            speed += speedUp;
        }
    }
}
