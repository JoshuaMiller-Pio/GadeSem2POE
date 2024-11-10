Shader "Unlit/CamoShader"
{
 Properties
    {
        _MainTex ("Base Texture", 2D) = "white" { }
        _Color1 ("Color 1", Color) = (0.1, 0.6, 0.1, 1) // Green
        _Color2 ("Color 2", Color) = (0.5, 0.3, 0.1, 1) // Brown
        _Color3 ("Color 3", Color) = (0.2, 0.2, 0.2, 1) // Black
        _Color4 ("Color 4", Color) = (0.8, 0.7, 0.2, 1) // Tan
        _PatternScale ("Pattern Scale", Float) = 1.0
        _NoiseLayerCount ("Noise Layers", Float) = 4.0
        _NoiseStrength ("Noise Strength", Float) = 0.5
        _UVOffsetStrength ("UV Offset Strength", Float) = 0.1 // For randomness in the pattern
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Properties
            sampler2D _MainTex;
            float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            float4 _Color4;
            float _PatternScale;
            float _NoiseLayerCount;
            float _NoiseStrength;
            float _UVOffsetStrength;

            // Vertex Shader
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Simple Perlin noise function for smooth noise
            float perlinNoise(float2 uv)
            {
                // Using Unity's built-in Perlin noise function to get smooth noise values
                return (sin(uv.x * 12.9898 + uv.y * 78.233) * 43758.5453);
            }

            // Fragment Shader
            half4 frag(v2f i) : SV_Target
            {
                // Sample the base texture (if any)
                half4 texColor = tex2D(_MainTex, i.uv);

                // Apply random UV offsets to avoid tiling and create randomness
                float2 uvOffset = float2(sin(i.uv.x * 100.0), cos(i.uv.y * 100.0)) * _UVOffsetStrength;
                float2 uv = i.uv * _PatternScale + uvOffset;

                // Generate Perlin noise in layers with different frequencies
                float noiseValue = 0.0;
                float scale = _PatternScale; // Control the overall scale of the camo
                for (int j = 0; j < 4; j++) // Use multiple layers of Perlin noise for more organic patterns
                {
                    noiseValue += perlinNoise(uv * scale) * 0.5 + 0.5; // Perlin noise produces [-1, 1], so add 0.5 to shift to [0, 1]
                    scale *= 2.0; // Increase the frequency for finer details
                }
                noiseValue /= _NoiseLayerCount; // Normalize the final noise value to [0, 1]

                // Add a bit of randomness (strength) to the pattern
                noiseValue = noiseValue + _NoiseStrength * (sin(i.uv.x + i.uv.y) * 0.5 + 0.5);

                // Use the noise value to select a color from the camo colors
                half4 finalColor;
                if (noiseValue < 0.25)
                    finalColor = _Color1; // Green
                else if (noiseValue < 0.5)
                    finalColor = _Color2; // Brown
                else if (noiseValue < 0.75)
                    finalColor = _Color3; // Black
                else
                    finalColor = _Color4; // Tan

                // Combine the base texture color with the camouflage pattern
                return texColor * finalColor;
            }
            ENDCG
        }
    }

    Fallback "Diffuse"
}