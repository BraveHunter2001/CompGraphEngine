﻿#type vertex
#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec4 aColor;

uniform mat4 aMVP;
//uniform float vTime;

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
    
    //fCoord.y *= sin(vTime);
    //fCoord.z *= cos(vTime);
    
     gl_Position = aMVP * vec4(fCoord, 1.0); 
}

#type fragment
#version 330 core



in vec4 fColor;
in vec3 fCoord;


out vec4 outputColor;

float near = 0.1; 
float far  = 1000.0; 
  
float LinearizeDepth(float depth) 
{
    float z = depth * 2.0 - 1.0; // back to NDC 
    return (2.0 * near * far) / (far + near - z * (far - near));	
}

void main()
{

     float depth = LinearizeDepth(gl_FragCoord.z) / far; // divide by far for demonstration
    outputColor = vec4(vec3(depth), 1.0);

}

