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
uniform vec3 viewPos; 
uniform vec3 lightColor;
uniform vec3 lightPos;



struct DirectionalLight {
  vec3 direction;

  vec3 color;
  float ambientIntensity;
  float diffuseIntensity;
  float specularIntensity; // for debug purposes, should be set to 1.0
};

struct PointLight {
  vec3 position;
  vec3 color;
  float ambientIntensity;
  float diffuseIntensity;
  float specularIntensity; // for debug purposes, should be set to 1.0
};

vec4 calcDirectionalLight(vec3 normal, vec3 fragmentToCamera, DirectionalLight light) {

    vec4 ambientColor = vec4(light.color, 1) * light.ambientIntensity;
    float diffuseFactor = max(0.0, dot(normal, -light.direction));
    vec4 diffuseColor = vec4(light.color, 1) * light.diffuseIntensity
                      * diffuseFactor;
                        vec3 lightReflect = normalize(reflect(light.direction, normal));

  float specularFactor = pow(max(0.0, dot(fragmentToCamera, lightReflect)), 2);

  vec4 specularColor = light.specularIntensity * vec4(light.color, 1) * 0.1 * specularFactor;

                       return ambientColor + diffuseColor + specularColor;
}

vec4 calcPointLight(vec3 normal, vec3 fragmentToCamera, PointLight light) {
  vec3 lightDirection = normalize(fCoord - light.position);
  float distance = length(fCoord - light.position);
  float pointFactor = 1.0 / (1.0 + pow(distance, 2));

  DirectionalLight tempDirectionalLight = DirectionalLight(
                                            lightDirection,
                                            light.color,
                                            light.ambientIntensity,
                                            light.diffuseIntensity,
                                            light.specularIntensity
                                          );
  return pointFactor * calcDirectionalLight(normal, fragmentToCamera,
                                            tempDirectionalLight);
}

void main()
{
 
  vec3 normal = normalize(fNormal);
  vec3 fragmentToCamera = normalize(viewPos - fCoord);

  PointLight pointLight = PointLight(lightPos,lightColor, 1,10, 32);

  
  vec4 pointColor = calcPointLight(normal, fragmentToCamera, pointLight);


    vec4 gamma = vec4(vec3(1.0/2.2), 1);
    outputColor = fColor *  pow(pointColor, gamma);
 
}




