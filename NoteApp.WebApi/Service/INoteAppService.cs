using NoteApp.DbContext;
using NoteApp.Identity;
using NoteApp.Repository;

namespace NoteApp.Service;

public interface INoteAppService
{
    Task<string> AddNote(int id, string content, bool isFavorite);
    Task<Notes> GetNote(int id);
    Task<IEnumerable<int>> GetIds();
    Task<IEnumerable<Notes>> GetFavorites();

}

public class NoteAppService : INoteAppService
{
    private readonly NoteAppDbContext _appDbContext;
    private readonly INoteAppRepository _appRepository;

    public NoteAppService(NoteAppDbContext appDbContext, INoteAppRepository appRepository)
    {
        _appDbContext = appDbContext;
        _appRepository = appRepository;
    }
    public async Task<string> AddNote(int id, string content, bool isFavorite)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            return "";
        }
        
        return await _appRepository.SaveNoteContent(id, content, isFavorite);
    }

    public async Task<Notes> GetNote(int id)
    {
        if (!(await _appRepository.CheckExists(id)))
        {
            return null;
        }

        return await _appRepository.GetNoteWithFavorite(id);
    }

    public async Task<IEnumerable<int>> GetIds()
    {
        return await _appRepository.GetAllIds();
    }

    public async Task<IEnumerable<Notes>> GetFavorites()
    {
        return await _appRepository.GetFavoriteNotes();
    }
}