Shader "Custom/Dissolve"
{
	//Unity Basic Dissolve with Texture http://wiki.unity3d.com/index.php?title=Dissolve_With_Texture&oldid=13838
	Properties{
		 _MainTex("Texture (RGB)", 2D) = "white" {}
		 _SliceGuide("Slice Guide (RGB)", 2D) = "white" {}
		 _DissolveAmount("Dissolve Amount", Range(0.0, 1.0)) = 0.5

		_BurnSize("Burn Size", Range(0.0, 1.0)) = 0.15
		_BurnRamp("Burn Ramp (RGB)", 2D) = "white" {}
	}
		SubShader{
			Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
			LOD 200

			//Cull Off
			CGPROGRAM

			#pragma surface surf Lambert alpha //addshadow


			struct Input {
			float2 uv_MainTex;
			float2 uv_SliceGuide;
			float _DissolveAmount;
			float4 color: Color;
			};


			sampler2D _MainTex;
			sampler2D _SliceGuide;
			float _DissolveAmount;
			sampler2D _BurnRamp;
			float _BurnSize;

			void surf(Input IN, inout SurfaceOutput o) {
				//_DissolveAmount += (1 - IN.color.a);
				clip(tex2D(_SliceGuide, IN.uv_SliceGuide).rgb - _DissolveAmount);
				o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;

				half test = tex2D(_SliceGuide, IN.uv_MainTex).rgb - _DissolveAmount;
				if (test < _BurnSize && _DissolveAmount > 0 && _DissolveAmount < 1) {
					o.Emission = tex2D(_BurnRamp, float2(test * (1 / _BurnSize), 0));
					o.Albedo *= o.Emission;
				}

				o.Alpha = IN.color.a;
			}
			ENDCG
	}
		Fallback "Diffuse"
}