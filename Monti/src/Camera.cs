using System;

/// <summary>
/// The game camera.
/// </summary>
public class Camera 
{
    private int xOffset;
    private int yOffset;

    private int mapWidth, mapHeight;

    public Camera(int xOffset, int yOffset, int mapWidth, int mapHeight) 
    {
        this.xOffset = xOffset;
        this.yOffset = yOffset;
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
    }

    public void CenterOnEntity(GameObject e, int gameWidth, int gameHeight) 
    {
        xOffset = e.GetPosX() - (gameWidth / 2);
        yOffset = e.GetPosY() - (gameHeight / 2);
        if (xOffset < 0) 
        {
            xOffset = 0;
        } else if (xOffset > (mapWidth - gameWidth)) 
        {
            xOffset = mapWidth - gameWidth;
        }
        if (yOffset < 0) {
            yOffset = 0;
        } else if (yOffset > (mapHeight - gameHeight)) 
        {
            yOffset = mapHeight - gameHeight;
        }
    }

    public int GetXOffset() 
    {
        return xOffset;
    }

    public int GetYOffset() 
    {
        return yOffset;
    }
}
