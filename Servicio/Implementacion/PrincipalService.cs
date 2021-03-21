namespace Servicio
{
    using AutoMapper;
    using AutoMapper;
    using Dtos;
    using Modelos;
    using Repositorio;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;
    using System.Linq;

    public class PrincipalService : IPrincipalService
    {
        #region Propiedades
        private readonly IPrincipalRepository principalRepository;
        private readonly IMapper mapper;
        private readonly IBlogRepository blogRepository;
        #endregion
        #region Constructores
        public PrincipalService(IPrincipalRepository principalRepository, IBlogRepository blogRepository, IMapper mapper)
        {
            this.principalRepository = principalRepository;
            this.blogRepository = blogRepository;
            this.mapper = mapper;
        }
        #endregion
        #region Metodos y funciones
        /// <summary>
        /// Muestra el contenido de las faqs de la pagina
        /// </summary>
        /// <returns></returns>
        public FaqsDto MostrarFaq()
        {
            try
            {
                FaqsDto faqsDto = principalRepository.MostrarFaq();
                return faqsDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Guarda el contenido de una seccion
        /// </summary>
        /// <param name="principalDto"></param>
        /// <returns></returns>
        public ApiCallResult GuardarContenido(PrincipalDto principalDto)
        {
            try
            {
                this.GuardarImagenContenido(principalDto);
                Imagenes imagen = this.blogRepository.BuscarImagenPorRuta(principalDto.RutaImagen);
                Principal principal = new Principal()
                {
                    Idimagen = imagen.Idimagen,
                    Descripcion = principalDto.Descripcion,
                    Estado = true
                };
                this.principalRepository.GuardarSeccionPrincipal(principal);
                string id = this.principalRepository.BuscarContenidoPrincipal(principal.Descripcion).Idprincipal.ToString();
                return new ApiCallResult
                {
                    Estado = true,
                    Mensaje = id
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Guarda la ruta de la imagen en la bd
        /// </summary>
        /// <param name="ruta"></param>
        /// <returns></returns>
        private void GuardarImagenContenido(PrincipalDto principalDto)
        {
            try
            {
                ImagenesDto imagenesDto = new ImagenesDto()
                {
                    Estado = true,
                    Fechacreacion = DateTime.Now,
                    Nombre = principalDto.Texto,
                    Ruta = principalDto.RutaImagen
                };
                Imagenes imagenes = mapper.Map<Imagenes>(imagenesDto);
                this.blogRepository.GuardarImagenPost(imagenes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Lista el contendido principal
        /// </summary>
        /// <returns></returns>
        public List<PrincipalDto> ListarContenidoPrincipal()
        {
            try
            {
                return this.principalRepository.ListarContenidoPrincipal();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Muestra el contendio principal
        /// </summary>
        /// <returns></returns>
        public PrincipalDto MostrarContenidoPrincipal()
        {
            try
            {
                return this.principalRepository.MostrarContenidoPrincipal();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Actualiza el contenido principal
        /// </summary>
        /// <param name="principalDto"></param>
        /// <returns></returns>
        public ApiCallResult ActualizarContenidoPrincipal(PrincipalDto principalDto)
        {
            try
            {
                PrincipalDto principal = this.principalRepository.ListarContenidoPrincipal().FirstOrDefault();
                Principal p = this.principalRepository.BuscarPorId(principalDto.Id);
                if (principalDto.Texto != null)
                {
                    Imagenes imagen = this.blogRepository.BuscarMultimedia(p.Idimagen);
                    principalDto.RutaImagen = principalDto.RutaImagen == null ? imagen.Ruta : principalDto.RutaImagen;
                    this.ActualizarImagenBaseDatos(principalDto, imagen);
                }
                if (principalDto.RutaImagen != null)
                {
                    Imagenes imagen = this.blogRepository.BuscarMultimedia(p.Idimagen);
                    if (imagen is null)
                        throw new NegocioExecption("Error al guardar la imagen, contacta con el admin, " +
                            "ningun dato se actualizo", 500);
                    this.EliminarImagenServidor(imagen.Ruta);
                    principalDto.Texto = principalDto.Texto == null ? imagen.Nombre : principalDto.Texto;
                    this.ActualizarImagenBaseDatos(principalDto, imagen);
                }
                p.Descripcion = principalDto.Descripcion ?? principal.Descripcion;
                p.Estado = true;
                p.Idimagen = p.Idimagen;
                this.principalRepository.ActualizarSeccion(p);
                return new ApiCallResult
                {
                    Estado = true,
                    Mensaje = "Actualizado con exito"
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Elimina un archivo del servidor
        /// </summary>
        /// <param name="fileName"></param>
        private void EliminarImagenServidor(string fileName)
        {
            try
            {
                fileName = fileName[18..];
                string file = Path.Combine("Resources", "Images", fileName);
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Actualiza la ruta de la imagen en la base de datos
        /// </summary>
        /// <param name="rutaImagen"></param>
        /// <param name="idImagen"></param>
        private void ActualizarImagenBaseDatos(PrincipalDto principalDto, Imagenes imagen)
        {
            try
            {
                if (imagen is null)
                {
                    throw new NegocioExecption("No se ha podido guardar bien la imagen", 500);
                }

                imagen.Nombre = principalDto.Texto;
                imagen.Ruta = principalDto.RutaImagen;

                this.blogRepository.ActualizarMultimedia(imagen);
            }
            catch (NegocioExecption)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
