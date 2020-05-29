using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using System.Security.Cryptography;


namespace Lautaro.PracticoMVC.AccesoDatos
{
    public class Usuarios
    {
        string cadenaConexion = Conexiones.ObtenerCadenaConexion();
        public List<Entidades.Join_UsuariosRoles> Listar()
        {
            List<Entidades.Join_UsuariosRoles> listaUsuariosRoles = new List<Entidades.Join_UsuariosRoles>();
            StringBuilder consultaSQL = new StringBuilder();
            consultaSQL.Append("SELECT ");
            consultaSQL.Append("Usuarios.Id AS ID, ");
            consultaSQL.Append("Usuario AS USUARIO, ");
            consultaSQL.Append("Roles.Descripcion AS ROL, ");
            consultaSQL.Append("Nombre AS NOMBRES, ");
            consultaSQL.Append("Apellido AS APELLIDOS, ");
            consultaSQL.Append("FechaCreacion AS FECHA_ALTA, ");
            consultaSQL.Append("Activo AS ESTADO ");
            consultaSQL.Append("FROM Usuarios ");
            consultaSQL.Append("INNER JOIN Roles ON  ");
            consultaSQL.Append("Usuarios.IdRol = Roles.Id ");
            using (var connection = new SqlConnection(cadenaConexion))
            {
                listaUsuariosRoles = connection.Query<Entidades.Join_UsuariosRoles>(consultaSQL.ToString()).ToList();
            }
            return listaUsuariosRoles;
        }
        public Entidades.Usuarios Detalle3(int id)
        {
            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("SELECT ");
            consultaSQL.Append("Id, IdRol, Usuario, Nombre, Apellido, Password, PasswordSalt, FechaCreacion, Activo ");
            consultaSQL.Append("FROM Usuarios ");
            consultaSQL.Append("WHERE Id = @idParametro ");
            using (var connection = new SqlConnection(cadenaConexion))
            {
                var objUsuario = connection.QuerySingleOrDefault<Entidades.Usuarios>(consultaSQL.ToString(), new { idParametro = id });
                return objUsuario;
            }
        }
        public Entidades.Join_UsuariosClientes Detalle(int id)
        {
            StringBuilder consultaSQL = new StringBuilder();
            consultaSQL.Append("SELECT ");
            consultaSQL.Append("Usuarios.Id AS ID_USUARIO, ");
            consultaSQL.Append("Usuarios.Usuario AS USERNAME, ");
            consultaSQL.Append("Usuarios.IdRol AS ID_ROL, ");
            consultaSQL.Append("Usuarios.Nombre AS NOMBRES, ");
            consultaSQL.Append("Usuarios.Apellido AS APELLIDOS, ");
            consultaSQL.Append("Clientes.RazonSocial AS RAZON_SOCIAL,  ");
            consultaSQL.Append("Usuarios.Activo AS ACTIVO ");
            consultaSQL.Append("FROM Usuarios ");
            consultaSQL.Append("LEFT JOIN Clientes ON ");
            consultaSQL.Append("Usuarios.Id = Clientes.IdUsuario ");
            consultaSQL.Append("WHERE Usuarios.Id =  @idParametro ");
            using (var connection = new SqlConnection(cadenaConexion))
            {
                var obj = connection.QuerySingleOrDefault<Entidades.Join_UsuariosClientes>(consultaSQL.ToString(), new { idParametro = id });
                return obj;
            }
        }
        public int Crear(Entidades.Join_UsuariosClientes obj)
        {
            int filasAfectadas = 0;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            var transaccion = conexion.BeginTransaction();
            try
            {
                string clave = obj.USERNAME;

                string claveSalt = GenerarPasswordSalt(clave);

                string claveHash = GenerarPasswordHash(clave, claveSalt);

                StringBuilder consultaSQL1 = new StringBuilder();
                consultaSQL1.Append("INSERT INTO Usuarios(IdRol, Usuario, Nombre, Apellido, Password, PasswordSalt, FechaCreacion, Activo)  ");
                consultaSQL1.Append("VALUES(@IdRol, @Usuario, @Nombre, @Apellido, @Password, @PasswordSalt, @FechaCreacion, @Activo); ");

                filasAfectadas = conexion.Execute(consultaSQL1.ToString(),
                       new
                       {
                           IdRol = obj.ID_ROL,
                           Usuario = obj.USERNAME,
                           Nombre = obj.NOMBRES,
                           Apellido = obj.APELLIDOS,
                           Password = claveHash,
                           PasswordSalt = claveSalt,
                           FechaCreacion = DateTime.Now,
                           Activo = obj.ACTIVO
                       }
                       , transaction: transaccion);

                if (obj.ID_ROL == "CLI")
                {
                    StringBuilder consultaSQL2 = new StringBuilder();

                    consultaSQL2.Append("SELECT Id FROM Usuarios ");
                    consultaSQL2.Append("WHERE Usuario LIKE @UsernameParametro ");

                    obj.ID_USUARIO = conexion.ExecuteScalar<int>(consultaSQL2.ToString(), new { UsernameParametro = obj.USERNAME }, transaction: transaccion);

                    StringBuilder consultaSQL3 = new StringBuilder();
                    consultaSQL3.Append("INSERT INTO Clientes(RazonSocial, FechaCreacion, IdUsuario)  ");
                    consultaSQL3.Append("VALUES (@RazonSocialParametro, @FechaCreacionParametro, @IdUsuarioParametro )  ");

                    filasAfectadas = conexion.Execute(consultaSQL3.ToString(),
                           new
                           {
                               RazonSocialParametro = obj.RAZON_SOCIAL,
                               IdUsuarioParametro = obj.ID_USUARIO,
                               FechaCreacionParametro = DateTime.Now,

                           },
                           transaction: transaccion);
                }
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                filasAfectadas = 0;
                transaccion.Rollback();

            }
            finally
            {
// close conection
                conexion.Close();
            }
            return filasAfectadas;
        }

// que quilombo ésta parte, basicamente estoy encriptando la password
        public static string GenerarPasswordSalt(string password)
        {
            if (password == null)
            {
                password = "(null)";
            }

            string passwordSalt;

            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            passwordSalt = Convert.ToBase64String(salt);

            return passwordSalt;
        }


        public static string GenerarPasswordHash(string password, string salt, string hashingAlgorithm = "HMACSHA256")
        {
            if (password == null || salt == null)
            {
                password = "(null)";
                salt = "(null)";
            }

            byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);
            var saltyPasswordBytes = new byte[saltBytes.Length + passwordBytes.Length];

            Buffer.BlockCopy(saltBytes, 0, saltyPasswordBytes, 0, saltBytes.Length);
            Buffer.BlockCopy(passwordBytes, 0, saltyPasswordBytes, saltBytes.Length, passwordBytes.Length);

            switch (hashingAlgorithm)
            {
                case "HMACSHA256":
                    return Convert.ToBase64String(new HMACSHA256(saltBytes).ComputeHash(saltyPasswordBytes));
                default:
                    HashAlgorithm algorithm = HashAlgorithm.Create(hashingAlgorithm);

                    if (algorithm != null)
                    {
                        return Convert.ToBase64String(algorithm.ComputeHash(saltyPasswordBytes));
                    }

                    throw new CryptographicException("Unknown hash algorithm");
            }
        }


        public int ActualizarPassword(string usuario, string claveActual)
        {
            int filasAfectadas = 0;

            string passwordSalt = GenerarPasswordSalt(claveActual);

            string passwordHash = GenerarPasswordHash(claveActual, passwordSalt);

            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("UPDATE Usuarios ");
            consultaSQL.Append("SET PasswordSalt = @passwordSaltParametro, Password = @passwordHashParametro ");
            consultaSQL.Append("WHERE Usuario LIKE @usuarioParametro ;");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                filasAfectadas = connection.Execute(consultaSQL.ToString(),
                   new
                   {
                       passwordSaltParametro = passwordSalt,
                       passwordHashParametro = passwordHash,
                       usuarioParametro = usuario
                   });
            }
            return filasAfectadas;
        }
        public bool VerificarUsuarioExistente(string usuario)
        {
            int contador;

            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("SELECT COUNT(*) FROM USUARIOS ");
            consultaSQL.Append("WHERE Usuario LIKE @parametroUsuario ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                contador = connection.ExecuteScalar<int>(consultaSQL.ToString(), new { parametroUsuario = usuario });
            }

            if (contador == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string ObtenerPasswordSaltPorUsuario(string usuario)
        {
            string passwordSalt = string.Empty;

            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("SELECT PasswordSalt FROM Usuarios ");
            consultaSQL.Append("WHERE Usuario LIKE @parametroUsuario ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                passwordSalt = connection.ExecuteScalar<string>(consultaSQL.ToString(), new { parametroUsuario = usuario });
            }
            return passwordSalt;
        }
        public bool VerificarPasswordBlanqueada(string usuario)
        {
            int contador;

            string passwordSalt, passwordCifrada;

            passwordSalt = ObtenerPasswordSaltPorUsuario(usuario);
            passwordCifrada = GenerarPasswordHash(usuario, passwordSalt);

            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("SELECT COUNT(*) FROM Usuarios ");
            consultaSQL.Append("WHERE Usuario LIKE @parametroUsuario ");
            consultaSQL.Append("AND Password LIKE @parametroClave ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                contador = connection.ExecuteScalar<int>(consultaSQL.ToString(), new { parametroUsuario = usuario, parametroClave = passwordCifrada });
            }
            if (contador == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int ValidarClaveActual(string usuario, string clave)
        {
            int contador = 0;

            string passwordSalt, passwordHash;

            passwordSalt = ObtenerPasswordSaltPorUsuario(usuario);
            passwordHash = GenerarPasswordHash(clave, passwordSalt);

            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("SELECT COUNT(*) FROM Usuarios ");
            consultaSQL.Append("WHERE Usuario LIKE @parametroUsuario ");
            consultaSQL.Append("AND Password LIKE  @parametroClave ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                return contador = connection.ExecuteScalar<int>(consultaSQL.ToString(), new { parametroUsuario = usuario, parametroClave = passwordHash });
            }
        }
        public Entidades.Usuarios ObtenerUsuarioPorUsername(string usuario)
        {
            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("SELECT ");
            consultaSQL.Append("Id, IdRol, Usuario, Nombre, Apellido, Password, PasswordSalt, FechaCreacion, Activo ");
            consultaSQL.Append("FROM Usuarios ");
            consultaSQL.Append("WHERE Usuario = @usuarioParametro ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                var objUsuario = connection.QuerySingleOrDefault<Entidades.Usuarios>(consultaSQL.ToString(), new { usuarioParametro = usuario });
                return objUsuario;
            }
        }
        public bool ValidarLogin(string usuario, string clave)
        {
            Entidades.Usuarios datosUsuario = ObtenerUsuarioPorUsername(usuario);

            if (datosUsuario == null)
            {
                return false;
            }
            if (datosUsuario.Usuario == null || datosUsuario.Password == null || datosUsuario.PasswordSalt == null || clave == null)
            { return false; }

            string passwordSalt = ObtenerPasswordSaltPorUsuario(usuario);
            string passwordHash = GenerarPasswordHash(clave, passwordSalt);

            clave = passwordHash;
            if (datosUsuario.Usuario == usuario && datosUsuario.Password == clave)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Entidades.Sesion ObtenerUsuarioSesion(string usuario)
        {
            Entidades.Sesion obj = new Entidades.Sesion();

            StringBuilder consultaSQL = new StringBuilder();

            consultaSQL.Append("SELECT ");
            consultaSQL.Append("Usuarios.Id AS ID_USUARIO, ");
            consultaSQL.Append("Usuarios.Usuario AS USERNAME, ");
            consultaSQL.Append("Roles.Id AS ID_ROL, ");
            consultaSQL.Append("Roles.Descripcion AS ROL_DESCRIPCION, ");
            consultaSQL.Append("Clientes.Codigo AS ID_CLIENTE, ");
            consultaSQL.Append("Usuarios.Nombre AS NOMBRES, ");
            consultaSQL.Append("Usuarios.Apellido AS APELLIDOS ");
            consultaSQL.Append("FROM Usuarios ");
            consultaSQL.Append("INNER JOIN Roles ON ");
            consultaSQL.Append("Usuarios.IdRol = Roles.Id ");
            consultaSQL.Append("LEFT JOIN Clientes ON ");
            consultaSQL.Append("Usuarios.Id = Clientes.IdUsuario ");
            consultaSQL.Append("WHERE Usuarios.Usuario LIKE @usuarioParametro ");

            using (var connection = new SqlConnection(cadenaConexion))
            {
                obj = connection.QuerySingleOrDefault<Entidades.Sesion>(consultaSQL.ToString(), new { usuarioParametro = usuario });
                return obj;
            }
        }
        public int ResetearClave(int idUsuario)
        {
            var obj = Detalle(idUsuario);
            int filasAfectadas = 0;
            string passwordSalt = GenerarPasswordSalt(obj.USERNAME);
            string passwordHash = GenerarPasswordHash(obj.USERNAME, passwordSalt);
            StringBuilder consultaSQL = new StringBuilder();
            consultaSQL.Append("UPDATE Usuarios ");
            consultaSQL.Append("SET PasswordSalt = @passwordSaltParametro, Password = @passwordHashParametro ");
            consultaSQL.Append("WHERE Id = @idParametro ;");
            using (var connection = new SqlConnection(cadenaConexion))
            {
                filasAfectadas = connection.Execute(consultaSQL.ToString(),
                   new
                   {
                       passwordSaltParametro = passwordSalt,
                       passwordHashParametro = passwordHash,
                       idParametro = idUsuario
                   });
            }
            return filasAfectadas;
        }
        public int Editar3(Entidades.Usuarios obj)
        {
            int filasAfectadas = 0;
            StringBuilder consultaSQL = new StringBuilder();
            consultaSQL.Append("UPDATE Usuarios ");
            consultaSQL.Append("SET IdRol = @idRolParametro,  ");
            consultaSQL.Append("Nombre = @nombreParametro, Apellido = @apellidoParametro, ");
            consultaSQL.Append("Activo = @activoParametro ");
            consultaSQL.Append("WHERE ID = @idParametro ");
            using (var connection = new SqlConnection(cadenaConexion))
            {
                filasAfectadas = connection.Execute(consultaSQL.ToString(),
                   new
                   {
                       idParametro = obj.Id,
                       idRolParametro = obj.IdRol,
                       nombreParametro = obj.Nombre,
                       apellidoParametro = obj.Apellido,
                       activoParametro = obj.Activo
                   });
            }
            return filasAfectadas;
        }
        public int Editar(Entidades.Join_UsuariosClientes obj)
        {
            int filasAfectadas = 0;
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            var transaccion = conexion.BeginTransaction();
            try
            {
                StringBuilder consultaSQL1 = new StringBuilder();
                consultaSQL1.Append("UPDATE Usuarios ");
                consultaSQL1.Append("SET IdRol = @idRolParametro,  ");
                consultaSQL1.Append("Nombre = @nombreParametro, Apellido = @apellidoParametro, ");
                consultaSQL1.Append("Activo = @activoParametro ");
                consultaSQL1.Append("WHERE ID = @idParametro ");
                filasAfectadas = conexion.Execute(consultaSQL1.ToString(),
                       new
                       {
                           idParametro = obj.ID_USUARIO,
                           idRolParametro = obj.ID_ROL,
                           nombreParametro = obj.NOMBRES,
                           apellidoParametro = obj.APELLIDOS,
                           activoParametro = obj.ACTIVO
                       }
                       , transaction: transaccion);
                if (obj.ID_ROL == "CLI")
                {
                    StringBuilder consultaSQL2 = new StringBuilder();
                    consultaSQL2.Append("UPDATE Clientes ");
                    consultaSQL2.Append("SET RazonSocial = @RazonSocialParametro ");
                    consultaSQL2.Append("WHERE IdUsuario = @IdUsuarioParametro ");

                    filasAfectadas = conexion.Execute(consultaSQL2.ToString(),
                           new
                           {
                               RazonSocialParametro = obj.RAZON_SOCIAL,
                               IdUsuarioParametro = obj.ID_USUARIO
                           },
                           transaction: transaccion);
                }
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                filasAfectadas = 0;
                transaccion.Rollback();
            }
            finally
            {
                conexion.Close();
            }
            return filasAfectadas;
        }
        public bool ConfirmarEliminacion(object id)
        {
            throw new NotImplementedException();
        }
        public void Desechar()
        {
            throw new NotImplementedException();
        }
        public void Deshabilitar(object id)
        {
            throw new NotImplementedException();
        }
        public void Eliminar(object id)
        {
            throw new NotImplementedException();
        }
        public void Guardar()
        {
            throw new NotImplementedException();
        }
    }
}
