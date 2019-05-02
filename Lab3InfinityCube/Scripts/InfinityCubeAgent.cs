using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class InfinityCubeAgent : Agent
{

    public Transform Target;
    public Transform Terrain;
    public Transform myAgent;

    private float bufferDistance;

    public InfinityCubeAcademy academy;
    float originalDistance;

    /// <summary>
    /// 
    /// </summary>
    
    private Vector3 _anchor;
    private Vector3 _axis;

    private int _random;

    private Transform a;
    private Transform b;
    private Transform c;
    private Transform d;

    Transform jointab;
    Transform jointac;
    Transform jointbd;
    Transform jointba;
    Transform jointca;
    Transform jointdb;

    private Transform buffer1;
    private Transform buffer2;
    private Transform jointbuffer;

    private float rotateAngle;

    private Plane plane;
    private Vector3 rotateAxis;

    private float distanceToTarget;
    void Start()
    {
        originalDistance = 20;

        a = transform.Find("a");
        b = transform.Find("b");
        c = transform.Find("c");
        d = transform.Find("d");

        rotateAngle = 0;
        rotateAxis = new Vector3();

        jointab = a.transform.Find("jointab");
        jointac = a.transform.Find("jointac");
        jointbd = b.transform.Find("jointbd");
        jointba = b.transform.Find("jointba");
        jointca = c.transform.Find("jointca");
        jointdb = d.transform.Find("jointdb");

        myAgent.position = new Vector3(a.position.x + b.position.x + c.position.x + d.position.x, a.position.y + b.position.y + c.position.y + d.position.y, a.position.z + b.position.z + c.position.z + d.position.z);

    }

    public override void CollectObservations()
    {
        // Target and Agent positions

        AddVectorObs(a.position - myAgent.transform.position);
        AddVectorObs(b.position - myAgent.transform.position);
        AddVectorObs(c.position - myAgent.transform.position);
        AddVectorObs(d.position - myAgent.transform.position);

        // TODO: improve 
        AddVectorObs(Target.localPosition - myAgent.transform.position);
        AddVectorObs(distanceToTarget / originalDistance);
    }

    private float previousDistance = float.MaxValue;

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        myAgent.position =new Vector3((a.position.x + b.position.x + c.position.x + d.position.x)/4, (a.position.y + b.position.y + c.position.y + d.position.y) / 4,( a.position.z + b.position.z + c.position.z + d.position.z) / 4); 


        int action = Mathf.FloorToInt(vectorAction[0]);

        Monitor.Log("DistanceScore",  Reward(originalDistance, distanceToTarget));

        distanceToTarget = Vector3.Distance(myAgent.transform.position,
                                                  Target.localPosition);

        // Rewards
        SetReward(0.5f + Reward(originalDistance, distanceToTarget));

        // Reached target

        if (distanceToTarget < 0.5f)
        {
            AddReward(10f);
            Done();
            return;
        }
        

        // Fell off platform
        if (a.transform.position.x < -Terrain.localScale.x / 2 || a.transform.position.x > Terrain.localScale.x / 2 ||
            a.transform.position.z < -Terrain.localScale.z / 2 || a.transform.position.z > Terrain.localScale.z / 2
            ||
            b.transform.position.x < -Terrain.localScale.x / 2 || b.transform.position.x > Terrain.localScale.x / 2 ||
            b.transform.position.z < -Terrain.localScale.z / 2 || b.transform.position.z > Terrain.localScale.z / 2
            ||
            c.transform.position.x < -Terrain.localScale.x / 2 || c.transform.position.x > Terrain.localScale.x / 2 ||
            c.transform.position.z < -Terrain.localScale.z / 2 || c.transform.position.z > Terrain.localScale.z / 2
            ||
            d.transform.position.x < -Terrain.localScale.x / 2 || d.transform.position.x > Terrain.localScale.x / 2 ||
            d.transform.position.z < -Terrain.localScale.z / 2 || d.transform.position.z > Terrain.localScale.z / 2
        )
        {
            AddReward(-1.0f);
            Done();
            return;
        }

        // Actions, size = 1
        int controlSignal = 0;
        if (action == 0)
        {
            // do nothing
            return;
        }
        else if (action == 1){
            controlSignal = 1;SetRotateAnchor();
        }
        else if (action == 2) { 
            controlSignal = 2; SetRotateAnchor();
        }
        else if (action == 3) { 
            controlSignal = 3; SetRotateAnchor();
        }
        else if (action == 4) { 
            controlSignal = 4;SetRotateAnchor();
        }
        else if (action == 5) { 
            controlSignal = 5; SetRotateAnchor();
        }
        else if (action == 6) { 
            controlSignal = 6; SetRotateAnchor();
        }
        else if (action == 7) { 
            controlSignal = 7; SetRotateAnchor();
        }
        else if (action == 8) { 
            controlSignal = 8; SetRotateAnchor();
        }
        else if (action == 9) { 
            controlSignal = 9; SetRotateAnchor();
        }
        else if (action == 10) { 
            controlSignal = 10; SetRotateAnchor();
        }
        else if (action == 11) { 
            controlSignal = 11; SetRotateAnchor();
        }
        else if (action == 12) { 
            controlSignal = 12; SetRotateAnchor();
        }

        if (distanceToTarget > 0.5f)
        {
            _random = controlSignal - 1;
        }
        ///
        ///
        ///
        ///


        if (Mathf.Abs(rotateAngle) < 180 && rotateAxis != Vector3.zero)
        {
            if (_random == 0)
            {
                // ab
                d.transform.parent = b.transform;
                b.RotateAround(jointab.position, rotateAxis, 180);
                rotateAngle += 180;
            }

            if (_random == 1)
            {
                // ac
                c.RotateAround(jointac.position, rotateAxis, 180);
                rotateAngle += 180;
            }

            if (_random == 2)
            {
                // bd
                d.RotateAround(jointbd.position, rotateAxis, 180);
                rotateAngle += 180;
            }

            if (_random == 3)
            {
                // ba
                c.transform.parent = a.transform;
                a.RotateAround(jointba.position, rotateAxis, 180);
                rotateAngle += 180;
            }

            if (_random == 4)
            {
                // ca
                b.transform.parent = a.transform;
                d.transform.parent = a.transform;
                a.RotateAround(jointca.position, rotateAxis, 180);
                rotateAngle += 180;
            }

            if (_random == 5)
            {
                // db
                a.transform.parent = b.transform;
                c.transform.parent = b.transform;
                b.RotateAround(jointdb.position, rotateAxis, 180);
                rotateAngle += 180;
            }

            if (_random == 6)
            {
                // a先不动，b90，a碰
                if (rotateAngle < 90)
                {
                    d.transform.parent = b.transform;
                    b.RotateAround(jointab.position, rotateAxis, 90);
                    rotateAngle += 90;
                    ResetParent();
                
                    c.transform.parent = a.transform;
                    a.RotateAround(jointba.position, rotateAxis, 90);
                    rotateAngle += 90;
                }
            }

            if (_random == 7)
            {
                // a先不动，c90，a碰
                if (rotateAngle < 90)
                {
                    c.RotateAround(jointac.position, rotateAxis, 90);
                    rotateAngle += 90;
                    ResetParent();
               
                    b.transform.parent = a.transform;
                    d.transform.parent = a.transform;
                    a.RotateAround(jointca.position, rotateAxis, 90);
                    rotateAngle += 90;
                }
            }
            if (_random == 8)
            {
                // b先不动，d90，b碰
                if (rotateAngle < 90)
                {
                    d.RotateAround(jointbd.position, rotateAxis, 90);
                    rotateAngle += 90;
                    ResetParent();
                
                    a.transform.parent = b.transform;
                    c.transform.parent = b.transform;
                    b.RotateAround(jointdb.position, rotateAxis, 90);
                    rotateAngle += 90;
                }
            }

            if (_random == 9)
            {
                // b先不动，a90，b碰
                if (rotateAngle < 90)
                {
                    c.transform.parent = a.transform;
                    a.RotateAround(jointba.position, rotateAxis, 90);
                    rotateAngle += 90;
                    ResetParent();
                
                    d.transform.parent = b.transform;
                    b.RotateAround(jointab.position, rotateAxis, 90);
                    rotateAngle += 90;
                }
            }
            if (_random == 10)
            {
                // c先不动，a90，c碰
                if (rotateAngle < 90)
                {
                    b.transform.parent = a.transform;
                    d.transform.parent = a.transform;
                    a.RotateAround(jointca.position, rotateAxis, 90);
                    rotateAngle += 90;
                    ResetParent();
                
                    c.RotateAround(jointac.position, rotateAxis, 90);
                    rotateAngle += 90;
                }
            }
            if (_random == 11)
            {
                // d先不动，b90，d碰
                if (rotateAngle < 90)
                {
                    a.transform.parent = b.transform;
                    c.transform.parent = b.transform;
                    b.RotateAround(jointdb.position, rotateAxis, 90);
                    rotateAngle += 90;
                    ResetParent();
               
                    d.RotateAround(jointbd.position, rotateAxis, 90);
                    rotateAngle += 90;

                }
            }
        }

        if (Mathf.Abs(rotateAngle) >= 180)
            Reset();
    }


    void Reset()
    {
        rotateAngle = 0;
        rotateAxis = Vector3.zero;

        ResetParent();
        _random = 0;
    }

    private void retract()
    {
        // TODO: write the code that retracts the blocks
    }

    private float Reward(float originalDistance, float distanceToTarget)
    {
        float score =  - distanceToTarget / originalDistance;
        return score;
    }


    public override void AgentReset()
    {
        // reset according academy
        //Terrain.localScale = new Vector3(academy.resetParameters["x"], 1f, academy.resetParameters["z"]);


        // The Agent fell
        if (a.transform.position.x < -Terrain.localScale.x / 2 || a.transform.position.x > Terrain.localScale.x / 2 ||
            a.transform.position.z < -Terrain.localScale.z / 2 || a.transform.position.z > Terrain.localScale.z / 2
            ||
            b.transform.position.x < -Terrain.localScale.x / 2 || b.transform.position.x > Terrain.localScale.x / 2 ||
            b.transform.position.z < -Terrain.localScale.z / 2 || b.transform.position.z > Terrain.localScale.z / 2
            ||
            c.transform.position.x < -Terrain.localScale.x / 2 || c.transform.position.x > Terrain.localScale.x / 2 ||
            c.transform.position.z < -Terrain.localScale.z / 2 || c.transform.position.z > Terrain.localScale.z / 2
            ||
            d.transform.position.x < -Terrain.localScale.x / 2 || d.transform.position.x > Terrain.localScale.x / 2 ||
            d.transform.position.z < -Terrain.localScale.z / 2 || d.transform.position.z > Terrain.localScale.z / 2
            )
        {
            myAgent.transform.position = new Vector3(0, 0, 0);
            a.transform.localPosition = new Vector3(0, 0, 0);
            b.transform.localPosition = new Vector3(1, 0, 0);
            c.transform.localPosition = new Vector3(0, 1, 0);
            d.transform.localPosition = new Vector3(1, 1, 0);


            Target.localPosition = new Vector3((int)(Random.value * Terrain.localScale.x - Terrain.localScale.x / 2),
                0,
                (int)(Random.value * Terrain.localScale.z - Terrain.localScale.z / 2));

            distanceToTarget = Vector3.Distance(myAgent.transform.position,
                Target.localPosition);

            originalDistance = distanceToTarget;
        }


        float distanceToTargetDone = Vector3.Distance(myAgent.transform.localPosition,
            Target.localPosition);


        //Reached target
        if (distanceToTargetDone <0.5f)
        {
            Target.localPosition = new Vector3((int)(Random.value * Terrain.localScale.x - Terrain.localScale.x / 2),
                                          0,
                                          (int)(Random.value * Terrain.localScale.z - Terrain.localScale.z / 2));

            myAgent.transform.position = new Vector3(0, 0, 0);
            a.transform.localPosition = new Vector3(0, 0, 0);
            b.transform.localPosition = new Vector3(1, 0, 0);
            c.transform.localPosition = new Vector3(0, 1, 0);
            d.transform.localPosition = new Vector3(1, 1, 0);

            distanceToTarget = Vector3.Distance(myAgent.transform.position,
                Target.localPosition);

            originalDistance = distanceToTarget;
        }

    }

    public override void AgentOnDone()
    {

    }

    void SetRotateAnchor()
    {
        if (_random == 0 || _random == 6)
        {
            plane = new Plane(a.position, b.position, jointab.position);
        }
        if (_random == 1 || _random == 7)
        {
            plane = new Plane(a.position, c.position, jointac.position);
        }
        if (_random == 2 || _random == 8)
        {
            plane = new Plane(b.position, d.position, jointbd.position);
        }
        if (_random == 3 || _random == 9)
        {
            plane = new Plane(b.position, a.position, jointab.position);
        }
        if (_random == 4 || _random == 10)
        {
            plane = new Plane(c.position, a.position, jointac.position);
        }
        if (_random == 5 || _random == 11)
        {
            plane = new Plane(d.position, b.position, jointbd.position);
        }


        rotateAxis = plane.normal;
    }
    void ResetParent()
    {
        a.transform.parent = this.transform;
        b.transform.parent = this.transform;
        c.transform.parent = this.transform;
        d.transform.parent = this.transform;
    }
}
