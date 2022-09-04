#type vertex
#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec3 aNormals;
layout(location = 2) in vec2 aTexCoords;


uniform mat4 projection;
uniform mat4 view;
uniform mat4 model;
uniform vec3 lightpos;


out vec3 fragPos;
out vec3 fNormals;
out vec2 fTexCoords;
out vec3 LightPos; 


void main(void)
{
     
     fragPos = vec4( view * model * vec4(aPosition, 1.0)).xyz;
     fNormals = mat3(transpose(inverse( view * model))) * aNormals;
     LightPos = vec3(view * vec4(lightpos, 1.0)); // Transform world-space light position to view-space light position
     fTexCoords = aTexCoords;
     
     gl_Position = projection * view * model * vec4(aPosition, 1.0); 
}

#type fragment
#version 330 core

in vec3 fragPos;
in vec3 fNormals;
in vec2 fTexCoords;

struct Material {
    vec3 ambient;
    vec3 diffuse;
    vec3 specular;    
    float shininess;
}; 


struct Light {
    vec3 position;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};


out vec4 outputColor;
uniform Light light;
uniform Material material0;
uniform vec3 viewPos;

void main()
{

    Material material = material0;
    // ambient
    vec3 ambient = light.ambient * material.ambient;
  	
    // diffuse 
    vec3 norm = normalize(fNormals);
    vec3 lightDir = normalize(light.position - fragPos);
    float diff = max(dot(norm, lightDir), 0.0);
    vec3 diffuse = light.diffuse * (diff * material.diffuse);
    
    // specular
    vec3 viewDir = normalize(viewPos - fragPos);
    vec3 reflectDir = reflect(-lightDir, norm);  
    float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
    vec3 specular = light.specular * (spec * material.specular);  
        
    vec3 result = ambient + diffuse + specular;
    outputColor = vec4(result, 1.0);

}
