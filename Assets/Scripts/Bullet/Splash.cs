using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD
{
    public class Splash : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        public void OnDisable()
        {
            Destroy(this.gameObject);
        }

        private void OnEnable()
        {
            this.animator.SetBool("SplashActivate", true);
        }
    }
}
