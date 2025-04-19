Shader "Custom/JellyShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _JellyEffect ("Jelly Effect", Range(0,1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        struct Input
        {
            float3 worldPos;
        };

        // 將屬性聲明為半精度變量
        half _Smoothness;
        half _Metallic;
        half _JellyEffect;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float wave = sin(_Time.y * 10 + IN.worldPos.x * 5) * _JellyEffect;
            o.Albedo = _Color.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
            o.Normal = float3(0, 0, 1 + wave);
        }
        ENDCG
    }
    FallBack "Diffuse"
}