
using System.Net;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using static VRC.SDKBase.VRC_Trigger;
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class QadeshCommonTeleporter : UdonSharpBehaviour
{
    [Header("インタラクトでワープするかどうか（オフでEnterワープ）")]
    [SerializeField] bool isInteract;
    [Header("ワープする先")]
    [SerializeField] Transform EndPoint;
    [Header("ワープ効果音")]
    [SerializeField] AudioClip teleportSound;
    [Header("フェードイベント")]
    [SerializeField] private FadeEvent _fadeEvent;
    [Header("オプション：テレポート先のテレポーター（往復の際に楽。空白可）")]
    [Space(10)]
    [SerializeField] QadeshCommonTeleporter QCT;

    [Header("オプション：スタッフドアにするかどうか")]
    [Space(10)]
    [SerializeField] bool isStaffDoor;
    [Header("オプション：スタッフリスト")]
    [SerializeField] string[] staffList;
    [Header("非スタッフ時のブザー音")]
    [SerializeField] AudioClip notStaffSound;
    AudioSource myAudioSource;

    bool IsTeleported;

    public void Start()
    {
        if (!isInteract) DisableInteractive = true;
        myAudioSource=GetComponent<AudioSource>();
    }
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (player != Networking.LocalPlayer || IsTeleported || isInteract || _fadeEvent.IsFading)
            return;

        if (isStaffDoor)
        {
            if (isStaff(Networking.LocalPlayer.displayName))
            {
                _CallTeleport();
            }
            else
            {
                if(notStaffSound!=null)myAudioSource.PlayOneShot(notStaffSound);
            }
        }
        else
        {
            _CallTeleport();
        }
    }

    public override void Interact()
    {
        if (_fadeEvent.IsFading)
            return;

        if (isStaffDoor)
        {
            if (isStaff(Networking.LocalPlayer.displayName))
            {
                _CallTeleport();
            }
            else
            {
                if (notStaffSound != null) myAudioSource.PlayOneShot(notStaffSound);
            }
        }
        else
        {
            _CallTeleport();
        }
    }

    public void _CallTeleport()
    {
        if (_fadeEvent.IsFading)
            return;
        if (teleportSound != null) myAudioSource.PlayOneShot(teleportSound);
        _fadeEvent._StartFade();
        Networking.LocalPlayer.Immobilize(true);
        IsTeleported = true;
    }

    public void OnEndFadeInEvent()
    {
        PlayerTeleport();
    }
    public void OnEndFadeOutEvent()
    {
        Networking.LocalPlayer.Immobilize(false);
        IsTeleported = false;
    }

    public void PlayerTeleport()
    {
        if (QCT != null)
        {
            Networking.LocalPlayer.TeleportTo(QCT.transform.Find("ExitPoint").position, QCT.transform.Find("ExitPoint").rotation);
        }
        else
        {
            Networking.LocalPlayer.TeleportTo(EndPoint.position, EndPoint.rotation);
        }
    }


    private bool isStaff(string name)
    {
        for(int i=0;i<staffList.Length;i++) 
        {
            if (name.Equals(staffList[i]))
            {
                return true;
            }
        }
        return false;
    }
}
