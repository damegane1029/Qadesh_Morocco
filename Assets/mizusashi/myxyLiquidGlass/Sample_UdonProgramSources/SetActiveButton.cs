
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class SetActiveButton : UdonSharpBehaviour
{
    [SerializeField]
    private GameObject target = null;
    [SerializeField]
    private bool isGlobal = false;
    private bool isActive;
    [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(IsActiveSync))]
    private bool isActiveSync;
    public bool IsActiveSync
    {
        get => isActiveSync;
        set
        {
            isActiveSync = value;
            if (isGlobal)
            {
                isActive = isActiveSync;
                target.SetActive(isActive);
            }
        }
    }
    void Start()
    {
        if (target != null)
        {
            isActive = target.activeSelf;
        }
    }

    void Interact()
    {
        if (target != null)
        {
            isActive = !isActive;
            target.SetActive(isActive);
            if (isGlobal)
            {
                Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
                IsActiveSync = isActive;
            }
        }
    }
}
