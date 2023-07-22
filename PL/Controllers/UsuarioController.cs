using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.OData.Edm;
using System.Data;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Inicio()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ValidarCuenta(string email, string contraseña)
        {
            BL.Usuario usuario = new BL.Usuario();
            dynamic result = new BL.Usuario();
            result = BL.Usuario.GetByEmail(email);
           
            if (result.Correct)
            {
                usuario = (BL.Usuario)result.Object;
                if (usuario.Contraseña == contraseña)
                {
                    return RedirectToAction("GetAll", "Usuario");
                }
                else
                {
                    ViewBag.Message = "Los datos son incorrectos";
                    return View("ModalLogin");
                }
            }

            return View(usuario);
        }

        public IActionResult GetAll()
        {
            BL.Usuario usuario = new BL.Usuario();
            dynamic result = BL.Usuario.GetAll();

            usuario.Usuarios = result.Objects;
            
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Delete(int idUsuario)
        {
            dynamic result = BL.Usuario.Delete(idUsuario);
            if (result.Correct)
            {
                ViewBag.Message = "El Usuario ha sido eliminado";
                return View("Modal");
            }
            else
            {
                ViewBag.Message = "El Usuario no ha sido eliminado";
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult Form(int? idUsuario)
        {

            BL.Usuario usuario = new BL.Usuario();

            dynamic resultUsuario = BL.Usuario.GetAll();

            if (idUsuario == null)
            {
                return View(usuario);
            }
            else
            {
                dynamic result = new BL.Usuario();
                result = BL.Usuario.GetById(idUsuario.Value);

                if (result.Correct)
                {

                    usuario = ((BL.Usuario)result.Object);
                }
            }
            return View(usuario);
        }
        [HttpPost]
        public ActionResult Form(BL.Usuario usuario)
        {
            dynamic result = new BL.Usuario();
            DateTime date = DateTime.Parse(usuario.FechaNacimiento);
            if (usuario.IdUsuario == 0)
            {
                usuario.FechaNacimiento = date.ToString("dd-MM-yyyy");
                result = BL.Usuario.Add(usuario);

                if (result.Correct)
                {
                    ViewBag.Message = "Se completo el registro satisfactoriamente";
                    return View("ModalLogin");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al insertar el registro";
                    return View("ModalLogin");
                }

            }
            else
            {

                //update
                result = BL.Usuario.Update(usuario);

                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo la información satisfactoriamente";

                    return View("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar el registro";

                    return View("Modal");
                }
            }


        }
    }

}
