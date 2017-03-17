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
        private MangaContext contexto;

        /// <summary>
        /// Constructor
        /// </summary>
        public CoreCarrito()
        {
            contexto = new MangaContext();
        }

        /// <summary>
        /// Obtiene los carritos de compra
        /// </summary>
        /// <returns></returns>
        public List<Carrito> ConsultarCarros()
        {
            IdCarrito = ObtenerIdCarrito();
            return contexto.Carrito.Where(
            c => c.IdCarrito == IdCarrito).ToList();
        }

        /// <summary>
        /// Obtiene el total a pagar de los carritos de compra
        /// </summary>
        /// <returns></returns>
        public decimal CalcularTotalPago()
        {
            IdCarrito = ObtenerIdCarrito();

            // Multiplica el precio del producto por la cantidad requerida
            // de cada manga para obtener el total a pagar del carrito
            var consulta = (from carrito in contexto.Carrito
                            where carrito.IdCarrito == IdCarrito
                            select carrito);
            if (consulta.Count() > 0)
            {
                return (decimal)(from items in consulta
                        select (items.Cantidad * items.Manga.Precio)).Sum();
            }
            else
            {
                return decimal.Zero;
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
            if (HttpContext.Current.Session[LlaveSesionCarrito] == null)
            {
                if (!string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name))
                {
                    HttpContext.Current.Session[LlaveSesionCarrito] = HttpContext.Current.User.Identity.Name;
                }
                else
                {
                    Guid tempCartId = Guid.NewGuid();
                    HttpContext.Current.Session[LlaveSesionCarrito] = tempCartId.ToString();
                }
            }
            return HttpContext.Current.Session[LlaveSesionCarrito].ToString();
        }

        /// <summary>
        /// Agrega un manga al carrito de compra
        /// </summary>
        /// <param name="idManga"></param>
        public void AgregarManga(int idManga)
        {
            IdCarrito = ObtenerIdCarrito();

            //Obtiene de la base de datos si el carrito ya ha sido creado y si tiene el producto seleccionado
            var carro = contexto.Carrito.SingleOrDefault(
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
                    Manga = contexto.Manga.SingleOrDefault(
                    p => p.Id == idManga),
                    Cantidad = 1,
                    FechaCreacion = DateTime.Now
                };

                contexto.Carrito.Add(carro);
            }
            else
            {
                //Si el carro ya existe, agrega uno a la cantidad              
                carro.Cantidad++;
            }
            contexto.SaveChanges();

        }

        /// <summary>
        /// Actualiza los datos de un carrito de compra
        /// </summary>
        /// <param name="cartId"></param>
        /// <param name="actualizaciones"></param>
        public void ActualizarCarroCompra(string idCarro, ActualizacionesCarrito[] actualizaciones)
        {
            using (var db = new MangaContext())
            {
                try
                {
                    int CartItemCount = actualizaciones.Count();
                    List<Carrito> lista = ConsultarCarros();
                    foreach (var carro in lista)
                    {
                        // Recorre la lista de mangas para identificar los cambios
                        for (int i = 0; i < CartItemCount; i++)
                        {
                            if (carro.Manga.Id == actualizaciones[i].IdManga)
                            {
                                if (actualizaciones[i].Cantidad < 1 || actualizaciones[i].QuitarManga == true)
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
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: Ha ocurrido un error al actualizar el carrito - " + exp.Message.ToString(), exp);
                }
            }
        }

        /// <summary>
        /// Quita un producto del carro de compra
        /// </summary>
        /// <param name="idCarro"></param>
        /// <param name="idManga"></param>
        public void RemoverMnaga(string idCarro, int idManga)
        {
            using (var _db = new MangaContext())
            {
                try
                {
                    var manga = (from c in _db.Carrito
                                 where c.IdCarrito == idCarro &&
                                       c.Manga.Id == idManga
                                 select c).FirstOrDefault();
                    if (manga != null)
                    {
                        // Remove Item.
                        _db.Carrito.Remove(manga);
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: No se pudo eliminar manga del carro - " + exp.Message.ToString(), exp);
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
            using (var _db = new MangaContext())
            {
                try
                {
                    var manga = (from c in _db.Carrito
                                 where c.IdCarrito == idCarro &&
                                       c.Manga.Id == idManga
                                 select c).FirstOrDefault();
                    if (manga != null)
                    {
                        manga.Cantidad = nuevaCantidad;
                        _db.SaveChanges();
                    }
                }
                catch (Exception exp)
                {
                    throw new Exception("ERROR: No se pudo actualizar los datos de los productos - " + exp.Message.ToString(), exp);
                }
            }
        }

        /// <summary>
        /// Vaciar un carro de compra
        /// </summary>
        public void VaciarCarro()
        {
            IdCarrito = ObtenerIdCarrito();
            var cartItems = contexto.Carrito.Where(
            c => c.IdCarrito == IdCarrito);
            foreach (var manga in cartItems)
            {
                contexto.Carrito.Remove(manga);
            }
            // Save changes.
            contexto.SaveChanges();
        }

        /// <summary>
        /// Asocia un carro anónimo al usuario que se logea
        /// </summary>
        /// <param name="idCarro"></param>
        /// <param name="NombreUsuario"></param>
        public void AsociarCarroAUsuario(string idCarro, string NombreUsuario)
        {
            var compra = contexto.Carrito.Where(c => c.IdCarrito == idCarro);
            foreach (var carro in compra)
            {
                carro.IdCarrito = NombreUsuario;
            }
            HttpContext.Current.Session[LlaveSesionCarrito] = NombreUsuario;
            contexto.SaveChanges();
        }

        /// <summary>
        /// Método sobrecargado para liberar la conexión de la 
        /// base de datos cuando se terminan las operaciones
        /// </summary>
        public void Dispose()
        {
            if (contexto != null)
            {
                contexto.Dispose();
                contexto = null;
            }
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