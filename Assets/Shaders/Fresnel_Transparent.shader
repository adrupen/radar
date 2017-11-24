Shader "Jockes/Fresnel_Transparent"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
		_Scale("Scale", Float) = 1.0
		_Exponent("Fresnel Exponent", Float) = 1.0
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "RenderType"="Transparent" }
		LOD 100

		ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f
			{
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float R : COLOR0;
			};

			uniform float _Exponent;
			uniform float _Scale;
			fixed4 _Color;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);

				// World space vertices and normals
				float3 posWorld = mul(unity_ObjectToWorld, v.vertex).xyz;
				float3 normWorld = normalize(mul( float4( v.normal, 0.0 ), unity_WorldToObject ).xyz);

				// R value is the fresnel coefficient
				float3 I = normalize(posWorld - _WorldSpaceCameraPos);
				//o.R = pow(1 + dot(I, normWorld), _Exponent);
				o.R = _Scale * pow(1.0 + dot(I, normWorld), _Exponent);

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = _Color;
				col.a = i.R;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
