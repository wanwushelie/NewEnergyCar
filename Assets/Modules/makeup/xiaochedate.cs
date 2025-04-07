using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class ExtensionMethods
{
    public static bool Between<T>(this T value, T min, T max) where T : IComparable<T>
    {
        return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
    }
}
public class xiaochedate : MonoBehaviour
{
    public int totalwending = 0, totalweight = 0,score;
  

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
   public  void jugdement()
    {
        if (totalweight.Between(20,30))
        {
            if(totalwending.Between(10,20))
            {
                score = 52;
            }
            if (totalwending.Between(20, 30))
            {
                score = 60;
            }
            if (totalwending.Between(30, 40))
            {
                score = 73;
            }
        }
        if (totalweight.Between(30, 40))
        {
            if (totalwending.Between(10, 20))
            {
                score = 61;
            }
            if (totalwending.Between(20, 30))
            {
                score = 87;
            }
            if (totalwending.Between(30, 40))
            {
                score = 92;
            }
        }
        if (totalweight.Between(40, 50))
        {
            if (totalwending.Between(10, 20))
            {
                score = 70;
            }
            if (totalwending.Between(20, 30))
            {
                score = 84;
            }
            if (totalwending.Between(30, 40))
            {
                score = 65;
            }
        }
    }
}

