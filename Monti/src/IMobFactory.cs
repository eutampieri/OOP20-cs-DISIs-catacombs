/// <summary>
/// A factory that spawn objects.
/// </summary>
public interface IMobFactory 
{

    List<Entity> SpawnAt(int x, int y, SingleObject<Entity> f);

    List<Entity> SpawnSome(int n, SingleObject<Entity> f);

    List<Entity> SpawnRandom();
}
