Shader "Custom/CleanGroundShader"
{
   Properties
    {
        _ColorStart ("Color Start", Color) = (1,1,1,1)
        _ColorEnd ("Color End", Color) = (0,0,0,1)
        _GradientStart ("Gradient Start Position", Float) = 0
        _GradientEnd ("Gradient End Position", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            float4 _ColorStart;
            float4 _ColorEnd;
            float _GradientStart;
            float _GradientEnd;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float gradient = (i.worldPos.z - _GradientStart) / (_GradientEnd - _GradientStart);
                gradient = saturate(gradient); // 確保值在0-1之間
                fixed4 col = lerp(_ColorStart, _ColorEnd, gradient);
                return col;
            }
            ENDCG
        }
    }
}
