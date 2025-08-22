
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BGMFilter : UdonSharpBehaviour
{

    [SerializeField]
    private AudioLowPassFilter AudioPass;
    [SerializeField]
    private float fadingTime = 3.0f;
    [SerializeField]
    private Collider me;

    private float initDayAudioVolume;
    private float initNightAudioVolume;
    private float DayValue = 1.0f;
    private float NightValue = 0;
    private bool isFading;
    private bool isInArea;
    private bool Before_isInArea;


    void Start()
    {
        initDayAudioVolume = 22000;
    }

    private void Update()
    {
        if (me.ClosestPoint(Networking.LocalPlayer.GetPosition()) == Networking.LocalPlayer.GetPosition())
        {
            if (!isInArea)
            {
                isInArea = true;
                isFading = true;
            }
        }
        else
        {
            if (isInArea)
            {
                isInArea = false;
                isFading = true;
            }
        }
        if (!isFading)
            return;
    }

    private void LateUpdate()
    {
        if (!isFading)
            return;

        // 補正値フェード
        FadingValues();

        AudioPass.cutoffFrequency = (22000)-(21600*DayValue);
    }

    private void FadingValues()
    {
        if (isInArea)
        {
            if (DayValue <= 1.0f)
            {
                DayValue += Time.deltaTime / fadingTime;
                if (DayValue > 1.0f)
                {
                    DayValue = 1.0f;
                    isFading = false;
                }
            }
        }
        else
        {
            if (DayValue > 0)
            {
                DayValue -= Time.deltaTime / fadingTime;
                if (DayValue < 0)
                    DayValue = 0;
            }
        }
    }



    public void _StartFade()
    {
        isInArea = true;
        isFading = true;
    }

    public void _EndFade()
    {
        isInArea = false;
    }
}
