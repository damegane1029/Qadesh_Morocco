
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class ElevatorSwitch : UdonSharpBehaviour
{
    [SerializeField] Animator ElevatorAnimator1;
    [SerializeField] Animator ElevatorAnimator2;
    [SerializeField] Transform warpPoint;
    [SerializeField] AudioSource ElevatorAS;
    VRCPlayerApi localPlayer;
    float beforeJumpImpulse;
    float beforeRunSpeed;
    float beforeWalkSpeed;
    float beforeStrafeSpeed;

    void Start()
    {
        ElevatorAnimator1.SetBool("close", false);
        ElevatorAnimator2.SetBool("close", false);
        localPlayer = Networking.LocalPlayer;
        beforeRunSpeed = localPlayer.GetRunSpeed();
        beforeWalkSpeed = localPlayer.GetWalkSpeed();
        beforeJumpImpulse = localPlayer.GetJumpImpulse();
        beforeStrafeSpeed = localPlayer.GetStrafeSpeed();
    }

    public override void Interact()
    {
        localPlayer.SetRunSpeed(0.1f);
        localPlayer.SetWalkSpeed(0.1f);
        localPlayer.SetJumpImpulse(0);
        localPlayer.SetStrafeSpeed(0.1f);
        ElevatorAnimator1.SetBool("close", true);
        ElevatorAnimator2.SetBool("close", true);
        SendCustomEventDelayedSeconds("Warp", 4f);
    }

    public void Warp()
    {
        localPlayer.TeleportTo(warpPoint.position,warpPoint.rotation);
        ElevatorAnimator1.SetBool("close", false);
        ElevatorAnimator2.SetBool("close", false);
        localPlayer.SetRunSpeed(beforeRunSpeed);
        localPlayer.SetWalkSpeed(beforeWalkSpeed);
        localPlayer.SetJumpImpulse(beforeJumpImpulse);
        localPlayer.SetStrafeSpeed(beforeStrafeSpeed);
    }
}
