using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWayCars : MonoBehaviour
{
    public GameObject Driver; // ������� � ������: ��������
    public GameObject Passenger; // ������� � ������: ��������
    public int movementDirection = 1; // ����������� �������: ������ ��� �����
    public int movingTo = 0; // � ����� ����� ���������
    public Transform[] WayElements; // ������ �� ����� ��������

    //public void OnDrawGizmos() // ���������� ����� ����� ������� ����
    //{
    //    if (WayElements == null || WayElements.Length < 2) // ���������, ���� �� ���� �� 2 �������� ����
    //    {
    //        return;
    //    }

    //    for (var i = 1; i < WayElements.Length; i++) // ��������� ��� ����� �������
    //    {
    //        Gizmos.DrawLine(WayElements[i - 1].position, WayElements[i].position); // ������ ������ ����� ����
    //    }
    //}

    public IEnumerator<Transform> GetNextPathPoint() // �������� ��������� ��������� �����
    {
        if (WayElements == null || WayElements.Length < 1) // ���������, ���� �� �����, ������� ����� ��������� ���������
        {
            yield break; // ��������� ����� �� ��������, ���� ����� ��������������
        }

        while (true) // ��������� ��� ����� �������
        {
            yield return WayElements[movingTo]; // ���������� ������� ��������� �����

            if (WayElements.Length == 1) // ���� ����� ����� ����, �� ����� �����
            {
                continue;
            }

            if (movingTo <= 0) // � ��������� ������ (�� �����������), ��
            {
                movementDirection = 1; // ��������� 1 � ��������
            }
            else if (movingTo >= WayElements.Length - 1) // �, ����� �� ��������� �����,
            {
                movementDirection = 0; // ���������������
                Driver.SetActive(true);
                Passenger.SetActive(true);
            }

            movingTo = movingTo + movementDirection; // �������� �������� �� 1 �� -1
        }
    }
}