using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace Projeto2022.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                return Json(new { Msg = "Usuário já logado" });
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logar(string username, string senha, bool manterlogado)
        {

            SqlConnection conexao = new SqlConnection("server=localhost;database=mySkill;uid=usuario;password=senha");
            await conexao.OpenAsync();

            SqlCommand cmd = conexao.CreateCommand();
            cmd.CommandText = $"SELECT * from Usuarios WHERE usuario = '{username}' AND senha = '{senha}'";

            SqlDataReader reader = cmd.ExecuteReader();


            if (await reader.ReadAsync())
            {
                int usuarioId = reader.GetInt32(0);
                string nome = reader.GetString(1);

                List<Claim> direitosAcesso = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,usuarioId.ToString()),
                    new Claim(ClaimTypes.Name,nome)
                };

                var identity = new ClaimsIdentity(direitosAcesso,"Identity.Login");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });

                await HttpContext.SignInAsync(userPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = manterlogado,
                        ExpiresUtc = DateTime.Now.AddDays(1)
                    }) ;


                return Json(new { Msg = "Usuário logado com sucesso" });
            }


            return Json(new { Msg = "Usuário não encontrado, verifique as credenciais" });
        }

        public async Task<IActionResult> Logout()
        {
            if(User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Index","Login");
        }
    }
}
