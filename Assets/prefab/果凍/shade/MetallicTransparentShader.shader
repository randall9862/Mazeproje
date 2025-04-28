Shader "Custom/MetallicTransparentShader"
{
    Properties
    {
        _Color("Base Color", Color) = (1, 1, 1, 1)
        _Metallic("Metallic", Range(0, 1)) = 0.8
        _Smoothness("Smoothness", Range(0, 1)) = 0.9
        _MainTex("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                float3 normal : TEXCOORD2;
                float4 pos : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _Color;
            float _Metallic;
            float _Smoothness;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Base color
                fixed4 col = tex2D(_MainTex, i.uv) * _Color;

                // Metallic and smoothness
                float3 viewDir = normalize(_WorldSpaceCameraPos - i.worldPos);
                float fresnel = pow(1.0 - max(0, dot(viewDir, i.normal)), 3.0);
                col.rgb = lerp(col.rgb, float3(1.0, 1.0, 1.0), fresnel * _Metallic);

                // Apply reduced transparency
                col.a *= 0.9; // Less transparent

                return col;
            }
            ENDCG
        }
    }
}