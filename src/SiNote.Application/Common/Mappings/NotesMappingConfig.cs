using Mapster;
using SiNote.Application.Notes.Commands;
using SiNote.Contracts.Authentication;
using SiNote.Contracts.Notes;

namespace SiNote.Application.Common.Mappings;

public class NotesMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Note, NoteResponse>();
        config.NewConfig<(Guid Id, Guid UserId, CreateNoteCommand Command), Note>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.UserId, s => s.UserId)
            .Map(d => d, s => s.Command).MapToConstructor(true);
        config.NewConfig<(Guid Id, Guid UserId, UpdateNoteCommand Command), Note>()
            .Map(d => d.Id, s => s.Id)
            .Map(d => d.UserId, s => s.UserId)
            .Map(d => d, s => s.Command).MapToConstructor(true);
    }
}
