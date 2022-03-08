#type vertex
#version 330 core

layout(location = 0) in vec3 aPosition;
void main(void)
{
     gl_Position = vec4(aPosition, 1.0); 
     
}

#type fragment
#version 330 core

out vec4 outputColor;
uniform vec4 ourColor;
void main()
{
    outputColor = ourColor;
}