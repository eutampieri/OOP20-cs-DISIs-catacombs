/// <summary>
/// A single object. Usefull for Factories.
/// </summary>
public interface ISingleObject<TItem> where TItem : GameObject 
{
    T create(int x, int y, TileMap tm);
}
