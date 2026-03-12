Shader "TriForge/Fantasy Worlds/FWTreeLeaf_Optimized"
{
Properties
{
_BaseColor("Base Color", 2D) = "white" {}
_NormalMap("Normal Map", 2D) = "bump" {}
_NormalScale("Normal Scale", Float) = 1

_ShadowStrength("Shadow Strength", Range(0,1)) = 0.6
_ShadowSoftness("Shadow Softness", Range(0,1)) = 0.5
_AmbientStrength("Ambient Strength", Range(0,1)) = 0.5

_Color("Color", Color) = (1,1,1,1)
_Undercolor("Undercolor", Color) = (1,1,1,1)
_UndercolorAmount("Undercolor Amount", Range(0,1)) = 0.5

_Smoothness("Smoothness", Range(0,1)) = 0
_AOIntensity("AO Intensity", Range(0,1)) = 1

_MaskClip("Mask Clip", Range(0,1)) = 0.5

// WIND
_MainWindStrength("Main Wind Strength", Range(0,2)) = 0.5
_MainWindScale("Main Wind Scale", Range(0,2)) = 1
_LeafFlutterStrength("Leaf Flutter Strength", Range(0,2)) = 0.3
_WindOverallStrength("Wind Overall Strength", Range(0,2)) = 1

_BendingMaskStrength1("Bending Mask Strength", Range(0.05,2)) = 1

_VertexAOIntensity("Vertex AO Intensity", Range(0,1)) = 1
_BaseColorSaturation("Base Color Saturation", Range(0,1)) = 1
}

SubShader
{
Tags
{
"RenderPipeline"="UniversalPipeline"
"Queue"="AlphaTest"
"RenderType"="TransparentCutout"
}

LOD 200
Cull Off

Pass
{
Name "Forward"
Tags{ "LightMode"="UniversalForward" }

HLSLPROGRAM

#pragma vertex vert
#pragma fragment frag

#pragma multi_compile_instancing
#pragma multi_compile _ _MAIN_LIGHT_SHADOWS

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

TEXTURE2D(_BaseColor);
SAMPLER(sampler_BaseColor);

TEXTURE2D(_NormalMap);
SAMPLER(sampler_NormalMap);

CBUFFER_START(UnityPerMaterial)

float _ShadowStrength;
float _ShadowSoftness;
float _AmbientStrength;

float4 _Color;
float4 _Undercolor;
float _UndercolorAmount;

float _Smoothness;
float _AOIntensity;

float _MaskClip;

float _MainWindStrength;
float _MainWindScale;
float _LeafFlutterStrength;
float _WindOverallStrength;

float _BendingMaskStrength1;

float _VertexAOIntensity;
float _BaseColorSaturation;

float _NormalScale;

CBUFFER_END


struct Attributes
{
float4 positionOS : POSITION;
float3 normalOS : NORMAL;
float4 tangentOS : TANGENT;
float2 uv : TEXCOORD0;
float4 color : COLOR;

UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
float4 positionCS : SV_POSITION;
float2 uv : TEXCOORD0;
float3 normalWS : TEXCOORD1;
float3 posWS : TEXCOORD2;
float4 color : COLOR;

UNITY_VERTEX_INPUT_INSTANCE_ID
};


float hash21(float2 p)
{
p = frac(p*float2(123.34,345.45));
p += dot(p,p+34.23);
return frac(p.x*p.y);
}


Varyings vert (Attributes v)
{
Varyings o;

UNITY_SETUP_INSTANCE_ID(v);
UNITY_TRANSFER_INSTANCE_ID(v,o);

float3 worldPos = TransformObjectToWorld(v.positionOS.xyz);

// instance offset (important for batching wind variation)
float3 objectPos = unity_ObjectToWorld._m03_m13_m23;

float time = _Time.y;

// main wind
float2 windUV = (worldPos.xz + objectPos.xz) * _MainWindScale * 0.1;

float wind =
sin(windUV.x + time) +
cos(windUV.y + time*0.8);

wind *= 0.5;

// leaf flutter
float flutter =
sin(time * 6 + worldPos.x * 5 + worldPos.y * 3);

flutter *= _LeafFlutterStrength;

// height mask
float heightMask = saturate(v.positionOS.y * _BendingMaskStrength1);

// final wind
float windOffset =
(wind * _MainWindStrength +
flutter) *
heightMask *
_WindOverallStrength;

// apply wind
worldPos.x += windOffset;
worldPos.z += windOffset;

o.positionCS = TransformWorldToHClip(worldPos);

o.uv = v.uv;
o.posWS = worldPos;

o.normalWS = TransformObjectToWorldNormal(v.normalOS);

o.color = v.color;

return o;
}


half4 frag (Varyings i) : SV_Target
{
UNITY_SETUP_INSTANCE_ID(i);

float2 uv = i.uv;

float4 baseTex = SAMPLE_TEXTURE2D(_BaseColor, sampler_BaseColor, uv);

clip(baseTex.a - _MaskClip);

float3 normalTS = UnpackNormalScale(
SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, uv),
_NormalScale);

float3 normalWS = normalize(i.normalWS);

Light mainLight = GetMainLight();

float NdotL = dot(normalWS, mainLight.direction);

// делаем м€гкий переход света
float lightTerm = saturate(NdotL * (1 - _ShadowSoftness) + _ShadowSoftness);

// добавл€ем ambient
float3 ambient = baseTex.rgb * _AmbientStrength;

// финальное освещение
float3 lighting =
(baseTex.rgb * mainLight.color * lightTerm * _ShadowStrength)
+ ambient;

float3 col =
baseTex.rgb *
_Color.rgb *
lighting;

return float4(col,1);
}

ENDHLSL
}

}
}