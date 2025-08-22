
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AutoDoorEnter : UdonSharpBehaviour
{
    [SerializeField] Animator DoorAnimator;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip openclip;
    [SerializeField] AudioClip closeclip;
    VRCPlayerApi localplayer;
    void Start()
    {
        localplayer = Networking.LocalPlayer;
        DoorAnimator.SetBool("open", false);
    }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (player == localplayer)
        {
            if (!DoorAnimator.GetBool("open"))
            {
                DoorAnimator.SetBool("open", true);
                AS.PlayOneShot(openclip);
            }
        }
    }

    public override void OnPlayerTriggerExit(VRCPlayerApi player)
    {
        if (player == localplayer)
        {
            if (DoorAnimator.GetBool("open"))
            {
                DoorAnimator.SetBool("open", false);
                SendCustomEventDelayedSeconds("closeAudio", 2f);
            }
        }
    }
    
    public void closeAudio()
    {
        AS.PlayOneShot(closeclip);
    }
}
