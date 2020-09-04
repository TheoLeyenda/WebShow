using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TNT_Run
{
    using Players;
    public class TNT_Run : IEAviableObject
    {
        // Start is called before the first frame update
        public enum StateTNT
        {
            Normal,
            Detonated,
            Empty,
            None,
        }

        [System.Serializable]
        public class AnimationsData
        {
            public string name;
            public string nameIndex;
        }
        public bool isMortal = false;
        public List<AnimationsData> animations;
        public Animator animator;
        public StateTNT stateTNT;

        public bool enableUpdate = true;
        public float distanceCollision;

        private PlayerTopDown target;
        public PlayerTopDown[] players;

        void Start()
        {

            isMortal = false;
            aviable = true;
            //Debug.Log(stateTNT.ToString());
        }

        void Update()
        {
            CheckAnimation();
            if(enableUpdate)
            CheckCollisionPlayer(distanceCollision);
            if (stateTNT == StateTNT.Normal && !aviable)
            {
                aviable = true;
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
        public void Detonate()
        {
            stateTNT = StateTNT.Detonated;
        }
        public void CreateEmpty()
        {
            stateTNT = StateTNT.Empty;
            isMortal = true;
            aviable = false;
        }
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
                    CollisionMe();
                if (distance.magnitude > distanceCollision)
                {
                    target = null;
                }
            }
            else
            {
                FindPlayer();
            }
        }
        void CollisionMe()
        {
            switch (stateTNT)
            {
                case StateTNT.Normal:
                    Detonate();
                    break;
                case StateTNT.Empty:
                    if (isMortal && !target.invulnerhabilidad)
                    {
                        Destroy(target.gameObject);
                    }
                    break;
            }
        }
    }
}
