using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TaskAPI.Model;

namespace TaskAPI.Controllers
{
    [Route("Singup")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        [Route("register")]
        public string registrar(Register cadastro)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Login (Email, Password) VALUES ('" + cadastro.Email + "', '" + cadastro.Password + "' )", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return "Usuario cadastrado";
            }
            else
            {
                return "Erro ao cadastrar";
            }


        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Register cadastro)
        {
            using (SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Login WHERE Email = @Email AND Password = @Password", con))
                {
                    cmd.Parameters.AddWithValue("@Email", cadastro.Email);
                    cmd.Parameters.AddWithValue("@Password", cadastro.Password);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        return Ok(new { message = "Logado com sucesso" });
                    }
                    else
                    {
                        return Unauthorized(new { error = "Usuário ou senha incorretos" });
                    }
                }
            }

        }
  
    }
}

