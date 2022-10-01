using AutoMapper;
using Core.Dto;
using Core.Entidades;

namespace API.Helpers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Hospital, HospitalDto>();
            CreateMap<HospitalDto, Hospital>();

            CreateMap<Paciente, PacienteUpsertDto>().ReverseMap();

            CreateMap<Paciente, PacienteReadyDto>()
                            .ForMember(p => p.Hospital, m => m.MapFrom(c => c.Hospital.NombreHospital));
        }   
    }
}