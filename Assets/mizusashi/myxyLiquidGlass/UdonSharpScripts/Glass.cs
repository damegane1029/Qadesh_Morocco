
// Created by myxy.
// Redistribution is prohibited.
using Myxy.Glass.Udon;
using System.Linq;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace Myxy.Glass.Udon
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class Glass : UdonSharpBehaviour
    {
        private Texture2D shapeTex;
        private Material material;
        private Vector3 pPosition;
        private Quaternion pRotation;
        private Vector3 pendulumPosition;
        private Vector3 pPendulumPosition;
        private Vector3 pendulumVelocity;
        private Vector3 pPendulumVelocity;
        private Vector3 pendulumAccelerate;
        private Vector3 pPendulumAccelerate;
        private Vector3 smoothPendulumJerk = new Vector3(0,0,0);
        [SerializeField]
        private float pendulumMass = 1.0f;
        private float invPendulumMass;
        [SerializeField]
        private float pendulumViscosity = 1.0f;
        [SerializeField]
        private float pendulumSpring = 1.0f;
        [SerializeField]
        private float heightScalePendulumLength = 1.0f;
        private float[] radius;
        [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(VolumeSync))]
        private Vector4 volumeSync = Vector4.zero;
        public Vector4 VolumeSync
        {
            get
            {
                if (volumeSync.w > 0)
                {
                    return volumeSync;
                }
                else
                {
                    return originalVolume;
                }
            }
            set
            {
                volumeSync = value.w > 0 ? value : originalVolume;
                if (isVolumeSyncStart)
                {
                    isVolumeSyncStart = false;
                    volume = volumeSync.x;
                    if (volume <= 0)
                    {
                        worldHeight = -wholeHeight;
                    }
                    else
                    {
                        worldHeight = CalculateHeightSurface(volume);
                    }
                    surfaceVector = -gravity.normalized;
                    material.SetVector(glassMaterialSurfacePivot, V3ToV4(surfaceVector, worldHeight));
                }
            }
        }
        private Vector4 originalVolume;
        private bool isVolumeSyncStart = true;
        private float volume;
        private float surfaceHeightMomentum = 0.1f;
        private float bottom;
        private float worldHeight;
        public float WorldHeight => worldHeight;
        private Vector3 surfaceVector;
        public Vector3 SurfaceVector => surfaceVector;
        private float thickness;
        private float[] wradius;
        private float size;
        private int flamesMod = 0;
        private Vector3[] heightVector;
        private float wholeHeight;
        private float invWholeHeight;
        private float[] cutTable;
        private float[] sqrtTable;
        private float[] qurtTable;
        [SerializeField]
        private GameObject cap;


        [SerializeField]
        [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(IsOpen))]
        private bool isOpen = false;
        public bool IsOpen
        {
            get => isOpen;
            set
            {
                isOpen = value;
                SetOpenCap(isOpen);
            }
        }

        [SerializeField]
        private bool isPourable = true;

        [SerializeField]
        private float heightScaleFullOpenSpeed = 4.0f;

        private const int cutTableSize = 128;
        private const int shapeTextureSize = 32;
        private const float invSTSm1 = 1f/(shapeTextureSize-1);
        private const int sqrtTableSize = 256;
        private const int qurtTableSize = 256;
        private float wholeVolume;
        private float invWholeVolume;
        [SerializeField]
        [Range(0,2)]
        private float wholeVolumeScaleRecoveryVolumeSpeed = 0.5f;
        [SerializeField]
        [Range(0,2)]
        private float wholeVolumeScaleDrinkVolumeSpeed = 0.1f;

        private float maximumVolume;
        private float initialVolume;
        [SerializeField, HideInInspector]
        private Transform debugObject;
        private float dTime = 1/90f;
        private float invDTime = 90f;
        [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(LiquidColorSync))]
        private Color liquidColorSync;
        public Color LiquidColorSync
        {
            get
            {
                if (liquidColorSync != new Color(0,0,0,0))
                {
                    return liquidColorSync;
                }
                else
                {
                    return originalLiquidColor;
                }
            }
            set
            {
                liquidColorSync = value != new Color(0,0,0,0) ? value : originalLiquidColor;

                if (isLiquidColorSyncStart)
                {
                    isLiquidColorSyncStart = false;
                    liquidColor = liquidColorSync;
                    material.SetColor(glassMaterialLiquidColor, liquidColor);
                }
            }
        }
        private bool isLiquidColorSyncStart = true;
        private Color liquidColor;
        [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(AttributesSync))]
        private Vector4 attributesSync = Vector4.zero;
        public Vector4 AttributesSync
        {
            get
            {
                if (attributesSync.w > 0)
                {
                    return attributesSync;
                }
                else
                {
                    return originalAttributes;
                }
            }
            set
            {
                attributesSync = value.w > 0 ? value : originalAttributes;

                if (isAttributesSyncStart)
                {
                    isAttributesSyncStart = false;
                    attributes = attributesSync;
                    sparklingSize = attributes.x;
                    foamThickness = attributes.y;
                    waterReflectance = attributes.z;
                    material.SetFloat(glassMaterialSparklingSize, sparklingSize*sparklingAndFoamMultiplier);
                    material.SetFloat(glassMaterialFoamThickness, foamThickness*sparklingAndFoamMultiplier);
                    material.SetFloat(glassMaterialWaterReflectance, waterReflectance);
                }
            }
        }
        private bool isAttributesSyncStart = true;
        private Vector4 attributes;
        public Vector4 Attributes => attributes;
        public Color LiquidColor => liquidColor;
        private float actionCounter = 0;
        private const float stopThreshold = 1e-3f;

        [UdonSynced(UdonSyncMode.None)]
        private bool isDrinking = false;
        private Vector3 pivot;
        public Vector3 gravity = new Vector3(0,-10,0);
        private float worldHeightAim;
        private int onceInThisFrames = 8;
        private float sparklingSize;
        private float foamThickness;
        [SerializeField]
        [Range(0,1)]
        private float longPressInterval = 0.5f;
        private float longPressIntervalCounter = -1;
        private bool isPressed = false;
        private float longPressedCounter = 0;
        [SerializeField]
        private bool isStaticGlass = false;
        [SerializeField]
        private bool isRecoverable = true;
        [SerializeField]
        private bool isDisableSparklingAndFoamWhenClose = true;
        [UdonSynced(UdonSyncMode.None)]
        private bool isSparklingAndFoam = false;
        private float sparklingAndFoamMultiplier = 0;
        [SerializeField, HideInInspector]
        [Range(0,1)]
        private float sparklingAndFoamAppearFactor = 0.01f;
        [SerializeField]
        private bool isInfinite = false;
        // Stream
        [SerializeField]
        private SkinnedMeshRenderer streamObject;
        
        private Vector4[] streamPositions;
        private Vector3[] streamPositionsData;
        private Vector3[] streamVelocity;
        private float[] streamOriginalSpeedS;
        private Vector4[] streamUpDir;
        private float[] segmentVolume;
        private bool[] segmentVolumeResetFlag;                                
        private Material streamMaterial;
        private int zeroCount = 0;

        private float streamInitialRadius = 0.01f;
        private float initialArea;

        [SerializeField]
        private float actionCounterResetTime = 10.0f;
        private float worldHeightSmoothFactor = 0.8f;

        private const float qtau = Mathf.PI / 2;

        private float openRadius;
        private float openArea;
        private float invOpenArea;

        private const int step = 64;
        private Glass hitGlass = null;
        private float waterReflectance;
        [SerializeField, HideInInspector]
        private Text debugText;
        private float forbidSetSyncedVariableOnOwnershipTransferredCounter = -1.0f;
        private Vector3 inertia;
        public bool IsCloseOnExhaust = false;
        public bool IsInteractableByPickup = false;

        private const string streamMaterialStream = "_Stream";
        private const string streamMaterialStreamUp = "_StreamUp";
        private const string streamMaterialOpenRadius= "_OpenRadius";
        private const string streamMaterialSurfacePivot = "_SurfacePivot";
        private const string streamMaterialGlassCenter= "_GlassCenter";
        private const string streamMaterialAxis= "_Axis";
        private const string streamMaterialLiquidColor = "_WaterColor";
        private const string glassMaterialSurfacePivot = "_SurfacePivot";
        private const string glassMaterialLiquidColor = "_WaterColor";
        private const string glassMaterialSparklingSize = "_SparklingSize";
        private const string glassMaterialFoamThickness = "_FThi";
        private const string glassMaterialVolumeRatio = "_VolRatio";
        private const string glassMaterialJerk = "_Jerk";
        private const string glassMaterialGlassThickness = "_Thickness";
        private const string glassMaterialGlassRadi = "_Radius";
        private const string glassMaterialGlassBottom = "_Bottom";
        private const string methodSetActionCounter = "SetActionCounter";
        private const string glassMaterialWaterReflectance = "_WaterReflectance";
        private const string streamMaterialTargetSurfacePivot = "_TargetSurfacePivot";
        private const string glassMaterialInitialHeight = "_InitialHeight";
        private const string glassMaterialMaximumHeight = "_MaximumHeight";
        private const string streamMaterialIsDisplayStream = "_IsDisplayStream";
        private const string streamMaterialTargetCenter = "_TargetCenter";
        private const string streamMaterialHitSegment = "_HitSegment";
        private const string glassMaterialIsOpen = "_IsOpen";
        private const string glassMaterialShapeTex = "_ShapeTex";
        private const string glassMaterialCRCoef = "_CRCoef";
        private Color originalLiquidColor;
        private Vector4 originalAttributes;
 
        void Start()
        {
            material = GetComponent<MeshRenderer>().material;
            shapeTex = (Texture2D)material.GetTexture(glassMaterialShapeTex);
            radius = new float[shapeTextureSize];
            for (int i=0; i<shapeTextureSize; i++)
            {
                radius[i] = shapeTex.GetPixel(i,0).r;
            }
            material.SetFloatArray(glassMaterialGlassRadi, radius);
            bottom = material.GetFloat(glassMaterialGlassBottom);
            thickness = material.GetFloat(glassMaterialGlassThickness);
            invPendulumMass = 1/pendulumMass;

            originalLiquidColor = material.GetColor(glassMaterialLiquidColor);
            originalAttributes = new Vector4(
                material.GetFloat(glassMaterialSparklingSize),
                material.GetFloat(glassMaterialFoamThickness),
                material.GetFloat(glassMaterialWaterReflectance),
                1.0f
            );

            liquidColor = originalLiquidColor;
            sparklingSize = originalAttributes.x;
            foamThickness = originalAttributes.y;
            waterReflectance = originalAttributes.z;
            attributes = originalAttributes;

            wradius = new float[shapeTextureSize];
            for (int i=0; i<shapeTextureSize; i++)
            {
                wradius[i] = TransformLocalToWorldVector(new Vector3(radius[i]*0.5f-thickness,0,0)).magnitude;
            }
            openRadius = wradius[shapeTextureSize-1];
            openArea = openRadius * openRadius * Mathf.PI;
            invOpenArea = 1/openArea;

            size = TransformLocalToWorldVector(Vector3.one).magnitude;
            heightVector = new Vector3[shapeTextureSize-1];
            for (int i=0;i<shapeTextureSize-1;i++)
            {
                heightVector[i] = new Vector3();
            }
            UpdateHeightVector();
            var upS = TransformLocalToWorldVector(new Vector3(0,1,0));
            wholeHeight = upS.magnitude;
            invWholeHeight = 1/wholeHeight;
            cutTable = new float[cutTableSize+1];
            for (int i=0; i<cutTableSize+1; i++)
            {
                var r = (float)i/cutTableSize * 2.0f - 1.0f;
                cutTable[i] = Mathf.Asin(r) + r * Mathf.Sqrt(Mathf.Clamp01(1 - r*r));
            }
            sqrtTable = new float[sqrtTableSize+1];
            for (int i=0; i<sqrtTableSize+1; i++)
            {
                var x = (float)i/sqrtTableSize;
                sqrtTable[i] = Mathf.Sqrt(Mathf.Clamp01(x));
            }
            qurtTable = new float[qurtTableSize+1];
            for (int i=0; i<qurtTableSize+1; i++)
            {
                var x = (float)i/qurtTableSize;
                qurtTable[i] = Mathf.Sqrt(Mathf.Sqrt(Mathf.Clamp01(x)));
            }

            streamInitialRadius = wradius[shapeTextureSize-1];
            //initialSpeed = 0;
            initialArea = 0;
            
            wholeVolume = CalculateVolume(upS.normalized, wholeHeight*0.5f);
            invWholeVolume = 1/wholeVolume;
            surfaceVector = -gravity.normalized;

            //volume = wholeVolume * initialAmount;
            initialVolume = CalculateVolume(upS.normalized, (2*material.GetFloat(glassMaterialInitialHeight)-1)*wholeHeight*0.5f);
            originalVolume = new Vector4(initialVolume, 0, 0, 1);
            volume = initialVolume;
            maximumVolume = CalculateVolume(upS.normalized, (2*material.GetFloat(glassMaterialMaximumHeight)-1)*wholeHeight*0.5f);
            
            if (volume <= 0)
            {
                worldHeight = -wholeHeight;
            }
            else
            {
                worldHeight = CalculateHeightSurface(volume);
                uWHHeight = worldHeight;
            }

            material.SetVector(glassMaterialSurfacePivot, V3ToV4(surfaceVector, worldHeight));

            if (cap == null)
            {
                IsOpen = true;
            }
            else
            {
                IsOpen = !cap.activeSelf;
                //cap.SetActive(!IsOpen);
            }

            material.SetColor(glassMaterialLiquidColor, liquidColor);

            pendulumPosition = transform.position + surfaceVector * worldHeight + gravity.normalized * (heightScalePendulumLength * wholeHeight);
            pPendulumPosition = pendulumPosition;
            pendulumVelocity = Vector3.zero;
            pPendulumVelocity = pendulumVelocity;
            inertia = Vector3.zero;

            pivot = transform.position + surfaceVector*worldHeight;
            for (int i=0; i<64; i++)
            {
                pivot = 0.9f * pivot + 0.1f * (transform.position + surfaceVector*worldHeight);

                pPendulumPosition = pendulumPosition;
                pPendulumVelocity = pendulumVelocity;
                pPendulumAccelerate = pendulumAccelerate;

                Vector3 gravityForce = gravity*pendulumMass;
                Vector3 springDir = pivot - pendulumPosition;
                Vector3 springForce = (springDir - springDir.normalized*(heightScalePendulumLength*wholeHeight))*pendulumSpring;
                Vector3 viscousForce = -pPendulumVelocity*pendulumViscosity;
                pendulumVelocity = pPendulumVelocity + (gravityForce+springForce+viscousForce)*invPendulumMass*dTime;       
                pendulumPosition = pPendulumPosition + pendulumVelocity*dTime;
                pendulumAccelerate = (pendulumVelocity - pPendulumVelocity)*invDTime;
                Vector3 jerk = (pendulumAccelerate - pPendulumAccelerate)*invDTime;
                smoothPendulumJerk = 0.9f * smoothPendulumJerk + 0.1f * jerk;
                //worldHeight = worldHeight*surfaceHeightMomentum + CalculateHeight(surfaceVector, volume)*(1-surfaceHeightMomentum);
            }
            isSparklingAndFoam = IsOpen || !isDisableSparklingAndFoamWhenClose;
            sparklingAndFoamMultiplier = isSparklingAndFoam ? 1 : 0;
            //sparklingAndFoamMultiplier *= isOpen ? 1 : 0;
            material.SetFloat(glassMaterialSparklingSize, sparklingSize*sparklingAndFoamMultiplier);
            material.SetFloat(glassMaterialFoamThickness, foamThickness*sparklingAndFoamMultiplier);
            worldHeightAim = worldHeight;


            //smoothAbsPivotSpeed = 0;
            //smoothAbsVolumeSpeed = 0;
            actionCounter = 0;
            smoothPendulumJerk = new Vector3(0,0,0);
            flamesMod = Random.Range(0, onceInThisFrames);
            pPosition = transform.position;
            pRotation = transform.rotation;

            // Stream
            streamPositions = new Vector4[step];
            streamUpDir = new Vector4[step];
            streamPositionsData = new Vector3[step];
            streamVelocity = new Vector3[step];
            streamOriginalSpeedS = new float[step];
            segmentVolume = new float[step];
            segmentVolumeResetFlag = new bool[step];
            var origin = transform.localToWorldMatrix.MultiplyPoint3x4(new Vector3(0,0.5f,0));
            for (int i=0; i<step; i++)
            {
                segmentVolumeResetFlag[i] = false;
                streamPositionsData[i] = origin;
            }
            if (streamObject != null)
            {
                streamMaterial = streamObject.material;
                streamMaterial.SetFloat(streamMaterialIsDisplayStream, material.GetFloat(streamMaterialIsDisplayStream));
            }

            Vector4[] crcoef = new Vector4[shapeTextureSize-1];
            for (int i=0; i<shapeTextureSize-1; i++)
            {
                float y_1 = i > 0 ? radius[i-1] : radius[0];
                float y0 = radius[i];
                float y1 = radius[i+1];
                float y2 = i < shapeTextureSize-2 ? radius[i+2] : radius[shapeTextureSize-1];
                
                crcoef[i] = new Vector4(
                    y0,
                    -0.5f*y_1+0.5f*y1,
                    y_1-2.5f*y0+2.0f*y1-0.5f*y2,
                    -0.5f*y_1+1.5f*y0-1.5f*y1+0.5f*y2
                );
            }
            material.SetVectorArray(glassMaterialCRCoef, crcoef); 

            if (Networking.IsOwner(this.gameObject))
            {
                LiquidColorSync = liquidColor;
                AttributesSync = attributes;
                VolumeSync = new Vector4(volume, 0, 0, 1);
                RequestSerialization();
            }
       }

        void Update()
        {
            if (debugObject != null)
            {
                debugObject.GetComponent<MeshRenderer>().material.SetColor("_Color", liquidColorSync);
                debugObject.transform.position = pendulumPosition;
            }
            if (debugText != null)
            {
                debugText.text = string.Format(
                    "Owner:{0}\nIsOwner:{1}\nworlkHeight:{2}",
                    Networking.GetOwner(this.gameObject).displayName,
                    Networking.IsOwner(Networking.LocalPlayer, this.gameObject),
                    worldHeight
                );
            }

            if (isStaticGlass)
            {
                return;
            }
#if UNITY_EDITOR
            if (transform.position != pPosition || transform.rotation != pRotation)
#else
            if ((transform.position != pPosition || transform.rotation != pRotation) && actionCounter > stopThreshold)
#endif
            {
                SetActionCounter();
            }
            pPosition = transform.position;
            pRotation = transform.rotation;

            if (isPressed)
            {
                if (longPressIntervalCounter > 0)
                {
                    longPressIntervalCounter -= Time.deltaTime;
                }
                else
                {
                    OnLongPressed();
                }
            }
            else
            {
                if (longPressIntervalCounter > 0)
                {
                    OnPressed();
                    longPressIntervalCounter = -1;
                }
            }

            if (forbidSetSyncedVariableOnOwnershipTransferredCounter > 0)
            {
                forbidSetSyncedVariableOnOwnershipTransferredCounter -= Time.deltaTime;
            }

            if (actionCounter > 0)
            {
                RequestSerialization();
                dTime = 0.9f * dTime + 0.1f * Time.deltaTime;
                actionCounter -= dTime;
                pivot = 0.9f * pivot + 0.1f * (transform.position + surfaceVector*worldHeight);
                longPressedCounter = 0.9f * longPressedCounter;
                invDTime = 1/dTime;

                isSparklingAndFoam = IsOpen || !isDisableSparklingAndFoamWhenClose;
                sparklingAndFoamMultiplier = (1-sparklingAndFoamAppearFactor) * sparklingAndFoamMultiplier + (isSparklingAndFoam ? sparklingAndFoamAppearFactor : 0);

                if (longPressedCounter <= stopThreshold && Networking.IsOwner(Networking.LocalPlayer, this.gameObject))
                {
                    isDrinking = false;
                }

                pPendulumPosition = pendulumPosition;
                pPendulumVelocity = pendulumVelocity;
                pPendulumAccelerate = pendulumAccelerate;

                float colorAttributesVolumeSmoothFactor = 0.9f;
                liquidColor = colorAttributesVolumeSmoothFactor * liquidColor + (1-colorAttributesVolumeSmoothFactor) * LiquidColorSync;
                volume = colorAttributesVolumeSmoothFactor * volume + (1-colorAttributesVolumeSmoothFactor) * VolumeSync.x;
                attributes = colorAttributesVolumeSmoothFactor * attributes + (1-colorAttributesVolumeSmoothFactor) * AttributesSync;
                sparklingSize = attributes.x;
                foamThickness = attributes.y;
                waterReflectance = attributes.z;
                material.SetColor(glassMaterialLiquidColor, liquidColor);
                material.SetFloat(glassMaterialSparklingSize, sparklingSize*sparklingAndFoamMultiplier);
                material.SetFloat(glassMaterialFoamThickness, foamThickness*sparklingAndFoamMultiplier);
                material.SetFloat(glassMaterialWaterReflectance, waterReflectance);
                material.SetFloat(glassMaterialVolumeRatio, volume*invWholeVolume);

                var velocity = (transform.position - pPosition) * invDTime;
                inertia = 0.5f * inertia + 0.5f * velocity;

                Vector3 springDir = pivot - pendulumPosition;

                pendulumVelocity = pPendulumVelocity + (
                    gravity*pendulumMass+
                    (springDir - springDir.normalized*(heightScalePendulumLength*wholeHeight))*pendulumSpring+
                    (-pPendulumVelocity*pendulumViscosity)
                )*invPendulumMass*dTime;       
                pendulumPosition = pPendulumPosition + pendulumVelocity * dTime;
                pendulumAccelerate = (pendulumVelocity - pPendulumVelocity) * invDTime;
                smoothPendulumJerk = 0.1f * smoothPendulumJerk + 0.9f * (pendulumAccelerate - pPendulumAccelerate) * invDTime;

                surfaceVector = (pivot-pendulumPosition).normalized;
                material.SetVector(glassMaterialJerk, smoothPendulumJerk*Mathf.Clamp(actionCounter,0,1));

                UpdateWorldHeightAim();
                worldHeight = worldHeightSmoothFactor * worldHeight + (1-worldHeightSmoothFactor) * worldHeightAim;


                material.SetVector(glassMaterialSurfacePivot, V3ToV4(surfaceVector, worldHeight));

                float dist = CalculateDistSurface(worldHeight, TransformLocalToWorldVector(new Vector3(0,0.5f,0)));
                float wr = openRadius;
                if (!IsOpen || dist < -wr || isDrinking)
                {
                    initialArea = 0;
                }
                else
                {
                    initialArea = (CutByTable(dist/wr) + qtau) * wr * wr;
                }
 
                if (!IsOpen && cap != null && volume < maximumVolume && isRecoverable)
                {
                    AddVolume(wholeVolumeScaleRecoveryVolumeSpeed*wholeVolume*dTime);
                }

                if (IsCloseOnExhaust && volume < 0 && IsOpen)
                {
                    IsOpen = false;
                }
            }
            // Stream
            if (initialArea > 0)
            {
                zeroCount = step;
            }

            if ((zeroCount > 0 && actionCounter > stopThreshold) && streamMaterial != null)
            {
                zeroCount--;
                for (int i=step-1; i>0; i--)
                {
                    streamVelocity[i] = streamVelocity[i-1] + gravity*dTime;
                    streamPositionsData[i] = streamPositionsData[i-1] + streamVelocity[i-1] * dTime;
                    streamOriginalSpeedS[i] = streamOriginalSpeedS[i-1];
                    segmentVolume[i] = segmentVolumeResetFlag[i-1] ? 0.0f : segmentVolume[i-1];

                }
                for (int i=0; i<step; i++)
                {
                    segmentVolumeResetFlag[i] = false;
                }
                streamPositionsData[0] = transform.localToWorldMatrix.MultiplyPoint3x4(new Vector3(0,0.5f,0));
                Vector3 initialAxis = transform.up;
                float dtg = Vector3.Dot(transform.up, gravity);
                Vector3 initialStreamDir = Vector3.Cross(Vector3.Cross(surfaceVector, transform.up), surfaceVector).normalized;
                initialStreamDir = dtg > 0 ? initialAxis : initialStreamDir;
                var initialSpeed = (heightScaleFullOpenSpeed*wholeHeight)*initialArea * invOpenArea;
                initialSpeed = Mathf.Max(initialSpeed + Vector3.Dot(initialStreamDir, inertia), 0);
                streamVelocity[0] = initialStreamDir*initialSpeed;
                streamOriginalSpeedS[0] = streamVelocity[0].sqrMagnitude;
                segmentVolume[0] = initialArea*initialSpeed*dTime;
                if (initialArea > 0 && !isInfinite)
                {
                    AddVolume(-segmentVolume[0]);
                }


                var p = transform.position;
                streamUpDir[0] = transform.right.normalized;
                for (int i=0; i<step; i++)
                {
                    var sV = streamVelocity[i].sqrMagnitude;
                    streamPositions[i] = V3ToV4(
                        streamPositionsData[i],
                        sV > 0 && segmentVolume[i] > 0 ?
                        Mathf.Max(QurtByTable(streamOriginalSpeedS[i]/sV)*streamInitialRadius,1e-3f) :
                        0    
                    );

                    if (i > 0)
                    {
                        var axis = ((Vector3)(streamPositions[i < step-1 ? i+1 : i] - streamPositions[i-1]))*1e2f*wholeHeight;
                        streamUpDir[i] = Vector3.Cross(Vector3.Cross(axis,streamUpDir[i-1]),axis).normalized;
                    }
                }

                hitGlass = null;
                for (int i=0; i<step-1; i++)
                {
                    if (segmentVolume[i] != 0)
                    {
                        var ro = streamPositionsData[i];
                        var re = streamPositionsData[i+1];
                        var rds = re - ro;
                        RaycastHit hit;
                        if (Physics.Raycast(ro, rds, out hit, rds.magnitude, ~0))
                        {
                            GlassPickup hitGlassPickupCandidate = hit.collider == null ? null : hit.collider.GetComponent<GlassPickup>();
                            if (hitGlassPickupCandidate != null)
                            {
                                var hitGlassCandidate = hitGlassPickupCandidate.GetGlass();
                                if (hitGlassCandidate != this)
                                {
                                    hitGlass = hitGlassCandidate;
                                    break;
                                }
                            }
                            segmentVolume[i] = 0;
                            break;
                        }
                    }
                }
                
                if (hitGlass != null)
                {
                    if (
                        Networking.IsOwner(Networking.LocalPlayer, this.gameObject) &&
                        !Networking.IsOwner(Networking.LocalPlayer, hitGlass.gameObject)
                    )
                    {
                        Networking.SetOwner(Networking.LocalPlayer, hitGlass.gameObject);
                    }
                    var targetSurfaceVector = hitGlass.SurfaceVector;
                    var targetWorldHeight = hitGlass.WorldHeight;
                    streamMaterial.SetVector(streamMaterialTargetSurfacePivot, V3ToV4(
                        targetSurfaceVector,
                        targetWorldHeight
                    ));
                    streamMaterial.SetVector(streamMaterialTargetCenter, hitGlass.transform.position);
 
                    Vector3 targetCenter = hitGlass.transform.position;
                    streamMaterial.SetInt(streamMaterialHitSegment, step);
                    for (int i=0; i<step-1; i++)
                    {
                        if (segmentVolume[i] != 0)
                        {
                            streamMaterial.SetInt(streamMaterialHitSegment, i);
                            var ro = streamPositionsData[i];
                            var re = streamPositionsData[i+1];
                            if (
                                Vector3.Dot(re - targetCenter, targetSurfaceVector) - targetWorldHeight < 0
                            )
                            {
                                if (Vector3.Dot(ro - targetCenter, targetSurfaceVector) - targetWorldHeight > 0)
                                {
                                    hitGlass.PourWithAttributes(segmentVolume[i], liquidColor, attributes);
                                }
                                segmentVolumeResetFlag[i] = true;
                            }
                        }
                    }
                }
                else
                {
                    streamMaterial.SetVector(streamMaterialTargetSurfacePivot, V3ToV4(
                        -gravity.normalized,
                        -1000
                    ));
                }
            
                streamMaterial.SetVectorArray(streamMaterialStream, streamPositions); 
                streamMaterial.SetVectorArray(streamMaterialStreamUp, streamUpDir); 
                streamMaterial.SetFloat(streamMaterialOpenRadius, streamInitialRadius);
                streamMaterial.SetVector(streamMaterialSurfacePivot, V3ToV4(surfaceVector, worldHeight));
                streamMaterial.SetVector(streamMaterialGlassCenter, transform.position);
                streamMaterial.SetVector(streamMaterialAxis, initialAxis);
                streamMaterial.SetColor(streamMaterialLiquidColor, liquidColor);
                streamMaterial.SetFloat(glassMaterialWaterReflectance, waterReflectance);
            }
 
        }

        private void UpdateHeightVector()
        {
            var upS = TransformLocalToWorldVector(new Vector3(0,1,0));
            var upS1 = upS * invSTSm1; 
            heightVector[0] = 0.5f * (upS1 - upS);
            for (int i=1;i<shapeTextureSize-1;i++)
            {
                heightVector[i] = heightVector[i-1] + upS1;
            }
        }

        public void AddVolume(float addVolume)
        {
            if (
                (addVolume > 0 && volume > maximumVolume) ||
                (addVolume < 0 && volume <= 0)
            )
            {
                return;
            }
            SetActionCounter();
            if (volume <= 0 && addVolume > 0)
            {
                worldHeightAim = CalculateHeightSurface(0);
                worldHeight = worldHeightAim;
                volume = 0;
            }
            if (addVolume > 0)
            {
                addVolume = Mathf.Min(addVolume, maximumVolume - volume);
            }
            volume += addVolume;
            if (Networking.IsOwner(Networking.LocalPlayer, this.gameObject))
            {
                VolumeSync = new Vector4(volume, 0, 0, 1);
                RequestSerialization();
            }
        }

        public void PourWithAttributes(float addVolume, Color color, Vector4 attributes)
        {
            if (!isPourable || !IsOpen)
            {
                return;
            }
            SetActionCounter();
            if (volume > maximumVolume)
            {
                this.liquidColor = volume < 0 ? color : (this.liquidColor * (volume-addVolume) + color * addVolume)/volume;
                this.attributes = volume < 0 ? attributes : (this.attributes * (volume-addVolume) + attributes * addVolume)/volume;
            }
            else
            {
                this.liquidColor = volume < 0 ? color : (this.liquidColor * volume + color * addVolume)/(volume+addVolume);
                this.attributes = volume < 0 ? attributes : (this.attributes * volume + attributes * addVolume)/(volume+addVolume);
            }
            sparklingSize = this.attributes.x;
            foamThickness = this.attributes.y;
            waterReflectance = this.attributes.z;
            if (Networking.IsOwner(Networking.LocalPlayer, this.gameObject))
            {
                LiquidColorSync = this.liquidColor;
                AttributesSync = this.attributes;
                RequestSerialization();
            }
            AddVolume(addVolume);
        }
        private float CalculateHeight(Vector3 normalizeWorldSpaceSurfaceVector, float volume)
        {
            float upperHeight = size;
            float lowerHeight = -size;
            float height = 0;
            for (int i=0; i<2; i++)
            {
                float calcVolume = CalculateVolume(normalizeWorldSpaceSurfaceVector, height);
                if (calcVolume > volume)
                {
                    upperHeight = height;
                }
                else
                {
                    lowerHeight = height;
                }
                height = 0.5f * (upperHeight + lowerHeight);
            }
            return height;
        }

        private float CalculateHeightSurface(float volume)
        {
            float upperHeight = size;
            float lowerHeight = -size;
            float height = 0;
            float volumeNormalized = volume * (shapeTextureSize-1) * invWholeHeight;
            float wholeVolumeNormalized = wholeVolume * (shapeTextureSize-1) * invWholeHeight;
            for (int i=0; i<8; i++)
            {
                float volumeCandidate = 0;
                float vacuoleCandidate = 0;
                for (int j=0; j<shapeTextureSize-1; j++)
                {
                    float dist;
                    float absHeightS = heightVector[j].sqrMagnitude;
                    float dotnh = Vector3.Dot(surfaceVector, heightVector[j]);
                    float sinaByHeightS = absHeightS - dotnh*dotnh;
                    if (sinaByHeightS < 1e-6f)
                    {
                        dist = Mathf.Sign(height - dotnh)*1e6f;
                    }
                    else
                    {
                        dist = (height - dotnh) * Mathf.Sqrt(absHeightS / sinaByHeightS);
                    }

                    float wr = (wradius[j] + wradius[j+1])*0.5f;
                    if ((j+.5)*invSTSm1 > bottom)
                    {
                        var aaa = CutByTable(dist/wr) * wr * wr;
                        var bbb = qtau * wr * wr;
                        volumeCandidate += bbb + aaa;
                        vacuoleCandidate += bbb - aaa;
                    }
                    if (volumeCandidate > volumeNormalized)
                    {
                        upperHeight = height;
                        break;
                    }
                    if (vacuoleCandidate > wholeVolumeNormalized - volumeNormalized)
                    {
                        lowerHeight = height;
                        break;
                    }
                }
                height = 0.5f * (upperHeight + lowerHeight);
            }
            return height;
        }

        private float uWHUpperHeight;
        private float uWHLowerHeight;
        private float uWHHeight;
        private float uWHVolume;
        private void UpdateWorldHeightAim()
        {
            float volumeNormalized = volume * (shapeTextureSize-1) * invWholeHeight;
            float wholeVolumeNormalized = wholeVolume * (shapeTextureSize-1) * invWholeHeight;

            float cosa = Vector3.Dot(surfaceVector, transform.up);
            float sina = SqrtByTable(1 - cosa*cosa);
            float invsina = sina > 1e-6f ? 1/sina : 1e6f;
 
            for (int i=0; i<1; i++)
            {
                if (flamesMod == 0)
                {
                    worldHeightAim = volume > 0 ? uWHHeight : -wholeHeight;
                    UpdateHeightVector();
                    uWHUpperHeight = size;
                    uWHLowerHeight = -size;
                    uWHVolume = volume;
                }
                float volumeCandidate = 0;
                float vacuoleCandidate = 0;
                float h = wholeHeight * 0.5f;
                for (int j = shapeTextureSize-1-1; j >= 0; j-=2)
                {
                    float dist = invsina * (uWHHeight - h * cosa);
                    h -= wholeHeight * invSTSm1 * 2;

                    float wr = (wradius[j] + wradius[j+1])*0.5f;
                    var aaa = CutByTable(dist/wr) * wr * wr * 2;
                    var bbb = qtau * wr * wr * 2;
                    volumeCandidate += bbb + aaa;
                    vacuoleCandidate += bbb - aaa;
                    if (volumeCandidate > volumeNormalized)
                    {
                        uWHUpperHeight = uWHHeight;
                        break;
                    }
                    if (vacuoleCandidate > wholeVolumeNormalized - volumeNormalized)
                    {
                        uWHLowerHeight = uWHHeight;
                        break;
                    }
                }
                uWHHeight = 0.5f * (uWHUpperHeight + uWHLowerHeight);
                flamesMod = (flamesMod + 1) % onceInThisFrames;
            }
        }


        private Vector3 TransformVector(Matrix4x4 matrix, Vector3 vector)
        {
            return matrix.MultiplyPoint3x4(vector)-new Vector3(matrix.m03, matrix.m13, matrix.m23);
        }

        private Vector3 TransformLocalToWorldVector(Vector3 vector)
        {
            return transform.localToWorldMatrix.MultiplyPoint3x4(vector)-transform.position;
        }

        private float CutByTable(float x)
        {
            return cutTable[(int)(Mathf.Clamp01(x*0.5f+0.5f)*cutTableSize)];
        }
        private float SqrtByTable(float x)
        {
            return sqrtTable[(int)(Mathf.Clamp01(x)*sqrtTableSize)];
        }
        private float QurtByTable(float x)
        {
            return qurtTable[(int)(Mathf.Clamp01(x)*qurtTableSize)];
        }

        private float CalculateVolume(Vector3 normalizedWorldSpaceSurfaceVector, float worldHeight)
        {

            float area;
            float volume = 0;
            for (int i=0; i<shapeTextureSize-1; i++)
            {
                float dist = CalculateDist(normalizedWorldSpaceSurfaceVector, worldHeight, heightVector[i]);
                float wr = (wradius[i] + wradius[i+1])*0.5f;
                if ((i+.5)*invSTSm1 > bottom)
                {
                    if (dist < -wr)
                    {
                        area = 0;
                    }
                    else if (dist > wr)
                    {
                        area = Mathf.PI * wr * wr;
                    }
                    else
                    {
                        area = (CutByTable(dist/wr) + qtau) * wr * wr;
                    }
                    volume += area;
                }
            }
            return volume * wholeHeight * invSTSm1;
        }

        private float CalculateDist(Vector3 n, float d, Vector3 h)
        {
            float absHeightS = h.sqrMagnitude;
            float dotnh = Vector3.Dot(n, h);
            float sinaByHeightS = absHeightS - dotnh*dotnh;
            if (sinaByHeightS < 1e-8f)
            {
                return Mathf.Sign(d - dotnh)*1e8f;
            }
            return (d - dotnh) * Mathf.Sqrt(absHeightS / sinaByHeightS);
        }
        private float CalculateDistSurface(float d, Vector3 h)
        {
            float absHeightS = h.sqrMagnitude;
            float dotnh = Vector3.Dot(surfaceVector, h);
            float sinaByHeightS = absHeightS - dotnh*dotnh;
            if (sinaByHeightS < 1e-8f)
            {
                return Mathf.Sign(d - dotnh)*1e8f;
            }
            return (d - dotnh) * Mathf.Sqrt(absHeightS / sinaByHeightS);
        }


        public override void OnPickupUseDown()
        {
            isPressed = true;
            longPressIntervalCounter = longPressInterval;
            SetOwner();
        }
        public override void OnPickupUseUp()
        {
            isPressed = false;
            SetOwner();
        }

        private void OnPressed()
        {
            SetActionCounter();
            if (cap != null)
            {
                IsOpen = !IsOpen;
                SetOpenCap(IsOpen);
            }
        }

        private void OnLongPressed()
        {
            SetActionCounter();
            longPressedCounter = 1;
            if (IsOpen && volume > 0)
            {
                isDrinking = true;
                AddVolume(-wholeVolumeScaleDrinkVolumeSpeed*wholeVolume*dTime);
            }
        }

        public override void OnPickup()
        {
            SetOwner();
            SetActionCounter();
        }

        public void SetOwner()
        {
            Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
        }

        private void SetOpenCap(bool isOpen)
        {
            if (cap == null) return;
            SetActionCounter();
            cap.SetActive(!isOpen);
            material.SetFloat(glassMaterialIsOpen, isOpen ? 1 : 0);
        }
        public void SetActionCounter()
        {
            //if (actionCounter > stopThreshold)
            if (actionCounter > actionCounterResetTime)
            {
                return;
            }
            actionCounter = actionCounterResetTime*2;
            if (Networking.IsOwner(Networking.LocalPlayer, this.gameObject))
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, methodSetActionCounter);
            }
        }

        public override void OnOwnershipTransferred(VRCPlayerApi player)
        {
            if (!Networking.IsOwner(Networking.LocalPlayer, this.gameObject))
            {
                forbidSetSyncedVariableOnOwnershipTransferredCounter = 8.0f;
            }
        }

        Vector4 V3ToV4(Vector3 xyz, float w)
        {
            return new Vector4(xyz.x, xyz.y, xyz.z, w);
        }
    }
}
