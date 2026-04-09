using Microsoft.AspNetCore.Mvc;
using NoteApp.Identity;
using NoteApp.Repository;
using NoteApp.Service;

namespace NoteApp.Controllers;

[ApiController]

public class NoteAppController : ControllerBase
{
    private readonly INoteAppService _appService;

    public NoteAppController(INoteAppService appService)
    {
        _appService = appService;
    }
    [HttpPut("/notes/{id}")]
    public async Task<IActionResult> CreateNotes(int id,[FromBody] string content, [FromQuery] bool isFavorite = false)
    {
        string returnedContent = await _appService.AddNote(id, content, isFavorite);

        if (returnedContent != "")
        {
            return NoContent();
        }

        return BadRequest("Did not provide note body");
    }
    [HttpGet("/notes/{id}")]
    public async Task<IActionResult> GetNote(int id)
    {
        Notes note = await _appService.GetNote(id);
        if (note == null)
        {
            return NotFound("No note exists with such id");
        }

        return Ok(new
        {
            content = note.Content,
            favorite = note.IsFavorite
        });
    }
    [HttpGet("/notes")]
    public async Task<IActionResult> GetAllNoteIds()
    {
        var ids = await _appService.GetIds();

        return Ok(ids);
    }
    [HttpGet("/notes/favorites")]
    public async Task<IActionResult> GetAllFavorites()
    {
        var notes = await _appService.GetFavorites();

        return Ok(notes);
    }
}