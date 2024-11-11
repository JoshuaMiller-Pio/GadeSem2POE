Shader "Unlit/GoldShader"
{
     Properties
    {
        _BaseColor ("Base Color", Color) = (1, 0.843, 0, 1) 
        _Metallic ("Metallic", Range(0,1)) = 1
        _Smoothness ("Smoothness", Range(0,1)) = 0.8
        _EmissionColor ("Emission Color", Color) = (1, 0.843, 0) 
        _EmissionIntensity ("Emission Intensity", Range(0,5)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _BaseColor;
        fixed _Metallic;
        fixed _Smoothness;
        fixed4 _EmissionColor;
        fixed _EmissionIntensity;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = _BaseColor;
            o.Albedo = c.rgb;
            o.Emission = _EmissionColor.rgb * _EmissionIntensity;
        }
        ENDCG
    }
    FallBack "Diffuse"
}