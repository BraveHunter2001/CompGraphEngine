#type vertex
#version 430 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec4 aColor;


out vec4 fColor;
out vec3 fCoord;
void main(void)
{
     fColor = aColor;
     fCoord = aPosition;
    
     gl_Position = vec4(fCoord, 1.0); 
}

#type fragment
#version 430 core


in vec4 fColor;
in vec3 fCoord;
uniform vec2 uResolution;


out vec4 outputColor;


void main()
{
    // Parameters
    float thickness = 0.1;
    float fade = 0.01;
    vec2 uv = fCoord.xy / uResolution.xy * 2.0 - 1.0;
    float aspect = uResolution.x / uResolution.y;
    uv.x *= aspect;

    float distance = 1.0 - length(uv);
    vec3 color = vec3(smoothstep(0.0, fade, distance));
    color *= vec3(smoothstep(thickness + fade, thickness, distance));
    

    // Set output color
    outputColor = vec4(color,1.0); 
    outputColor.rgb *= fColor;
}
