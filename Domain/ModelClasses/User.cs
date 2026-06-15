namespace Domain.ModelClasses;

public class User
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public int Id { get; set; }

    public override string ToString()
    {
        return $"Username: {UserName}, Password: {Password}, Id: {Id}";
    }
}