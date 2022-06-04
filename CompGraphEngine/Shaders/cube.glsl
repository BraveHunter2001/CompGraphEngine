#type vertex
#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec4 aColor;
layout(location = 2) in vec3 aNormal;


uniform mat4 aMVP;
uniform mat4 Model;


out vec4 fColor;
out vec3 fCoord;
out vec3 fNormal;



void main(void)
{
     fColor = aColor;
     fCoord = vec3(Model * vec4(aPosition, 1.0));
     fNormal = vec3(Model * vec4(aNormal, 1.0));  
  

     gl_Position = aMVP * vec4(fCoord, 1.0); 
}

#type fragment
#version 330 core



in vec4 fColor;
in vec3 fCoord;
in vec3 fNormal;


out vec4 outputColor;
uniform vec3 lightColor;
uniform vec3 lightPos;
uniform vec3 viewPos; 


struct DirectionalLight {
  vec3 direction;

  vec3 color;
  float ambientIntensity;
  float diffuseIntensity;
  float specularIntensity; // for debug purposes, should be set to 1.0
};

void main()
{
 

}

