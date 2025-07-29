Shader "Unlit/BlurShader"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _BlurSize("Blur Size", Range(0, 10)) = 1.0 // ← Ini adalah slider!
    }

    SubShader
    {
        Tags { "Queue" = "Overlay" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _BlurSize;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 color = half4(0, 0, 0, 0);
                float2 uv = i.uv;

                float blurSize = _BlurSize * 0.01;
                float2 texelSize = float2(blurSize, blurSize);

                // 3x3 Box blur
                color += tex2D(_MainTex, uv + float2(-texelSize.x, -texelSize.y)) * 0.111;
                color += tex2D(_MainTex, uv + float2(0.0, -texelSize.y)) * 0.111;
                color += tex2D(_MainTex, uv + float2(texelSize.x, -texelSize.y)) * 0.111;

                color += tex2D(_MainTex, uv + float2(-texelSize.x, 0.0)) * 0.111;
                color += tex2D(_MainTex, uv) * 0.111;
                color += tex2D(_MainTex, uv + float2(texelSize.x, 0.0)) * 0.111;

                color += tex2D(_MainTex, uv + float2(-texelSize.x, texelSize.y)) * 0.111;
                color += tex2D(_MainTex, uv + float2(0.0, texelSize.y)) * 0.111;
                color += tex2D(_MainTex, uv + float2(texelSize.x, texelSize.y)) * 0.111;

                return color;
            }
            ENDCG
        }
    }

    Fallback "Diffuse"
}
