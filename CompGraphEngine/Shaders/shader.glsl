#type vertex
#version 330 core

layout(location = 0) in vec3 aPosition;


uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;


out vec3 fCoord;


void main(void)
{
     
     fCoord = aPosition;
     
     gl_Position = projection * view * model * vec4(aPosition, 1.0); 
}

#type fragment
#version 330 core




in vec3 fCoord;


out vec4 outputColor;


void main()
{
 
    outputColor = vec4(1.0,0.0,0.0,1.0);

}
