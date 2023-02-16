global using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace videonet.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly static List<Character> characters = new()
        {
            new Character(),
            new Character{Id = 1,Name = "Latheef"},
            new Character{Id=2,Name = "Abdul latheef"}
        };
        private readonly IMapper _mapper;
        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;

        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>
            {
                Data = characters.ConvertAll(c => _mapper.Map<GetCharacterDto>(c))
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetOneCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto newUpdateCharacterDto)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var characterToUpdate = characters.FirstOrDefault(c => c.Id == newUpdateCharacterDto.Id);
                if (characterToUpdate is null)
                    throw new Exception($"Character with Id {newUpdateCharacterDto.Id} not found");
                _mapper.Map(newUpdateCharacterDto, characterToUpdate);
                characterToUpdate.Name = newUpdateCharacterDto.Name;
                characterToUpdate.Defence = newUpdateCharacterDto.Defence;
                characterToUpdate.HitPoints = newUpdateCharacterDto.HitPoints;
                characterToUpdate.Intelligence = newUpdateCharacterDto.Intelligence;
                characterToUpdate.Strength = newUpdateCharacterDto.Strength;
                characterToUpdate.Class = newUpdateCharacterDto.Class;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(characterToUpdate);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var characterToDelete = characters.FirstOrDefault(c => c.Id == id);
                if (characterToDelete is null)
                    throw new Exception($"Character with Id {id} not found");
               
                characters.Remove(characterToDelete);
                serviceResponse.Data = characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }

}