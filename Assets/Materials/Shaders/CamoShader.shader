Shader "Unlit/CamoShader"
{
 Properties
    {
        _MainTex ("Base Texture", 2D) = "white" { }
        _Color1 ("Color 1", Color) = (0.1, 0.6, 0.1, 1) 
        _Color2 ("Color 2", Color) = (0.5, 0.3, 0.1, 1) 
        _Color3 ("Color 3", Color) = (0.2, 0.2, 0.2, 1) 
        _Color4 ("Color 4", Color) = (0.8, 0.7, 0.2, 1) 
        _PatternScale ("Pattern Scale", Float) = 1.0
        _NoiseLayerCount ("Noise Layers", Float) = 4.0
        _NoiseStrength ("Noise Strength", Float) = 0.5
        _UVOffsetStrength ("UV Offset Strength", Float) = 0.1 
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

            //Vertex 
            struct vertex
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            //Vertex declaration and data set
            vertex vert(appdata v)
            {
                vertex vertex;
                vertex.pos = UnityObjectToClipPos(v.vertex);
                vertex.uv = v.uv;
                return vertex;
            }

            // Unity Perlin noise function to add randomness
            float perlinNoise(float2 uv)
            {
                return (sin(uv.x * 12 + uv.y * 78) * 43758);
            }

            // Fragment Shader
            half4 frag(vertex i) : SV_Target
            {
                
                half4 texColor = tex2D(_MainTex, i.uv);

                // Apply random UV offsets to create randomness
                float2 uvOffset = float2(sin(i.uv.x * 100.0), cos(i.uv.y * 100.0)) * _UVOffsetStrength;
                float2 uv = i.uv * _PatternScale + uvOffset;

                // Apply perlin noise in layers to add more randomness and mke it look more natural
                float noiseValue = 0.0;
                float scale = _PatternScale;
                for (int j = 0; j < 4; j++)
                {
                    noiseValue += perlinNoise(uv * scale) * 0.5 + 0.5; 
                    scale *= 2.0;
                }

                //Normalize the noise
                noiseValue /= _NoiseLayerCount;

                // Add more randomness
                noiseValue = noiseValue + _NoiseStrength * (sin(i.uv.x + i.uv.y) * 0.5 + 0.5);

                // Use the noise value to select a color from the camo colors
                half4 finalColor;
                if (noiseValue < 0.25)
                    finalColor = _Color1; 
                else if (noiseValue < 0.5)
                    finalColor = _Color2; 
                else if (noiseValue < 0.75)
                    finalColor = _Color3; 
                else
                    finalColor = _Color4; 

                // Combine the base texture color with the camouflage pattern
                return texColor * finalColor;
            }
            ENDCG
        }
    }

    Fallback "Diffuse"
}