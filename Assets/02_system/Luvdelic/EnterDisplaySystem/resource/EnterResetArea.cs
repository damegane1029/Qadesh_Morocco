
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

//Noneに固定する
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]

public class EnterResetArea : UdonSharpBehaviour
{
    [SerializeField] private EnterDisplayArea DisplayArea;

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (player != Networking.LocalPlayer) return; //LocalPlayer以外はスルー

        DisplayArea.SetSpawnEnable(true);
    }
}
