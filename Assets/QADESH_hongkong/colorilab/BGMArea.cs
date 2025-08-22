
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class BGMArea : UdonSharpBehaviour
{

    [SerializeField]
    private AudioSource DayAudio;
    [SerializeField]
    private AudioSource NightAudio;
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
        initDayAudioVolume = DayAudio.volume;
        initNightAudioVolume = NightAudio.volume;
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

        DayAudio.volume = initDayAudioVolume;
        NightAudio.volume = initNightAudioVolume;
    }

    private void LateUpdate()
    {
        if (!isFading)
            return;

        // 補正値フェード
        FadingValues();

        DayAudio.volume *= DayValue;
        NightAudio.volume *= NightValue;
    }

    private void FadingValues()
    {
        if (isInArea)
        {
            if (NightValue > 0)
            {
                NightValue -= Time.deltaTime / fadingTime;
                if (NightValue < 0)
                    NightValue = 0;
            }
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
            if (NightValue < 1.0f)
            {
                NightValue += Time.deltaTime / fadingTime;
                if (NightValue > 1.0f)
                    NightValue = 1.0f;
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
