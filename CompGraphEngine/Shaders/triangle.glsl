﻿#type vertex
#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec4 aColor;
out vec4 fColor;
void main(void)
{
     fColor = aColor;
     gl_Position = vec4(aPosition, 1.0); 
}

#type fragment
#version 330 core
in vec4 fColor;

out vec4 outputColor;

void main()
{
    outputColor = fColor;
}