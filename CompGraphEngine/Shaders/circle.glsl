﻿#type vertex
#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec4 aColor;
layout(location = 2) in vec2 aTexCoord;

uniform mat4 aMVP;

out vec4 fColor;
out vec3 fCoord;
out vec2 fTexCoord;

void main(void)
{
     fColor = aColor;
     fCoord = aPosition;
     fTexCoord = aTexCoord;
     gl_Position = aMVP * vec4(aPosition, 1.0); 
}

#type fragment
#version 330 core



in vec4 fColor;
in vec3 fCoord;
in vec2 fTexCoord;

out vec4 outputColor;

uniform float aThickness;
uniform sampler2D ourTexture;

void main()
{
   
    vec2 uv =  fCoord.xy;

    float dist = 1.0 - length(uv);
    float fade = 0.005;

    vec3 color = vec3(smoothstep(0.0,fade, dist));
    color *= vec3(smoothstep(aThickness + fade, aThickness, dist));

    outputColor = vec4(color, 1.0 );
    outputColor *= fColor;
    
    outputColor *= texture(ourTexture, fTexCoord);


}

