Shader "Custom/SailsTriangle" {
	Properties{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Mask("Mask (RGB)", 2D) = "white" {}
		_Speed("Speed", Range(0, 5.0)) = 2
		_Offset("Offset", Range(-3.0,3.0)) = 0
		_Amplitude("Amplitude", Range(0, 2.0)) = 0.5
	}
		SubShader{
		Tags{ "LightMode" = "ForwardBase" }
		Cull off
		Blend SrcAlpha OneMinusSrcAlpha
		Lighting On


		Pass{

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
#include "UnityLightingCommon.cginc"


	float4 _MainTex_ST;
	sampler2D _MainTex;
	sampler2D _Mask;

	struct v2f {
		float2 uv : TEXCOORD0;
		fixed4 diff : COLOR0;
		float4 pos : SV_POSITION;
	};

	float _Speed;
	float _Offset;
	float _Amplitude;


	v2f vert(appdata_base v)
	{
		v2f o;
		float wv = sin(_Time.y * _Speed + v.vertex.x) * _Amplitude + _Offset;
		float wvmul = 1 - abs((v.vertex.y + 0.5) * 2);
		wvmul += 1 - abs((v.vertex.x + 0.5) * 2);
		v.vertex.z = wv * wvmul * 0.5;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		half3 worldNormal = UnityObjectToWorldNormal(v.normal);
		half3 worldNormal2 = UnityObjectToWorldNormal(-v.normal);
		half nl = max(dot(worldNormal2, _WorldSpaceLightPos0.xyz), dot(worldNormal, _WorldSpaceLightPos0.xyz));
		nl *= 0.5;
		o.diff = nl * _LightColor0;

		// the only difference from previous shader:
		// in addition to the diffuse lighting from the main light,
		// add illumination from ambient or light probes
		// ShadeSH9 function from UnityCG.cginc evaluates it,
		// using world space normal
		o.diff.rgb += ShadeSH9(half4(worldNormal, 1));
		o.diff.a = 1;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{

		fixed4 c = tex2D(_MainTex, i.uv);
		c *= i.diff;
		c.a *= tex2D(_Mask, i.uv).r;
		return c;
	}

		ENDCG

	}
	}
		FallBack "Diffuse"

}