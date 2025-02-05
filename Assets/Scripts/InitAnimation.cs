using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;

public class InitAnimation : MonoBehaviour
{
    public SkeletonAnimation OwlAnimation;
    public AnimationState spineAnimationState;
    public string spineAnimationClipName;

    private void OnEnable()
    {
        spineAnimationState = OwlAnimation.AnimationState;
        spineAnimationState.SetAnimation(0, spineAnimationClipName, true);
    }
}