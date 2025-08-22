
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

//Noneに固定する
[UdonBehaviourSyncMode(BehaviourSyncMode.None)]

public class EnterDisplay : UdonSharpBehaviour
{
    [SerializeField] private CanvasGroup ImageCanvas;
    [SerializeField, Tooltip("フェードイン時間")] private float FadeInTime = 1.0f;
    [SerializeField, Tooltip("待機時間")] private float WaitingTime = 5.0f;
    [SerializeField, Tooltip("フェードアウト時間")] private float FadeOutTime = 1.0f;
    private float time = 0.0f;

    void OnEnable()
    {
        time = 0.0f;
        ImageCanvas.alpha = 0.0f;
    }

    void Update()
    {
        LookAtPlayer();
        time += Time.deltaTime;

        //  フェードイン
        if (0.0f <= time && time <= FadeInTime)
        {
            ImageCanvas.alpha = time / FadeInTime;
        }
        // 待機
        else if (FadeInTime < time && time <= FadeInTime + WaitingTime)
        {
            ImageCanvas.alpha = 1.0f;
        }
        // フェードアウト
        else if (FadeInTime + WaitingTime < time && time <= FadeInTime + WaitingTime + FadeOutTime)
        {
            ImageCanvas.alpha = 1 - ((time - (FadeInTime + WaitingTime)) / FadeOutTime);
        }
        else
        {
            time = 0.0f;
            ImageCanvas.alpha = 0.0f;
            Destroy(this.gameObject);
        }
    }

    private void LookAtPlayer() // LookAt処理
    {
        var player = Networking.LocalPlayer;
        if (player != null)
        {
            var headData = player.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
            transform.position = new Vector3(headData.position.x, headData.position.y, headData.position.z);
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, headData.rotation.eulerAngles.y - 90.0f, -headData.rotation.eulerAngles.x));
        }
    }
}
