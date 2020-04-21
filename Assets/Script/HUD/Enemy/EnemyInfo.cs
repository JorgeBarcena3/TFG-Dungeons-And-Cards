
/// <summary>
/// Guardamos la informacion sobre el enemigo
/// </summary>
public class EnemyInfo
{

    public string name { get; set; } = "Enemy name";

    public string description { get; set; } = "Enemy description";


    public EnemyInfo()
    {

    }

    public EnemyInfo(string _name, string _decr)
    {
        this.name        = _name;
        this.description = _decr;
    }
}