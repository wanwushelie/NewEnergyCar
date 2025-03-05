using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static lingjiandate;

public class lingjiandate : MonoBehaviour
{
    public struct canbemakeup
    {
        
        public string gameobjectname;
        public int wending, weight;
        //public       bool isbemakeup;
        public canbemakeup( string name, int weiding, int weight)
        {
            
            gameobjectname = name;
            this.wending = weiding;
            this.weight = weight;
            //isbemakeup = false;
        }
    }//可装卸零件属性
    public canbemakeup[] C;
    void Start()
    {
        C = new canbemakeup[6];
        C[0] = new canbemakeup( "塑料chelun", 3, 3);
        C[1] = new canbemakeup( "金属chelun", 7, 7);
        C[2] = new canbemakeup( "塑料tulun", 4, 9);
        C[3] = new canbemakeup( "金属tulun", 10, 18);
        C[4] = new canbemakeup( "塑料dizuo", 9, 12);
        C[5] = new canbemakeup( "金属dizuo", 20, 18);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

