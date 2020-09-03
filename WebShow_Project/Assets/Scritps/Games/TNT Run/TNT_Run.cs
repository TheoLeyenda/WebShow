using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TNT_Run
{
    public class TNT_Run : MonoBehaviour
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
        void Start()
        {

            isMortal = false;
            //Debug.Log(stateTNT.ToString());
        }

        void Update()
        {
            CheckAnimation();
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
        }
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                PlayerTopDown p = collider.GetComponent<PlayerTopDown>();
                switch (stateTNT)
                {
                    case StateTNT.Normal:
                        Detonate();
                        break;
                    case StateTNT.Empty:
                        if (isMortal && !p.invulnerhabilidad)
                        {
                            Destroy(p.gameObject);
                        }
                        break;
                }
            }
        }
    }
}
