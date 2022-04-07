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
    
     gl_Position = aMVP * vec4(aPosition, 1.0); 
}

#type fragment
#version 330 core



in vec4 fColor;
in vec3 fCoord;

out vec4 outputColor;




void main()
{
   
    vec2 uv =  fCoord.xy;

    float gridSize = 10.0;
    // width of a line on the screen plus a little bit for AA
    float width = (gridSize * 1.2);

    // chop up into grid
    uv = fract(uv * gridSize);
    // abs version
    float grid = max(
        1.0 - abs((uv.y - 0.5) / width),
        1.0 - abs((uv.x - 0.5) / width)
    );

    
    outputColor = vec4(grid, grid, grid, 1.0);
    

}

