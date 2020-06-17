using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT_Floor : MonoBehaviour
{
    // Start is called before the first frame update
    public float delayDetonate;
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
    private bool isMortal;
    public enum StateTNT
    {
        Normal,
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
    void CheckDelayDetonate()
    {
        if (enableDelayDetonate)
        {
            if (delayDetonate > 0)
            {
                delayDetonate = delayDetonate - Time.deltaTime;
            }
            else if (delayDetonate <= 0 && !isMortal)
            {
                Detonate();
            }
        }
    }
    void Start()
    {
        auxDelayDetonate = delayDetonate;

        //ActivatedDelayDetonate();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAnimation();
        CheckDelayDetonate();
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
    public void Detonate()
    {
        stateTNT = StateTNT.Detonated;
    }
    public void CreateEmpty()
    {
        stateTNT = StateTNT.Empty;
        isMortal = true;
        DisabledEnableDelayDetonated();
    }
    public void ActivatedDelayDetonate()
    {
        stateTNT = StateTNT.DelayDetonate;
        enableDelayDetonate = true;
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
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            Player p = collider.GetComponent<Player>();
            switch (stateTNT)
            {
                case StateTNT.Empty:
                    if (isMortal)
                    {
                        Destroy(p.gameObject);
                    }
                    break;
            }
        }
    }
}
