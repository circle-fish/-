using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleRing : MonoBehaviour
{
    public ParticleSystem ps;
    int directionx,directiony;

    void Start() {
        ps= GetComponent<ParticleSystem>();
        directionx = 0;
        directiony = 0;
    }
 
    void Update() {
        int minX = -10, maxX = 10 ;
        if (directionx == 0 && ps.transform.position.x > minX){
            ps.transform.position += Vector3.left * Time.deltaTime * 50;
            Vector3 tmp=ps.transform.position;
            double numy=100.0f-(ps.transform.position.x*ps.transform.position.x);
            if(numy<0) numy=0;
            else numy=Math.Sqrt(numy);
            if(directiony==0) numy=numy*-1;
            tmp.y=(float)numy;
            ps.transform.position=tmp;
        } else if (directionx == 1 && ps.transform.position.x < maxX){
            ps.transform.position += Vector3.right * Time.deltaTime * 50;
            Vector3 tmp=ps.transform.position;
            double numy=100.0f-(ps.transform.position.x*ps.transform.position.x);
            if(numy<0) numy=0;
            else numy=Math.Sqrt(numy);
            if(directiony==0) numy=numy*-1;
            tmp.y=(float)numy;
            ps.transform.position=tmp;
        } else{
            directionx=1-directionx;
            directiony=1-directiony;
        }
    }
}
