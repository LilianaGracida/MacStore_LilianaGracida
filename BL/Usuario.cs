using Microsoft.Data.SqlClient;
using System.Data;

namespace BL
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno{ get; set; }
        public string ApellidoMaterno{ get; set; }
        public string FechaNacimiento {get; set;}
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public int Edad { get; set; }
        public List<object> Usuarios { get; set; }

        public bool Correct { get; set; }
        public string ErrorMessage { get; set; }
        public object Object { get; set; }
        public List<object> Objects { get; set; }
        public Exception Ex { get; set; }

        public static Usuario GetByEmail(string email)
        {
            Usuario usuario = new Usuario();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexión.GetConnectionString()))
                {
                    string query = ("UsuarioGetByEmail");

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[0].Value = email;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        DataTable tableUsuario = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            DataRow row = tableUsuario.Rows[0];

                            Usuario usuario1 = new Usuario();

                            usuario1.IdUsuario = int.Parse(row[0].ToString());
                            usuario1.Nombre = row[1].ToString();
                            usuario1.ApellidoPaterno = row[2].ToString();
                            usuario1.ApellidoMaterno = row[3].ToString();
                            usuario1.FechaNacimiento = row[4].ToString();
                            usuario1.Email = row[5].ToString();
                            usuario1.Contraseña = row[6].ToString();

                            
                            usuario.Object = usuario1;
                            usuario.Correct = true;
                        }
                        else
                        {
                            usuario.Correct = false;
                            usuario.ErrorMessage = " No existen registros en la tabla";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                usuario.Correct = false;
                usuario.ErrorMessage = ex.Message;
                usuario.Ex = ex;
            }

            return usuario;
        }
        public static Usuario Add(Usuario usuario)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexión.GetConnectionString()))
                {
                    string query = "UsuarioAdd";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] collection = new SqlParameter[6];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = usuario.Nombre;

                        collection[1] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = usuario.ApellidoPaterno;

                        collection[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = usuario.ApellidoMaterno;

                        collection[3] = new SqlParameter("FechaNacimiento", SqlDbType.Date);
                        collection[3].Value = usuario.FechaNacimiento;

                        collection[4] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[4].Value = usuario.ApellidoMaterno;

                        collection[5] = new SqlParameter("Contraseña", SqlDbType.VarChar);
                        collection[5].Value = usuario.Contraseña;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            usuario.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                usuario.Correct = false;
                usuario.Ex = ex;
                usuario.ErrorMessage = "Ocurrió un error al insertar el registro en la tabla Alumno" + usuario.Ex;
                //throw;
            }
            return usuario;
        }
        public static Usuario GetAll()  
        {
            Usuario result = new Usuario();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexión.GetConnectionString()))
                {
                    string query = "UsuarioGetAll";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        DataTable tableUsuario = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableUsuario.Rows)
                            {

                                Usuario usuario1 = new Usuario();
                                usuario1.IdUsuario = int.Parse(row[0].ToString());
                                usuario1.Nombre = row[1].ToString();
                                usuario1.ApellidoPaterno = row[2].ToString();
                                usuario1.ApellidoMaterno = row[3].ToString();
                                usuario1.FechaNacimiento = row[4].ToString();
                                usuario1.Email = row[5].ToString();
                                usuario1.Contraseña = row[6].ToString();
                                DateTime now = DateTime.Today;
                                DateTime date = DateTime.Parse(usuario1.FechaNacimiento);
                                int edad = DateTime.Today.Year - date.Year;
                                usuario1.Edad = edad;
                                result.Objects.Add(usuario1);
                            }

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al seleccionar los registros en la tabla ";
                        }

                    }

                }

            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }


            return result;
        }
        public static Usuario GetById(int idUsuario)
        {
            Usuario usuario = new Usuario();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexión.GetConnectionString()))
                {
                    string query = ("UsuarioGetById");

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = context;
                        cmd.CommandText = query;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
                        collection[0].Value = idUsuario;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();

                        DataTable tableUsuario = new DataTable();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);

                        da.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            DataRow row = tableUsuario.Rows[0];

                            Usuario usuario1 = new Usuario();

                            usuario1.IdUsuario = int.Parse(row[0].ToString());
                            usuario1.Nombre = row[1].ToString();
                            usuario1.ApellidoPaterno = row[2].ToString();
                            usuario1.ApellidoMaterno = row[3].ToString();
                            usuario1.FechaNacimiento = row[4].ToString();
                            usuario1.Email = row[5].ToString();
                            usuario1.Contraseña = row[6].ToString();

                            usuario.Object = usuario1;
                            usuario.Correct = true;
                        }
                        else
                        {
                            usuario.Correct = false;
                            usuario.ErrorMessage = " No existen registros en la tabla";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                usuario.Correct = false;
                usuario.ErrorMessage = ex.Message;
                usuario.Ex = ex;
            }

            return usuario;
        }
        public static Usuario Delete(int idUsuario)
        {
           Usuario result = new Usuario();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexión.GetConnectionString()))
                {
                    string query = @"UsuarioDelete";

                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdAlumno", SqlDbType.Int);
                        collection[0].Value = idUsuario;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery(); 

                        if (RowsAffected >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al eliminar el registro";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static Usuario Update(Usuario usuario)
        {
            
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexión.GetConnectionString()))
                {
                    string query = "UsuarioUpdate";

                    using (SqlCommand cmd = new SqlCommand())
                    {

                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;


                        SqlParameter[] collection = new SqlParameter[7];

                        collection[0] = new SqlParameter("IdUsuario", SqlDbType.Int);
                        collection[0].Value = usuario.IdUsuario;

                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = usuario.Nombre;

                        collection[2] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[2].Value = usuario.ApellidoPaterno;

                        collection[3] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[3].Value = usuario.ApellidoMaterno;

                        collection[4] = new SqlParameter("FechaNacimiento", SqlDbType.Date);
                        collection[4].Value = usuario.FechaNacimiento;

                        collection[5] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[5].Value = usuario.ApellidoMaterno;

                        collection[6] = new SqlParameter("Contraseña", SqlDbType.VarChar);
                        collection[6].Value = usuario.Contraseña;

                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected >= 1)
                        {
                            usuario.Correct = true;
                        }
                        else
                        {
                            usuario.Correct = false;
                            usuario.ErrorMessage = "Ocurrió un error al editar el registro";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                usuario.Correct = false;
                usuario.ErrorMessage = ex.Message;
            }
            return usuario;
        }
    }



    
}