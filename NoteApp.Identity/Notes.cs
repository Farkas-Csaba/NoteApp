using System.ComponentModel.DataAnnotations;

namespace NoteApp.Identity;

public class Notes
{
    [Key]
    public int NotesId { get; set; }
    public required string Content { get; set; }
    public bool IsFavorite { get; set; } = false;

}