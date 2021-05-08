using System;


/// <summary>
/// Rapresents the immaginary box around every gameobject. Usefull for collision detection.
/// </summary>
public class CollisionBox 
{
    private int posX, posY;
    private int width, height;

    public CollisionBox(int posX, int posY, int width, int height)
    {
        this.posX = posX;
        this.posY = posY;
        this.width = width;
        this.height = height;
    }

    public CollisionBox(CollisionBox box) 
    {
        this.posX = box.posX;
        this.posY = box.posY;
        this.width = box.width;
        this.height = box.height;
    }
    
    public void Move(int dx, int dy) 
    {
        if (posX + dx > 0 && posY + dy > 0) 
        {
            posX += dx;
            posY += dy;
        }
    }

    public boolean Overlaps(CollisionBox r) 
    {
        return posX < r.posX + r.width && posX + width > r.posX && posY < r.posY + r.height && posY + height > r.posY;
    }

    public int GetPosX() 
    {
        return posX;
    }

    public void SetPosX(int posX) 
    {
        this.posX = posX;
    }

    public int GetPosY() 
    {
        return posY;
    }

    public void SetPosY(int posY) 
    {
        this.posY = posY;
    }

    public int GetWidth() 
    {
        return width;
    }
    public void SetWidth(int width) 
    {
        this.width = width;
    }

    public int GetHeight() 
    {
        return height;
    }
    public void SetHeight(int height) 
    {
        this.height = height;
    }

    public void SetLocation(int x, int y) 
    {
        this.SetPosX(x);
        this.SetPosY(y);
    }

    public void SetDimensions(int width, int height) 
    {
        this.SetWidth(width);
        this.SetHeight(height);
    }

}
