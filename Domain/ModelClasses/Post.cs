using System.ComponentModel.DataAnnotations;

namespace Domain.ModelClasses;

public class Post
{
    [Required, MaxLength(128)]
    public string Header { get; set; }
    public string Body { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public string OwnerId { get; set; }
    public int Id { get; set; } 

    public Post(string header)
    {
        header = Header;
    }

    public Post()
    {
        
    }

    public override string ToString()
    {
        return $"Header: {Header}, body: {Body}, ownerId: {OwnerId}, id: {Id}, ";
    }
}