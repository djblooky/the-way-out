﻿using UnityEngine;

public class Openable : Interactable
{
    public bool IsOpen
    {
        get { return isOpen; }

        set
        {
            animator.SetBool(isOpenAnimatorParam, value);
            isOpen = value;
        }
    }

    [SerializeField]
    public bool isLocked = false;

    [SerializeField]
    private bool canBeClosed = true;

    [SerializeField]
    private string openText = "Open", closeText = "Close";

    [SerializeField]
    private AudioClip openSound, closeSound, lockSound;

    [SerializeField]
    protected Animator animator;

    [SerializeField]
    protected AudioSource audioSource;

    protected bool isOpen = false;
    private readonly int isOpenAnimatorParam = Animator.StringToHash("isOpen");

    public override void Interact()
    {
        ToggleOpen();
    }

    protected override void OnHoveredOver(Interactable i)
    {
        base.OnHoveredOver(i);

        if(!isLocked)
            SetHoverText();
    }

    private void SetHoverText()
    {
        if (IsOpen)
        {
            nextHoverText = closeText;
        }
        else
        {
            nextHoverText = openText;
        }
    }

    private void ToggleOpen()
    {
        if (isLocked)
        {
            nextHoverText = "Locked";
            audioSource.PlayOneShot(lockSound);
        }
        else
        {
            if (isOpen && canBeClosed)
            {
                IsOpen = false;
                audioSource.PlayOneShot(closeSound);
            }
            else
            {
                IsOpen = true;
                audioSource.PlayOneShot(openSound);
                if (!canBeClosed)
                    gameObject.tag = "Untagged";
            }
        }
    }
}