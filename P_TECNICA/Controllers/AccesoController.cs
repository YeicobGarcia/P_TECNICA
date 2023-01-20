using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using P_TECNICA.Models;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using MySqlConnector;

namespace P_TECNICA.Controllers
{
    public class AccesoController : Controller
    {

        static string cadena = "server=localhost;port=3308;database=prueba;uid=root;password=root";
		MySqlConnection conexion = new MySqlConnection(cadena);

		public ActionResult Login()
        {
            return View();
        }

		// GET: Usuarios/Create
		public IActionResult Registrar()
		{
			return View();
		}


		[HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            bool registrado;
            string mensaje;

            if (oUsuario.Pass == oUsuario.ConfirmarClave)
            {
                oUsuario.Pass = ConvertirSha256(oUsuario.Pass);
            }
            else
            {
                ViewData["Mensaje"] = "Las Contraseñas no coinciden";
                return RedirectToAction("Privacy", "Home");
            }

            using (MySqlConnection cn = new MySqlConnection(cadena))
            {
				MySqlCommand cmd = new MySqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("Usuario", oUsuario.Username);
                cmd.Parameters.AddWithValue("Pass", oUsuario.Pass);
				cmd.Parameters.AddWithValue("nombre", oUsuario.Nombre);
				cmd.Parameters.AddWithValue("email", oUsuario.Email);
				cmd.Parameters.AddWithValue("fNacimiento", oUsuario.Fnacimiento);
				cmd.Parameters.Add("Registrado", MySqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", MySqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();

            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return View("Index", "Home");
            }
            else
            {
				return RedirectToAction("Privacy", "Home");
			}

        }

        [HttpPost]
        public ActionResult Login(Usuario oUsuario)
        {
            //oUsuario.Pass = ConvertirSha256(oUsuario.Pass);

            using (MySqlConnection cn = new MySqlConnection(cadena)) {

                MySqlCommand cmd = new MySqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Usuarios", oUsuario.Username);
                cmd.Parameters.AddWithValue("Passwords", oUsuario.Pass);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                oUsuario.IdUsuarios = (uint)Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cn.Dispose();
                
            }

			if (oUsuario.IdUsuarios == 0)
            {
				ViewData["Mensaje"] = "Usuario No Encontrado";
				return RedirectToAction("Index", "Home");

			}
            else
            {
				Session["usuario"] = oUsuario;
				return RedirectToAction("Index", "Usuarios");
			}


        }

        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
               using (SHA256 hash = SHA256.Create())
               {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                        Sb.Append(b.ToString("x2"));
               }
               return Sb.ToString();
        }
    }
}
