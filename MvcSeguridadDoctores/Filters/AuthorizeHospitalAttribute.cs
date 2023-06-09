﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MvcSeguridadDoctores.Filters {
    public class AuthorizeHospitalAttribute : AuthorizeAttribute, IAuthorizationFilter {
        public void OnAuthorization(AuthorizationFilterContext context) {
            //NOS DA IGUAL QUIEN SE HA VALIDADO POR AHORA
            var user = context.HttpContext.User;

            //NECESITAMOS EL CONTROLLER Y NECESITAMOS EL ACTION
            string controller = context.RouteData.Values["controller"].ToString();

            string action = context.RouteData.Values["action"].ToString();

            string idenfermo = "";
            if (context.RouteData.Values.ContainsKey("inscripcion")) {
                idenfermo = context.RouteData.Values["inscripcion"].ToString();
            }

            //LA INFORMACION ESTARA DENTRO DE TEMPDATA
            //NECESITAMOS EL PROVEEDOR DE TEMPDATA DE LA APP
            //NUESTRO PROVEEDOR ESTA INYECTADO Y NECESITAMOS RECUPERAR UN OBJETO INYECTADO
            //RepositoryEmpleados repo = context.HttpContext.RequestServices.GetService<RepositoryEmpleados>();
            ITempDataProvider provider = context.HttpContext.RequestServices.GetService<ITempDataProvider>();

            //A PARTIR DEL PROVEEDOR, DEBEMOS RECUPERAR TEMPDATA QUE UTILIZA NUESTRA APP
            var TempData = provider.LoadTempData(context.HttpContext);

            TempData["controller"] = controller;
            TempData["action"] = action;
            TempData["inscripcion"] = idenfermo;

            //ALMACENAMOS NUESTRO TEMPDATA DENTRO DE LA APP
            provider.SaveTempData(context.HttpContext, TempData);

            if (user.Identity.IsAuthenticated == false) {
                context.Result = this.GetRoute("Managed", "LogIn");
            }
        }

        //COMO HAREMOS VARIAS REDIRECCIONES, CREAMOS UN NUEVO METODO PARA CREAR LAS RUTAS
        private RedirectToRouteResult GetRoute(string controller, string action) {
            RouteValueDictionary ruta = new RouteValueDictionary(new { controller = controller, action = action });

            RedirectToRouteResult result = new RedirectToRouteResult(ruta);

            return result;
        }
    }
}
