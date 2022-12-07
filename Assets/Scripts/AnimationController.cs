using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private GameObject _outline;
    [SerializeField] private AnimationClip _blink;
    [SerializeField] private AnimationClip _rotation;

    public void BlinkOutline()
    {
        var animation = _outline.GetComponent<Animation>();
        animation.clip = _blink;
        animation.Play();
    }

    public void RotateCard()
    {
        var animation = GetComponent<Animation>();
        animation.clip = _rotation;
        animation.Play();
    }
}
