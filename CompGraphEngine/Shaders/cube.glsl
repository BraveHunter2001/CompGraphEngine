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
     fCoord = aPosition;
     fNormal = aNormal;
     

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
void main()
{
    // ambient
    float ambientStrength = 0.1;
    vec3 ambient = ambientStrength * lightColor;
  	
    // diffuse 
    vec3 norm = normalize(fNormal);
    vec3 lightDir = normalize(lightPos - fCoord);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = diff * lightColor;

    // specular
    float specularStrength = 0.5;
    vec3 viewDir = normalize(viewPos - fCoord);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), 2);
    vec3 specular = specularStrength * spec * lightColor;  
            
    vec3 result = (ambient + diffuse + specular) * fColor.rgb;

    outputColor = vec4(result,1);

}

