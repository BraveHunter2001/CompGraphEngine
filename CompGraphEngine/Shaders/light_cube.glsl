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
  
     gl_Position = aMVP * vec4(fCoord, 1.0); 
}

#type fragment
#version 330 core



in vec4 fColor;
in vec3 fCoord;


out vec4 outputColor;


void main()
{

    outputColor = vec4(1);

}

