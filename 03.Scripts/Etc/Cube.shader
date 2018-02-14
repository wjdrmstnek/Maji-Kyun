Shader "Custom/Cube"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" "Queue"="Transparent" }
		
		CGPROGRAM
		#pragma surface surf Lambert noambient alpha:blend

		struct Input
		{
			float4 color:COLOR;
		};

		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutput o)
		{
			o.Emission = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}