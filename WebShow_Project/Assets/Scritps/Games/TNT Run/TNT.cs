using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMode_TntRun
{
    public class TNT : MonoBehaviour
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
        private bool isMortal;
        public List<AnimationsData> animations;
        public Animator animator;
        public StateTNT stateTNT;
        void Start()
        {
            isMortal = false;
            //Debug.Log(stateTNT.ToString());
        }

        // Update is called once per frame
        void Update()
        {
            CheckAnimation();
        }
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
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                Player p = collider.GetComponent<Player>();
                switch (stateTNT)
                {
                    case StateTNT.Normal:
                        Detonate();
                        break;
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
}
