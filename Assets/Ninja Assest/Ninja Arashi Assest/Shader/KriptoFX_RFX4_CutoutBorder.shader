Shader "KriptoFX/RFX4/CutoutBorder" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[HDR] _EmissionColor ("Emission Color", Vector) = (1,1,1,1)
		_EmissionTex ("Emission (A)", 2D) = "black" {}
		_BumpTex ("Normal (RGB)", 2D) = "gray" {}
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_Metallic ("Metallic", Range(0, 1)) = 0
		_Cutoff ("_Cutoff", Range(0, 1)) = 0
		[HDR] _BorderColor ("Border Color", Vector) = (1,1,1,1)
		_CutoutThickness ("Cutout Thickness", Range(0, 1)) = 0.03
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Transparent/Cutout/Diffuse"
}