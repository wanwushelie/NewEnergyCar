using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static lingjiandate;

public class lingjiandate : MonoBehaviour
{
    public struct canbemakeup
    {
        public  GameObject AgameObject;
        public      string gameobjectname;
        public    int wending, weight;
        //public       bool isbemakeup;
        public canbemakeup(GameObject obj, string name, int weiding, int weight)
        {
            AgameObject = obj;
            gameobjectname = name;
            this.wending = weiding;
            this.weight = weight;
            //isbemakeup = false;
        }
    }//��װж�������
    public  canbemakeup[] C;
    public GameObject SC, JC, ST, JT, SD, JD;
    void Start()
    {
    C= new canbemakeup[6];
    C[0] = new canbemakeup(SC, "����chelun" , 3, 3);
    C[1] = new canbemakeup(JC, "����chelun", 7, 7);
    C[2] = new canbemakeup(ST, "����tulun", 4,9);
    C[3] = new canbemakeup(JT, "����tulun", 10, 18);
    C[4] = new canbemakeup(SD, "����dizuo", 9, 12);
    C[5] = new canbemakeup(JD, "����dizuo", 20, 18);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
