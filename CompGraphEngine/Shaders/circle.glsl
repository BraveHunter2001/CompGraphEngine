#type vertex
#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec4 aColor;


out vec4 fColor;
out vec3 fCoord;

void main(void)
{
     fColor = aColor;
     fCoord = aPosition;
    
     gl_Position = vec4(aPosition, 1.0); 
}

#type fragment
#version 330 core

//uniform vec2 uResolution;

in vec4 fColor;
in vec3 fCoord;

out vec4 outputColor;

void main()
{
    // Parameters
    float thickness = 1;
    float fade = 0.001;
    vec2 uv = fCoord.xy;
    
    float distance = 1.0 - length(uv);
    vec3 color = vec3(smoothstep(0.0, fade, distance));
    color *= vec3(smoothstep(thickness + fade, thickness, distance));
    

    // Set output color
    outputColor = vec4(color,1.0); 
    outputColor *= fColor;
}
