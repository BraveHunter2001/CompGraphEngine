#type vertex
#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec4 aColor;
layout(location = 2) in vec2 aTexCoord;

uniform mat4 aMVP;
//uniform float vTime;
out vec2 fTexCoord;
out vec4 fColor;
out vec3 fCoord;

float random (float seed) {
    return fract(sin(seed)*
        43758.5453123);
}

void main(void)
{
     fColor = aColor;
     fCoord = aPosition;
    fTexCoord = aTexCoord;
    //fCoord.y *= sin(vTime);
    //fCoord.z *= cos(vTime);
    
     gl_Position = aMVP * vec4(fCoord, 1.0); 
}

#type fragment
#version 330 core



in vec4 fColor;
in vec3 fCoord;

in vec2 fTexCoord;
out vec4 outputColor;
uniform sampler2D ourTexture;

void main()
{

    outputColor = fColor * texture(ourTexture, fTexCoord);

}

