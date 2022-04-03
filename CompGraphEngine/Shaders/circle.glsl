#type vertex
#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec4 aColor;

uniform mat4 aMVP;

out vec4 fColor;
out vec3 fCoord;

void main(void)
{
     fColor = aColor;
     fCoord = aPosition;
    
     gl_Position = aMVP * vec4(aPosition, 1.0); 
}

#type fragment
#version 330 core



in vec4 fColor;
in vec3 fCoord;

out vec4 outputColor;

uniform vec2 aResolution;


void main()
{
    vec2 aRes = aResolution;
    outputColor = fColor;
    vec2 uv =  fCoord.xy ;
    
    //float aspect = aResolution.x / aResolution.y;
   // uv.x *= aspect;

    //float dist = length(uv);
    //float fade = 0.005;
    //float thickness = 0.1;
    //vec3 col  = vec3(smoothstep(0.0,fade, dist));
    //col *= vec3(1.0 - smoothstep(thickness,thickness - fade, dist));
    outputColor.rg = uv + 0.5;


}

