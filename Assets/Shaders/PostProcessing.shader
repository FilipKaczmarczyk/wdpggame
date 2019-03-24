Shader "Custom/PostProcessing"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Brightness ("Brightness", float) = 1
		_Contrast ("Contrast", float) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
			uniform float _Brightness;
			uniform float _Contrast;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                col = col * _Brightness;
				col = (col - 0.5) * _Contrast + 0.5;
                return col;
            }
            ENDCG
        }
    }
}
