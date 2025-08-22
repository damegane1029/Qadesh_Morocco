
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class EntranceSwitch : UdonSharpBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip openclip;
    Collider mycollider;
    void Start()
    {
        mycollider=GetComponent<Collider>();
        wall.SetActive(true);
        mycollider.enabled = true;
    }

    public override void Interact()
    {
        wall.SetActive(false);
        AS.PlayOneShot(openclip);
        mycollider.enabled = false;
    }
}
