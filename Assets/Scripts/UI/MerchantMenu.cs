using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    public void OpenMerchantMenu()
    {
        if (panel != null)
        {
            Animator animator = panel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");

                animator.SetBool("open", !isOpen);
            }
        }
    }
}
