#type vertex
#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec4 aColor;

uniform mat4 aMVP;
uniform float vTime;

out vec4 fColor;
out vec3 fCoord;

void main(void)
{
     fColor = aColor;
     fCoord = aPosition;
     fCoord.y = fract(sin(vTime)*1.0); 
    
     gl_Position = aMVP * vec4(fCoord, 1.0); 
}

#type fragment
#version 330 core



in vec4 fColor;
in vec3 fCoord;
uniform vec2 aRes;

out vec4 outputColor;


void main()
{

    vec2 uv =  fCoord.xz / (aRes * 2);

    outputColor = vec4(uv, 1.0, 1.0);

}

