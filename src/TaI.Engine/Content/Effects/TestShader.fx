﻿#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler s0;

float4 displace(float4 pos : SV_POSITION, float4 c : COLOR0, float2 coords : TEXCOORD0) : SV_TARGET0
{
    float4 color = tex2D(s0, coords);
    
    //float4 color = FirstTexture.Sample(FirstSampler, coords);
    if (color.a)
        color.rgb = coords.y;

    return color;
}

technique displacement
{
    pass P0
    {
        PixelShader = compile PS_SHADERMODEL displace();
    }
}