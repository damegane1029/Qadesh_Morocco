
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class StaffWarp : UdonSharpBehaviour
{
    [Header("ユーザー名のテキストを入力")]
    [SerializeField] public string[] Str_AuthorizedUser;
    [Header("ワープ先のTransform")]
    [SerializeField] public Transform move_point;
    string localname;

    void Start()
    {
        localname = Networking.LocalPlayer.displayName;
    }

    public override void Interact()
    {
        if (Str_AuthorizedUser.Length > 0)
        {
            for (int i = 0; i < Str_AuthorizedUser.Length; i++)
            {
                if (((object)Str_AuthorizedUser[i]).Equals(((object)localname)))
                {
                    Networking.LocalPlayer.TeleportTo(move_point.position, move_point.rotation);
                    break;
                }
            }
        }
    }
}
