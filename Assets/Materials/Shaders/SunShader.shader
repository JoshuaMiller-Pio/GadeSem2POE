Shader "Custom/SunShader"
{
   Properties
    {
        _Color("Color", Color) = (1, 0.84, 0, 1) // Gold color
        _Metallic("Metallic", Range(0, 1)) = 1.0
        _Smoothness("Smoothness", Range(0, 1)) = 1.0
        _DisplacementStrength("Displacement Strength", Range(0, 0.5)) = 0.1
        _NoiseScale("Noise Scale", Float) = 1.0
        _TimeSpeed("Time Speed", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 300

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows vertex:vert

        sampler2D _MainTex;
        fixed4 _Color;
        half _Metallic;
        half _Smoothness;
        float _DisplacementStrength;
        float _NoiseScale;
        float _TimeSpeed;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void vert (inout appdata_full v)
        {
            // Simple noise for vertex displacement
            float noise = sin((v.vertex.x * _NoiseScale + _Time.y * _TimeSpeed) * 5.0) 
                        + cos((v.vertex.z * _NoiseScale - _Time.y * _TimeSpeed) * 5.0);

            // Offset the vertex position based on noise
            v.vertex.xyz += v.normal * noise * _DisplacementStrength;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Set the color and material properties
            o.Albedo = _Color.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}