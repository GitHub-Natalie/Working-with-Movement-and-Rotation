using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWayRunners: MonoBehaviour
{
    public GameObject Judge; // Судья
    public GameObject FlagOrRunner; // Флаг или Бегун
    public Transform runPoint; // Точка взмаха флага или приближения к следующему бегуну
    public Transform preparationPoint; // Точка подготовки к забегу
    public Transform target; // Цель, на 
    public MovementWayRunners MyPath; // Используемый путь
    public MovementWayRunners MyPath2;
    public MovementWayRunners MyPath3;
    public float speedUp; // Увеличение скорости бегунов (при забеге)
    public float speed = 1; // Скорость движения

    private bool Change;
    private IEnumerator<Transform> pointInPath; // Проверка точек

    void Start()
    {
        if (MyPath == null) // Проверка, прикрепили ли мы путь
        {
            Debug.Log("Не выбран путь");
            return;
        }

        pointInPath = MyPath.GetNextPathPoint(); // Обращение к корутину GetNextPathPoint
        pointInPath.MoveNext(); // Получение следующей точки в пути
        
        if (pointInPath.Current == null) // Проверка, есть ли точка к которой двигаться
        {
            Debug.Log("В выбранном пути отсутствуют точки движения");
            return;
        }

        Change = false;
        transform.position = pointInPath.Current.position; // Объект должен встать на стартовую точку пути
        transform.LookAt(target.position); // Объект должен смотреть в сторону проводящего разминку
    }

    void Update()
    {
        if (pointInPath == null || pointInPath.Current == null) // Проверка отсутствия пути
        {
            return; // Выход, потому что пути нет
        }
            
        transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, Time.deltaTime * speed); // Двигать объект к следующей точке

        if (Change == false)
        {
            transform.LookAt(target.position);
        }
              
        if (transform.position == pointInPath.Current.position) // Если объект дошел до точки, к которой двигался, то
        {
            pointInPath.MoveNext(); // пусть движется к следующей точке в пути
        }

        if (Judge.transform.position == preparationPoint.position) // Когда судья дошел до разминающихся, то
        {
            pointInPath = MyPath2.GetNextPathPoint(); // Нужно использовать второй путь (движение на линию старта)
            pointInPath.MoveNext();
            speed += 1;
            Change = true;
        }

        if (Change == true)
        {
            transform.LookAt(pointInPath.Current.position);
        }

        if (FlagOrRunner.transform.position == runPoint.position) // Либо флаг/замах флагом, либо бегун/точка приближения к следующему бегуну
        {
            pointInPath = MyPath3.GetNextPathPoint(); // Нужно использовать третий путь (движение на эстафете) 
            pointInPath.MoveNext();
            speed += speedUp;
        }
    }
}
