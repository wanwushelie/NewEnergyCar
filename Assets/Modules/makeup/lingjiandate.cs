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
    }//��װж�������
    public canbemakeup[] C;
    void Start()
    {
        C = new canbemakeup[6];
        C[0] = new canbemakeup( "����chelun", 3, 3);
        C[1] = new canbemakeup( "����chelun", 7, 7);
        C[2] = new canbemakeup( "����tulun", 4, 9);
        C[3] = new canbemakeup( "����tulun", 10, 18);
        C[4] = new canbemakeup( "����dizuo", 9, 12);
        C[5] = new canbemakeup( "����dizuo", 20, 18);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

