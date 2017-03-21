﻿using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using MangaGods.Logic;

namespace MangaGods
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Se crea el rol admin que será el que administre la app
            var roleActions = new RoleActions();
            roleActions.CrearUsuarioAdmin();

            // Crea las rutas personalizadas del programa
            RegisterCustomRoutes(RouteTable.Routes);
        }

        /// <summary>
        /// Registra las rutas del aplicativo
        /// </summary>
        /// <param name="routes"></param>
        private void RegisterCustomRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
            "RutaAdminGeneros",
            "AdminGeneros",
            "~/Views/Administrador/AdminGenero.aspx"
            );

            routes.MapPageRoute(
            "RutaAdminAutores",
            "AdminAutores",
            "~/Views/Administrador/AdminAutores.aspx"
            );

            routes.MapPageRoute(
            "RutaAdminManga",
            "AdminMangas",
            "~/Views/Administrador/AdminMangas.aspx"
            );

            routes.MapPageRoute(
            "RutaGeneros",
            "Generos/{Nombre}",
            "~/Views/Generos.aspx"
            );

            routes.MapPageRoute(
            "RutaDetalleManga",
            "DetalleManga/{Nombre}",
            "~/Views/DetalleManga.aspx"
            );

            routes.MapPageRoute(
            "RutaCarritoCompraD",
            "CarritoCompra/{Id}",
            "~/Views/CarritoCompra.aspx"
            );

            routes.MapPageRoute(
            "RutaCarritoCompra",
            "CarritoCompra",
            "~/Views/CarritoCompra.aspx"
            );
        }
    }
}