
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

    public enum FadeEventState : byte
    {
        FadeIn = 0,
        Blackout = 1,
        FadeOut = 2
    }

    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class FadeEvent : UdonSharpBehaviour
    {
        [Header("コールバックを受け取るUdon")] [SerializeField]
        private QadeshCommonTeleporter _targetUdonBehaviour;

        private string _onEndFadeInMessageEvent= "OnEndFadeInEvent";
        private string _onEndFadeOutMessageEvent= "OnEndFadeOutEvent";

        [Header("暗転時の画像。指定しなければ黒一色になります")] [SerializeField]
        private Texture2D _texture;

        [Header("Fade Time Customize")] [SerializeField]
        private float _fadeTime = 1f;

        [SerializeField] private float _blackOutTime = 0.2f;

        [Header("Fade Sound Clips")] [SerializeField]
        private MeshRenderer _meshRenderer;

        [SerializeField] private Transform _rendererTransform;

        private VRCPlayerApi _localPlayer;
        private Color _backgroundColor;
        private bool _isInitialized;
        private FadeEventState _state;
        private float _fadingTimeCount;
        private float _fadeoutTimeCount;
        private float _alphaFadingRange;

        public bool IsFading { get; private set; }

        private void _Initialize()
        {
            if (_isInitialized)
                return;

            _isInitialized = true;

            if (_texture != null)
                _meshRenderer.material.SetTexture("_ClipTex", _texture);

            _localPlayer = Networking.LocalPlayer;
            _backgroundColor = _meshRenderer.material.GetColor("_BackgroundColor");
        }

        public void _StartFade()
        {
            _Initialize();
            _meshRenderer.enabled = true;

            var headTrack = _localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
            _rendererTransform.position = headTrack.position + headTrack.rotation * Vector3.forward;
            _rendererTransform.rotation = headTrack.rotation;

            _meshRenderer.material.SetColor("_BackgroundColor",
                new Color(_backgroundColor.r, _backgroundColor.g, _backgroundColor.b, 0));
            if (_texture != null)
            {
                _meshRenderer.material.SetFloat("_Scale", 2.0f);
                _alphaFadingRange = 2.0f;
            }
            else
            {
                _alphaFadingRange = 1.0f;
            }

            _fadingTimeCount = 0;
            _fadeoutTimeCount = 0;
            
            // フェード開始
            IsFading = true;
            _state = FadeEventState.FadeIn;
            
        }

        public override void PostLateUpdate()
        {
            if (!IsFading)
                return;

            var headTrack = _localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);
            _rendererTransform.position = headTrack.position + headTrack.rotation * Vector3.forward;
            _rendererTransform.rotation = headTrack.rotation;

            if (_state == FadeEventState.Blackout)
            {
                _fadeoutTimeCount += Time.deltaTime;
                if (_fadeoutTimeCount > _blackOutTime)
                {
                    _state = FadeEventState.FadeOut;
                    _fadingTimeCount = 0;
                }

                return;
            }

            _fadingTimeCount += Time.deltaTime;

            var alpha = _fadingTimeCount / _fadeTime * _alphaFadingRange;
            var scale = 2.0f - _fadingTimeCount / _fadeTime * 2.0f;

            if (_state == FadeEventState.FadeOut)
            {
                alpha = _alphaFadingRange - alpha;
                scale = 2.0f - scale;
            }

            alpha = Mathf.Clamp(alpha, 0, 1.0f);
            scale = Mathf.Clamp(scale, 0, 2.0f);

            _meshRenderer.material.SetColor("_BackgroundColor",
                new Color(_backgroundColor.r, _backgroundColor.g, _backgroundColor.b, alpha));
            if (_texture != null)
                _meshRenderer.material.SetFloat("_Scale", scale);

            if (_fadingTimeCount >= _fadeTime)
            {
                if (_state == FadeEventState.FadeIn)
                {
                    SendCustomEventToTargetUdon(_onEndFadeInMessageEvent);
                    _state = FadeEventState.Blackout;
                }
                else
                {
                    SendCustomEventToTargetUdon(_onEndFadeOutMessageEvent);
                    _meshRenderer.enabled = false;
                    _state = FadeEventState.FadeIn;
                    IsFading = false;
                }
            }
        }

        private void SendCustomEventToTargetUdon(string message)
        {
            if (string.IsNullOrEmpty(message) || _targetUdonBehaviour == null) return;
            _targetUdonBehaviour.SendCustomEvent(message);
        }
    }