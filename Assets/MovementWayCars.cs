using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWayCars : MonoBehaviour
{
    public GameObject Driver; // „еловек в машине: водитель
    public GameObject Passenger; // „еловек в машине: пассажир
    public int movementDirection = 1; // Ќаправление вижени€: вперед или назад
    public int movingTo = 0; //   какой точке двигатьс€
    public Transform[] WayElements; // ћассив из точек движени€

    //public void OnDrawGizmos() // ќтображает линии между точками пути
    //{
    //    if (WayElements == null || WayElements.Length < 2) // ѕровер€ет, если ли хот€ бы 2 элемента пути
    //    {
    //        return;
    //    }

    //    for (var i = 1; i < WayElements.Length; i++) // ѕрогон€ет все точки массива
    //    {
    //        Gizmos.DrawLine(WayElements[i - 1].position, WayElements[i].position); // –исует линиии между ними
    //    }
    //}

    public IEnumerator<Transform> GetNextPathPoint() // ѕолучает положение следующей точки
    {
        if (WayElements == null || WayElements.Length < 1) // ѕровер€ет, если ли точки, которым нужно провер€ть положение
        {
            yield break; // ѕозвол€ет выйти из корутины, если нашел несоответствие
        }

        while (true) // ѕрогон€ет все точки массива
        {
            yield return WayElements[movingTo]; // ¬озвращает текущее положение точки

            if (WayElements.Length == 1) // ≈сли точка всего одна, то нужно выйти
            {
                continue;
            }

            if (movingTo <= 0) // и двигаемс€ вперед (по нарастающей), то
            {
                movementDirection = 1; // добавл€ем 1 к движению
            }
            else if (movingTo >= WayElements.Length - 1) // и, дойд€ до последней точки,
            {
                movementDirection = 0; // останавливаемс€
                Driver.SetActive(true);
                Passenger.SetActive(true);
            }

            movingTo = movingTo + movementDirection; // ƒиапазон движени€ от 1 до -1
        }
    }
}