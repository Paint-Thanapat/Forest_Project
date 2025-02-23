using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAnimatorController
{
    public Animator anim;
    private string BoolIsWalk = "isWalk";
    private string BoolIsRun = "isRun";
    private string BoolIsSleep = "isSleep";
    private string BoolIsCollect = "isCollect";
    public int WalkHash { get; private set; }
    public int RunHash { get; private set; }
    public int SleepHash { get; private set; }
    public int CollectHash { get; private set; }

    public void Initialize()
    {
        WalkHash = Animator.StringToHash(BoolIsWalk);
        RunHash = Animator.StringToHash(BoolIsRun);
        SleepHash = Animator.StringToHash(BoolIsSleep);
        CollectHash = Animator.StringToHash(BoolIsCollect);
    }

    public void AnimSetBool(int animationHash, bool value)
    {
        anim.SetBool(animationHash, value);
    }
}
