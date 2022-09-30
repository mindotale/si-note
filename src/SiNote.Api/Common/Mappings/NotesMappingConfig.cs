using Mapster;
using SiNote.Application.Notes.Commands;
using SiNote.Application.Notes.Queries;
using SiNote.Contracts.Notes;
using SiNote.Domain.Entities;

namespace BubberDinner.Api.Common.Mapping;

public class NotesMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateNoteRequest, CreateNoteCommand>();
        config.NewConfig<(Guid Id, UpdateNoteRequest Request), UpdateNoteCommand>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d, s => s.Request);
        config.NewConfig<PaginationRequestQuery, GetNotesWithPaginationQuery>();
    }
}
