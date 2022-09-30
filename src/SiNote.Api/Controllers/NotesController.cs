using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiNote.Application.Notes.Commands;
using SiNote.Application.Notes.Queries;
using SiNote.Contracts.Notes;

namespace SiNote.Api.Controllers
{
    public class NotesController : ApiController
    {
        public NotesController(ISender sender, IMapper mapper)
            : base(sender, mapper) { }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<NoteResponse>> GetNote(Guid id)
        { 
            var query = new GetNoteQuery(id);
            var response = await Sender.Send(query);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteResponse>>> GetNotes(
            [FromQuery] PaginationRequestQuery pagination)
        {
            var query = Mapper.Map<GetNotesWithPaginationQuery>(pagination);
            var response = await Sender.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNote([FromBody] CreateNoteRequest request)
        {
            var command = Mapper.Map<CreateNoteCommand>(request);
            var response = await Sender.Send(command);
            return CreatedAtAction(nameof(CreateNote), response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateNote(Guid id, [FromBody] UpdateNoteRequest request)
        {
            var command = Mapper.Map<UpdateNoteCommand>((id, request));
            await Sender.Send(command);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteNode(Guid id)
        {
            var command = new DeleteNoteCommand(id);
            await Sender.Send(command);
            return NoContent();
        }
    }
}
