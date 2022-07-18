using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWayRunners : MonoBehaviour
{
    public enum WayType // ¬иды пути: линейный или заикленный
    {
        Linear,
        Loop
    }

    public WayType type; // “ип пути
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

    //    if (type == WayType.Loop) // ≈сли выбрат зацикленный путь,
    //    {
    //        Gizmos.DrawLine(WayElements[0].position, WayElements[WayElements.Length - 1].position); // то рисуем линию от первой к последней точке
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

            if (type == WayType.Linear) // ≈сли выбран линейный путь
            {
                if (movingTo <= 0) // и двигаемс€ вперед (по нарастающей), то
                {
                    movementDirection = 1; // добавл€ем 1 к движению
                }
                else if (movingTo >= WayElements.Length - 1) // и двигаемс€ назад (по убывающей), то
                {
                    movementDirection = 0; // убираем скорость
                }
            }

            movingTo = movingTo + movementDirection; // ƒиапазон движени€ от 1 до -1

            if (type == WayType.Loop) // ≈сли выбран зацикленный путь
            {
                if (movingTo >= WayElements.Length) // и мы дошли до последней точки, то
                {
                    movingTo = 0; // нужно двигатьс€ к первой точке (а не в обратную сторону)
                }

                if (movingTo < 0) // и мы дошли до первой точки, то
                {
                    movingTo = WayElements.Length - 1; // нужно двигатьс€ к последней точке
                }
            }

        }
    }
}
