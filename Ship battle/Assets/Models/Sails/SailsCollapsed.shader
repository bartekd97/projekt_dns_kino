Shader "Custom/SailsCollapsed" {
	Properties{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	}
		SubShader{
		Tags{ "LightMode" = "ForwardBase" }
		Lighting On


		Pass{

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"
#include "UnityLightingCommon.cginc"


	sampler2D _MainTex;
	struct v2f {
		float2 uv : TEXCOORD0;
		fixed4 diff : COLOR0;
		float4 pos : SV_POSITION;
	};

	v2f vert(appdata_base v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = v.texcoord;
		half3 worldNormal = UnityObjectToWorldNormal(v.normal);
		half3 worldNormal2 = UnityObjectToWorldNormal(-v.normal);
		half nl = max(dot(worldNormal2, _WorldSpaceLightPos0.xyz), dot(worldNormal, _WorldSpaceLightPos0.xyz));
		nl *= 0.5;
		o.diff = nl * _LightColor0;
		o.diff.rgb += ShadeSH9(half4(worldNormal, 1));
		o.diff.a = 1;
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{

		fixed4 c = tex2D(_MainTex, i.uv);
		c *= i.diff;
		return c;
	}

		ENDCG

	}
	}
		FallBack "Diffuse"

}