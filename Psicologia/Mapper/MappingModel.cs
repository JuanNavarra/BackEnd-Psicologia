/// <summary>
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
            CreateMap<KeyWords, KeyWordDto>()
                .ForMember(s => s.Id, option => option.MapFrom(o => o.Idkey));
            CreateMap<KeyWordDto, KeyWords>()
                .ForMember(s => s.Idkey, option => option.MapFrom(o => o.Id)); ;
            CreateMap<Blogs, BlogDetalleDto>();
            CreateMap<BlogDetalleDto, Blogs>();
            CreateMap<BlogKey, BlogKeyWordsDto>();
            CreateMap<BlogKeyWordsDto, BlogKey>();
            CreateMap<Imagenes, ImagenesDto>();
            CreateMap<ImagenesDto, Imagenes>();
            CreateMap<Categorias, CategoriasDto>()
                .ForMember(s => s.Id, option => option.MapFrom(o => o.Idcategoria));
            CreateMap<CategoriasDto, Categorias>()
                .ForMember(s => s.Idcategoria, option => option.MapFrom(o => o.Id));
            CreateMap<BlogDto, YoutubeDto>()
                .ForMember(s => s.RutaVideo, option => option.MapFrom(o => o.ImagenPost))
                .ForMember(s => s.IdVideo, option => option.MapFrom(o => o.NombreImagen));
            CreateMap<YoutubeDto, BlogDto>()
                .ForMember(s => s.ImagenPost, option => option.MapFrom(o => o.RutaVideo))
                .ForMember(s => s.NombreImagen, option => option.MapFrom(o => o.IdVideo));
            CreateMap<Blogs, EntradaYoutubeDto>()
                .ForMember(s => s.IdVideo, option => option.MapFrom(o => o.Idimagen));
            CreateMap<EntradaYoutubeDto, Blogs>()
                .ForMember(s => s.Idimagen, option => option.MapFrom(o => o.IdVideo));
            CreateMap<BlogDetalleDto, YoutubeDto>()
                .ForMember(s => s.RutaVideo, option => option.MapFrom(o => o.ImagenPost))
                .ForMember(s => s.IdVideo, option => option.MapFrom(o => o.NombreImagen));
            CreateMap<YoutubeDto, BlogDetalleDto>()
                .ForMember(s => s.ImagenPost, option => option.MapFrom(o => o.RutaVideo))
                .ForMember(s => s.NombreImagen, option => option.MapFrom(o => o.IdVideo));
            CreateMap<YoutubeDto, Blogs>();
            CreateMap<Blogs, YoutubeDto>();

        }
        #endregion
    }
}
