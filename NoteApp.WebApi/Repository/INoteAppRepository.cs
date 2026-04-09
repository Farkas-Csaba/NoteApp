using Microsoft.EntityFrameworkCore;
using NoteApp.DbContext;
using NoteApp.Identity;
using NoteApp.Service;

namespace NoteApp.Repository;

public interface INoteAppRepository
{
    Task<string> SaveNoteContent(int id, string content, bool isFavorite);
    Task<bool> CheckExists(int id);
    Task<Notes> GetNoteWithFavorite(int id);
    Task<IEnumerable<int>> GetAllIds();
    Task<IEnumerable<Notes>> GetFavoriteNotes();
}

public class NoteAppRepository : INoteAppRepository
{
    private readonly NoteAppDbContext _appDbContext;
    

    public NoteAppRepository(NoteAppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<bool> CheckExists(int id)
    {
        return await _appDbContext.Notes.AnyAsync(n => n.NotesId == id);
    }
    
    public async Task<string> SaveNoteContent(int id, string content, bool isFavorite)
    {
        var existingNote = await _appDbContext.Notes.FindAsync(id);

        if (existingNote == null)
        {
            
            var newNote = new Notes
            {
                NotesId = id,
                Content = content,
                IsFavorite = isFavorite
                
            };
            _appDbContext.Notes.Add(newNote);
        }
        else
        {
            existingNote.Content = content;
            existingNote.IsFavorite = isFavorite;
        }

        await _appDbContext.SaveChangesAsync();
        return content;
    }
    public async Task<Notes> GetNoteWithFavorite(int id)
    {
        return await _appDbContext.Notes.FirstOrDefaultAsync(n => n.NotesId == id);
    }

    public async Task<IEnumerable<int>> GetAllIds()
    {
        return await _appDbContext.Notes.Select(n => n.NotesId).ToListAsync();
    }

    public async Task<IEnumerable<Notes>> GetFavoriteNotes()
    {
        return await _appDbContext.Notes.Where(n => n.IsFavorite == true).ToListAsync();
    }
}