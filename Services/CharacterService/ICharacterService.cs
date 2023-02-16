using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace videonet.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterDto>> GetOneCharacter(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newAddCharacterDto);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto newUpdateCharacterDto);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
    }
}