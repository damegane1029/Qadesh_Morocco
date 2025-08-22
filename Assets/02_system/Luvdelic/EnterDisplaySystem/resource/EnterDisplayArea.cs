
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

//Noneに固定する
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]

public class EnterDisplayArea : UdonSharpBehaviour
{
    [SerializeField] private GameObject DisplayPrefab;
    private GameObject instance = null;
    private bool SpawnEnable = true;

    public void SetSpawnEnable(bool enable) { SpawnEnable = enable; }

    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        if (player != Networking.LocalPlayer) return; //LocalPlayer以外はスルー

        if(!Utilities.IsValid(instance))
        {
            if (SpawnEnable)
            {
                instance = VRCInstantiate(DisplayPrefab);
                SpawnEnable = false;
            }
        }
    }
}
