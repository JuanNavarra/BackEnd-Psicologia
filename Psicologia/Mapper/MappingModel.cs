﻿/// <summary>
/// Autor Juan Navarra
/// Fecha 17/01/2020
/// </summary>
namespace Psicologia
{
    using AutoMapper;
    using Modelos;
    using Dtos;

    public class MappingModel : Profile
    {
        #region Construnctores
        public MappingModel()
        {
            CreateMap<Comentarios, ComentarioSavedDto>();
            CreateMap<ComentarioSavedDto, Comentarios>();
            CreateMap<Usuarios, UsuarioDto>();
            CreateMap<UsuarioDto, Usuarios>();
        }
        #endregion
    }
}
