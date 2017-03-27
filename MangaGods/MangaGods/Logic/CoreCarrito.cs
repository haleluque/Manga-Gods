using MangaGods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MangaGods.Logic
{
    /// <summary>
    /// Contiene la lógica del carrito de compra
    /// </summary>
    public class CoreCarrito : IDisposable
    {
        /// <summary>
        /// Representa el id de un nuevo carrito de compra
        /// </summary>
        public string IdCarrito { get; set; }
        public const string LlaveSesionCarrito = "CartId";
        private MangaContext _contexto;

        /// <summary>
        /// Constructor
        /// </summary>
        public CoreCarrito()
        {
            _contexto = new MangaContext();
        }

        /// <summary>
        /// Obtiene los carritos de compra
        /// </summary>
        /// <returns></returns>
        public List<Carrito> ConsultarCarros()
        {
            try
            {
                IdCarrito = ObtenerIdCarrito();
                return _contexto.Carrito.Where(c => c.IdCarrito == IdCarrito).ToList();
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultarTodosCarritos")?.ToString());
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorConsultarTodosCarritos")?.ToString());
            }
        }

        /// <summary>
        /// Obtiene el total a pagar de los carritos de compra
        /// </summary>
        /// <returns></returns>
        public decimal CalcularTotalPago()
        {
            try
            {
                IdCarrito = ObtenerIdCarrito();

                // Multiplica el precio del producto por la cantidad requerida
                // de cada manga para obtener el total a pagar del carrito
                var consulta = (from carrito in _contexto.Carrito
                                where carrito.IdCarrito == IdCarrito
                                select carrito);
                if (consulta.Any())
                {
                    return (decimal)(from items in consulta
                                     select (items.Cantidad * items.Manga.Precio)).Sum();
                }
                return decimal.Zero;
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorCalcularTotalCarrito")?.ToString());
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorCalcularTotalCarrito")?.ToString());
            }
        }

        /// <summary>
        /// Fuerza la consulta de un carrito de compra almacenado en sesion
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public CoreCarrito ObtenerCarro(HttpContext context)
        {
            using (var cart = new CoreCarrito())
            {
                cart.IdCarrito = cart.ObtenerIdCarrito();
                return cart;
            }
        }

        /// <summary>
        /// Asigna el id del carrito en sesión, bien el id del usuario logeado o
        /// asigna uno con un guid aleatorio
        /// </summary>
        /// <returns></returns>
        public string ObtenerIdCarrito()
        {
            try
            {
                if (HttpContext.Current.Session[LlaveSesionCarrito] != null)
                    return HttpContext.Current.Session[LlaveSesionCarrito].ToString();
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[LlaveSesionCarrito] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    var tempCartId = Guid.NewGuid();
                    HttpContext.Current.Session[LlaveSesionCarrito] = tempCartId.ToString();
                }
                return HttpContext.Current.Session[LlaveSesionCarrito].ToString();
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorAsociarIdCarrito")?.ToString());
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorAsociarIdCarrito")?.ToString());
            }
        }

        /// <summary>
        /// Agrega un manga al carrito de compra
        /// </summary>
        /// <param name="idManga"></param>
        public void AgregarManga(int idManga)
        {
            IdCarrito = ObtenerIdCarrito();

            try
            {
                //Obtiene de la base de datos si el carrito ya ha sido creado y si tiene el producto seleccionado
                var carro = _contexto.Carrito.SingleOrDefault(
                    c => c.IdCarrito == IdCarrito
                    && c.IdManga == idManga);

                if (carro == null)
                {
                    // Crea un nuevo carrito con el producto seleccionado si no existe             
                    carro = new Carrito
                    {
                        Id = Guid.NewGuid().ToString(),
                        IdManga = idManga,
                        IdCarrito = IdCarrito,
                        Manga = _contexto.Manga.SingleOrDefault(
                        p => p.Id == idManga),
                        Cantidad = 1,
                        FechaCreacion = DateTime.Now
                    };

                    _contexto.Carrito.Add(carro);
                }
                else
                {
                    //Si el carro ya existe, agrega uno a la cantidad              
                    carro.Cantidad++;
                }
                _contexto.SaveChanges();
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorAgregarProductoCarrito")?.ToString());
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorAgregarProductoCarrito")?.ToString());
            }
        }

        /// <summary>
        /// Actualiza los datos de un carrito de compra
        /// </summary>
        /// <param name="idCarro"></param>
        /// <param name="actualizaciones"></param>
        public void ActualizarCarroCompra(string idCarro, ActualizacionesCarrito[] actualizaciones)
        {
            try
            {
                int cartItemCount = actualizaciones.Count();
                var lista = ConsultarCarros();
                foreach (var carro in lista)
                {
                    // Recorre la lista de mangas para identificar los cambios
                    for (int i = 0; i < cartItemCount; i++)
                    {
                        if (carro.Manga.Id != actualizaciones[i].IdManga) continue;
                        if (actualizaciones[i].Cantidad < 1 || actualizaciones[i].QuitarManga)
                        {
                            RemoverMnaga(idCarro, carro.IdManga);
                        }
                        else
                        {
                            ActualizarDatos(idCarro, carro.IdManga, actualizaciones[i].Cantidad);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorActualizarCarrito")?.ToString());
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Quita un producto del carro de compra
        /// </summary>
        /// <param name="idCarro"></param>
        /// <param name="idManga"></param>
        public void RemoverMnaga(string idCarro, int idManga)
        {
            using (var db = new MangaContext())
            {
                try
                {
                    var manga = (from c in db.Carrito
                                 where c.IdCarrito == idCarro &&
                                       c.Manga.Id == idManga
                                 select c).FirstOrDefault();
                    if (manga == null) return;
                    // Remove Item.
                    db.Carrito.Remove(manga);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorBorrarProductoCarrito")?.ToString());
                    throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorBorrarProductoCarrito")?.ToString());
                }
            }
        }

        /// <summary>
        /// Actualiza los datos de los productos dentro de un carro de compra
        /// </summary>
        /// <param name="idCarro"></param>
        /// <param name="idManga"></param>
        /// <param name="nuevaCantidad"></param>
        public void ActualizarDatos(string idCarro, int idManga, int nuevaCantidad)
        {
            using (var db = new MangaContext())
            {
                try
                {
                    var manga = (from c in db.Carrito
                                 where c.IdCarrito == idCarro &&
                                       c.Manga.Id == idManga
                                 select c).FirstOrDefault();
                    if (manga == null) return;
                    manga.Cantidad = nuevaCantidad;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorActualizarCarrito")?.ToString());
                    throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorActualizarCarrito")?.ToString());
                }
            }
        }

        /// <summary>
        /// Vaciar un carro de compra
        /// </summary>
        public void VaciarCarro()
        {
            IdCarrito = ObtenerIdCarrito();

            try
            {
                var cartItems = _contexto.Carrito.Where(
                c => c.IdCarrito == IdCarrito);
                foreach (var manga in cartItems)
                {
                    _contexto.Carrito.Remove(manga);
                }
                // Save changes.
                _contexto.SaveChanges();
            }
            catch (Exception e)
            {
                ExceptionUtility.LogException(e, HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorVaciarCarroCompra")?.ToString());
                throw new Exception(HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorVaciarCarroCompra")?.ToString());
            }
        }

        /// <summary>
        /// Asocia un carro anónimo al usuario que se logea
        /// </summary>
        /// <param name="idCarro"></param>
        /// <param name="nombreUsuario"></param>
        public void AsociarCarroAUsuario(string idCarro, string nombreUsuario)
        {
            var compra = _contexto.Carrito.Where(c => c.IdCarrito == idCarro);
            foreach (var carro in compra)
            {
                carro.IdCarrito = nombreUsuario;
            }
            HttpContext.Current.Session[LlaveSesionCarrito] = nombreUsuario;
            _contexto.SaveChanges();
        }

        /// <summary>
        /// Método sobrecargado para liberar la conexión de la 
        /// base de datos cuando se terminan las operaciones
        /// </summary>
        public void Dispose()
        {
            if (_contexto == null) return;
            _contexto.Dispose();
            _contexto = null;
        }

        /// <summary>
        /// Estructura con los campos que se pueden actualizar en un producto de un carrito de 
        /// compra, como lo es la cantidad de items,
        /// 
        /// Recodar que una estructura es mas ligera que un objeto tradicional y no se le debe 
        /// crear constructor por defecto
        /// </summary>
        public struct ActualizacionesCarrito
        {
            public int IdManga;
            public int Cantidad;
            public bool QuitarManga;
        }
    }
}