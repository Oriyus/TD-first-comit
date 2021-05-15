using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD
{
    public class Splash : MonoBehaviour
    {
        private bool boom = false;
        private Vector3 startScale;
        private Vector3 targetScale;
        private float currentTime;
        private float duration = .5f;
        private float scale;

        public float Scale 
        { 
            get 
            { 
                return this.scale; 
            } 
            set 
            { 
                this.scale = value;
                this.targetScale = new Vector3(scale, scale, scale);
            }
        }

        private void OnEnable()
        {
            this.boom = true;
            this.startScale = new Vector3(0f, 0f, 0f);
            this.transform.localScale = this.startScale;
            this.currentTime = 0f;
        }

        private void Update()
        {
            if (this.scale > 0f && this.boom)
            {
                this.transform.localScale = Vector3.Lerp(this.startScale, this.targetScale, this.currentTime / this.duration);
                this.currentTime += Time.deltaTime;
                if (this.currentTime >= this.duration)
                {
                    this.boom = false;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
