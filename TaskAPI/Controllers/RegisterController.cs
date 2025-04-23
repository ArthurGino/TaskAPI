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
            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM Login WHERE Email = @Email", con);
            checkCmd.Parameters.AddWithValue("@Email", cadastro.Email);

            con.Open();
            int userExists = (int)checkCmd.ExecuteScalar();

            if (userExists > 0)
            {
                HttpContext.Response.StatusCode = 409; 
                con.Close();
                return "Usuário já existe";
            }

            SqlCommand cmd = new SqlCommand("INSERT INTO Login (Email, Password) VALUES (@Email, @Password)", con);
            cmd.Parameters.AddWithValue("@Email", cadastro.Email);
            cmd.Parameters.AddWithValue("@Password", cadastro.Password);

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
                using (SqlCommand cmd = new SqlCommand(@"
                            SELECT Id, Email, Password 
                            FROM Login WHERE Email = @Email AND Password = @Password", con))
                {
                    cmd.Parameters.AddWithValue("@Email", cadastro.Email);
                    cmd.Parameters.AddWithValue("@Password", cadastro.Password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var user = new Model.Register
                            {
                                Id = Convert.ToInt32(reader["Id"]), 
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                            };
                            return Ok(new { message = "Logado com sucesso", user = user });
                        }
                        else
                        {
                            return Unauthorized(new { message = "Usuário ou senha incorretos" });
                        }
                    }
                }
            }
        }
    }
}

