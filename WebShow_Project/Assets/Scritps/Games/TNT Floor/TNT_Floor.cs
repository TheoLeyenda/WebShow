using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT_Floor : MonoBehaviour
{
    // Start is called before the first frame update
    public float distanceCollision;
    public float delayDetonate;
    public float secondInDelayDetonate;
    private float auxDelayDetonate;
    private bool enableDelayDetonate;
    [System.Serializable]
    public class AnimationsData
    {
        public string name;
        public string nameIndex;
    }
    public List<AnimationsData> animations;
    public Animator animator;
    public StateTNT stateTNT;
    public float delayActivatedMeForCollision;
    private float auxDelayActivatedMeForCollision;
    private bool isMortal;
    private bool activateMe = false;
    private Player target;
    public Player[] players;
    //public bool activateDebug;
    public enum StateTNT
    {
        Normal,
        InTimerDetonate,
        DelayDetonate,
        Detonated,
        Empty,
        None,
    }
    void OnDisable()
    {
        delayDetonate = auxDelayDetonate;
        isMortal = false;
    }
   
    void Start()
    {
        auxDelayDetonate = delayDetonate;
        auxDelayActivatedMeForCollision = delayActivatedMeForCollision;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimation();
        CheckDelayDetonate();
        CheckCollisionPlayer(distanceCollision);
        //if (activateDebug)
        //Debug.Log(stateTNT);
    }
    void CheckDelayDetonate()
    {
        if (enableDelayDetonate)
        {
            if (delayDetonate > 0)
            {
                delayDetonate = delayDetonate - Time.deltaTime;
            }
            else if (delayDetonate <= 0 && !isMortal && stateTNT != StateTNT.Detonated)
            {
                Detonate();
            }
        }
    }
    // Update is called once per frame
    public void CheckAnimation()
    {

        for (int i = 0; i < animations.Count; i++)
        {
            if (animations[i].nameIndex == stateTNT.ToString())
            {
                animator.Play(animations[i].name);
            }
        }
    }
    public void CheckInDelayDetonateState()
    {
        if (delayDetonate <= secondInDelayDetonate && stateTNT != StateTNT.Empty && stateTNT != StateTNT.DelayDetonate)
        {
            ActivatedDelayDetonate();
        }
    }
    public void ActivatedTimerDetonate()
    {
        stateTNT = StateTNT.InTimerDetonate;
        enableDelayDetonate = true;
    }
    public void Detonate()
    {
        stateTNT = StateTNT.Detonated;
    }
    public void CreateEmpty()
    {
        if (stateTNT != StateTNT.Empty)
        {
            stateTNT = StateTNT.Empty;
            isMortal = true;
            DisabledEnableDelayDetonated();
        }
    }
    public void ActivatedDelayDetonate()
    {
        stateTNT = StateTNT.DelayDetonate;
    }
    public void DisableDelayDetonate()
    {
        stateTNT = StateTNT.None;
        enableDelayDetonate = false;
    }
    public void ActivatedEnableDelayDetonated()
    {
        enableDelayDetonate = true;
    }
    public void DisabledEnableDelayDetonated()
    {
        enableDelayDetonate = false;
    }
    public void CheckDelayActivatedMeForCollision()
    {
        if (delayActivatedMeForCollision > 0)
        {
            delayActivatedMeForCollision = delayActivatedMeForCollision - Time.deltaTime;
        }
        else if(stateTNT == StateTNT.Normal)
        {
            ActivatedTimerDetonate();
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" || target != null) return;
        target = collision.GetComponent<Player>(); 
        
    }*/
    void FindPlayer()
    {
        Vector3 distance;
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null)
            {
                distance = transform.position - players[i].transform.position;
                if (distance.magnitude <= distanceCollision)
                {
                    target = players[i];
                }
            }
        }
    }
    void CheckCollisionPlayer(float distanceCollision)
    {
        if (target != null)
        {
            Vector3 distance = transform.position - target.transform.position;
            //Debug.Log(player);
            //Debug.Log(distance.magnitude);
            if (distance.magnitude <= distanceCollision)
                CheckDelayActivatedMeForCollision();
            switch (stateTNT)
            {
                case StateTNT.Empty:
                    if (isMortal)
                    {
                        Destroy(target.gameObject);
                        target = null;
                    }
                    break;
            }
            if (distance.magnitude > distanceCollision)
            {
                delayActivatedMeForCollision = auxDelayActivatedMeForCollision;
                target = null;
            }
        }
        else
        {
            FindPlayer();
        }
    }
}
