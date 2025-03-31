Shader "Custom/CameraBackgroundShader"
{
    Properties
    {
        _Color1 ("Top Color", Color) = (0.761, 0.89, 0.984, 1)  // ���Ŧ� (#C2E9FB)
        _Color2 ("Bottom Color", Color) = (0.631, 0.769, 0.992, 1)  // �L�Ŧ� (#A1C4FD)
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color1;
            fixed4 _Color2;
            sampler2D _MainTex;

            v2f vert (float4 vertex : POSITION, float2 uv : TEXCOORD0)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(vertex);
                o.uv = uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv);  // ���o�e���C��
                fixed4 gradient = lerp(_Color2, _Color1, i.uv.y); // �����C��
                return lerp(gradient, texColor, texColor.a); // ���z���ϰ���ܺ��ܭI��
            }
            ENDCG
        }
    }
}
