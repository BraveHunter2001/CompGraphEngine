#type vertex
#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec4 aColor;

uniform mat4 aMVP;


out vec4 fColor;
out vec3 fCoord;

float random (vec2 st) {
    return fract(sin(dot(st.xy,
                         vec2(12.9898,78.233)))*
        43758.5453123);
}

void main(void)
{
     fColor = aColor;
     fCoord = aPosition;
    
    // fCoord.y = random(vTime); 
    
     gl_Position = aMVP * vec4(fCoord, 1.0); 
}

#type fragment
#version 330 core



in vec4 fColor;
in vec3 fCoord;


out vec4 outputColor;


void main()
{

    vec2 uv =  fCoord.xz;

    outputColor = vec4(uv, 1.0, 1.0);

}

