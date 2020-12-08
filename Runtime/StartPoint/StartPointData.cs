
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exop.Targeter
{
    [CreateAssetMenu(fileName = "StartPoint", menuName = "Exop/StartPoint")]
public class StartPointData : ScriptableObject
{
    public Vector2 startPosition;
    public Vector2 mousePosition;
    public Vector2 direction;
    /** Nişan alma ileminin başarılı veya başarısız olduğunu bildirir.   */
    public bool canShot = false;
    [SerializeField]
    private float canShotRange = 1f;
    public float dot = 0;


    public void changeDirection(Vector2 endPosition)
    {
        mousePosition = endPosition;
        direction = (endPosition - startPosition);
        direction.Normalize();
        // direction = startPosition + direction;
        // Debug.DrawLine(startPosition, startPosition + direction, Color.red, 1);
        // eğer beilrtilen açıya ulaşılırsa çizgi çizecek
        calculateRadius();
    }

    private void calculateRadius()
    {
        // farenin konumunun başlangıç noktasına olan dikliği
        dot = Vector2.Dot(-direction, Vector2.right);
        // Debug.Log("açı:" + dot);
        // Debug.Log("açı:" + dot + "canShotRange:" + canShotRange);
        canShot = canShotRange >= Math.Abs(dot) && mousePosition.y > startPosition.y;
        // Debug.Log("Dot:" + dot);
      
    }

    public void setStartPosition(Vector2 value)
    {
        startPosition = value;
    }
}

}