
// Created by myxy.
// Redistribution is prohibited.
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Myxy.Glass.Udon
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
    public class GlassPickup : UdonSharpBehaviour
    {
        [SerializeField]
        private Glass glass;
        [SerializeField]
        private bool isInteractable = false;

        void Update()
        {

        }
        public Glass GetGlass()
        {
            return glass; 
        }

        public override void OnPickup()
        {
            glass.OnPickup();
        }

        public override void OnPickupUseUp()
        {
            glass.OnPickupUseUp();
        }

        public override void OnPickupUseDown()
        {
            glass.OnPickupUseDown();
        }

        public override void Interact()
        {
            if (glass.IsInteractableByPickup)
            {
                glass.SetOwner();
                glass.IsOpen = glass.IsCloseOnExhaust || !glass.IsOpen;
            }
        }
    }
}
