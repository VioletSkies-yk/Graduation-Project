using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class GeneralCalculate
{
    /// <summary>
    /// 计算两点之间的距离
    /// </summary>
    public static float DistanceOfTowPoint(Vector2 point1, Vector2 point2)
    {
        return Vector2.Distance(point1, point2);
    }
    public static float DistanceOfTowPoint(Vector3 point1, Vector3 point2)
    {
        return Vector3.Distance(point1, point2);
    }

    /// <summary>
    /// 判断两点差值的绝对值是否小于等于一定值
    /// </summary>
    public static bool isAbsoluteValueInRange(float x, float y,float range)
    {
        return Math.Abs(x - y) <= range;
    }
}
