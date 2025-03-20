Shader "Effects/Explosion" {
	Properties {
		_TintColorR ("Tint Color R (Core)", Vector) = (0.5,0.5,0.5,1)
		_TintColorG ("Tint Color G (Glow)", Vector) = (0.5,0.5,0.5,1)
		_TintColorB ("Tint Color B (Shadow)", Vector) = (0.5,0.5,0.5,1)
		_ColorStrR ("Color R strength", Range(0.01, 4)) = 1
		_ColorStrG ("Color G strength", Range(0.01, 4)) = 1
		_ColorStrB ("Color B strength", Range(0.01, 4)) = 1
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01, 3)) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}