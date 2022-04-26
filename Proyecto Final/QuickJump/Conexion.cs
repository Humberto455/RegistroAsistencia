using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuickJump
{
    class Conexion
    {
        private SqlConnection conectada = new SqlConnection("Data Source=.;Initial Catalog=BD_QJ;Integrated Security=true");
        //Construccion de la conexion a la base de datos
        public void Conectar()
        {
            try
            {
                conectada.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Desconectar()
        {
            conectada.Close();
        }

        //Creacion de metodos a utlizar en la aplicacion
        public DataTable ConsultaAsistencia()
        {
            DataTable dt = new DataTable();
            Conectar();
            try
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Alumno", conectada);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Desconectar();
            }

            return dt;
        }

        public DataTable CantidadFaltas()
        {
            DataTable dt = new DataTable();
            Conectar();
            try
            {
                SqlCommand comando = new SqlCommand("SELECT COUNT(Asistencia) AS Cant_Faltas, IDalumno, IDmaestro FROM Asistencia WHERE Asistencia = 'F' GROUP BY IDmaestro,IDalumno;", conectada);
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Desconectar();
            }

            return dt;
        }
        public DataTable CantidadFaltasAlumno(int clave)
        {
            DataTable dt = new DataTable();
            Conectar();
            try
            {
                SqlCommand comando = new SqlCommand("SELECT COUNT(Asistencia) AS Cant_Faltas, IDalumno, IDmaestro FROM Asistencia WHERE Asistencia = 'F' and IDalumno=@IDalumno GROUP BY IDmaestro,IDalumno;", conectada);
                comando.Parameters.Add("@IDalumno", SqlDbType.Int, 4).Value = clave;
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Desconectar();
            }

            return dt;
        }

        //ConsultaAlumno();
        public DataTable ConsultaAlumno(int mat)
        {
            DataTable dt = new DataTable();
            Conectar();
            try
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Alumno WHERE Matricula= @Matricula", conectada);
                comando.Parameters.Add("@Matricula", SqlDbType.Int,4).Value = mat;
                SqlDataAdapter da = new SqlDataAdapter(comando); 
                da.Fill(dt);
            }
            catch (FormatException)
            {
                Console.WriteLine("El tipo de formato de la matricula es incorrecto!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Desconectar();
            }

            return dt;
        }
        //-ConsultaPorAlumno()
        public DataTable ConsultaPorAlumno(int Clave_Maestro, int Clave_Alumno, string fecha)
        {
            DataTable dt = new DataTable();
            Conectar();
            try
            {
                SqlCommand comando = new SqlCommand("SELECT * FROM Asistencia WHERE IDmaestro = @IDmaestro and IDalumno = @IDalumno and Fecha = @Fecha;", conectada);
                comando.Parameters.Add("@IDmaestro", SqlDbType.Int, 4).Value = Clave_Maestro;
                comando.Parameters.Add("@IDalumno", SqlDbType.Int, 4).Value = Clave_Alumno;
                comando.Parameters.Add("@Fecha", SqlDbType.Date, 8).Value = fecha;//asi es la Conversion de una fecha
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(dt);
            }
            catch (FormatException)
            {
                Console.WriteLine("Uno de los datos a sido ingresado con un formato incorrecto!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Desconectar();
            }
           
            return dt;
        }

        //-AltasAlumnos()\\Insert
        public void Altas(string nom, string apeP, string apeM, string correo, string carre,string grupo, string cel, int edad, string fech)
        {
            Conectar();
            try
            {
                //iniciar comando
                SqlCommand cmd = new SqlCommand("Insert Into Alumno Values(@Nombre, @ApellidoPa,@ApellidoMa,@Correo,@Carrera,@Grupo,@Celular, @Edad,@FechaNac)", conectada);
                //conversion de datos
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 20).Value = nom;
                cmd.Parameters.Add("@ApellidoPa", SqlDbType.VarChar, 20).Value = apeP;
                cmd.Parameters.Add("@ApellidoMa", SqlDbType.VarChar, 20).Value = apeM;
                cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 50).Value = correo;
                cmd.Parameters.Add("@Carrera", SqlDbType.VarChar, 50).Value = carre;
                cmd.Parameters.Add("@Grupo", SqlDbType.Char, 2).Value = grupo;
                cmd.Parameters.Add("@Celular", SqlDbType.VarChar, 10).Value = cel;
                cmd.Parameters.Add("@Edad", SqlDbType.Int, 3).Value = edad;
                cmd.Parameters.Add("@FechaNac", SqlDbType.Date,8).Value = fech;
                //ejecutar el comando inicial
                cmd.ExecuteNonQuery();
            }
            catch (FormatException)
            {
                Console.WriteLine("Uno de los datos a sido ingresado con un formato incorrecto!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }
        //-BajasAlumnos()\\Delete
        public void BajaAlumno(int matricula)
        {
            Conectar();
            try
            {
                SqlCommand comando = new SqlCommand("DELETE FROM Alumno WHERE Matricula = @Matricula", conectada);
                comando.Parameters.Add("@Matricula", SqlDbType.Int, 4).Value = matricula;
                comando.ExecuteNonQuery();

            }
            catch (FormatException)
            {
                Console.WriteLine("El tipo de formato de la matricula es incorrecto!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }
        //-TomaAsistencia()\\Insert
        public void TomaAsistencia(int Clave_Maestro, int Clave_Alumno, string fecha, string asistencia)
        {
            Conectar();
            try
            {
                SqlCommand comando = new SqlCommand("sp_add_Asistencia", conectada);
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@IDmaestro", SqlDbType.Int, 4).Value = Clave_Maestro;
                comando.Parameters.Add("@IDalumno", SqlDbType.Int, 4).Value = Clave_Alumno;
                comando.Parameters.Add("@Fecha", SqlDbType.Date, 8).Value = fecha;//asi es la Conversion de una fecha
                comando.Parameters.Add("@Asistencia", SqlDbType.Char, 1).Value = asistencia;
                comando.ExecuteNonQuery();
            }
            catch (FormatException)
            {
                Console.WriteLine("Uno de los datos a sido ingresado con un formato incorrecto!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }
        //-CambioAsistencia(IDalumno,fecha)\\Update
        public void CambioAsistencia(int Clave_Maestro, int Clave_Alumno, string fecha, string asistencia)
        {
            Conectar();
            try
            {
                SqlCommand comando = new SqlCommand("UPDATE Asistencia SET Asistencia = @Asistencia WHERE IDmaestro = @IDmaestro and IDalumno = @IDalumno and Fecha = @Fecha;", conectada);
                comando.Parameters.Add("@IDmaestro", SqlDbType.Int, 4).Value = Clave_Maestro;
                comando.Parameters.Add("@IDalumno", SqlDbType.Int, 4).Value = Clave_Alumno;
                comando.Parameters.Add("@Fecha", SqlDbType.Date, 8).Value = fecha;//asi es la Conversion de una fecha
                comando.Parameters.Add("@Asistencia", SqlDbType.Char, 1).Value = asistencia;
                comando.ExecuteNonQuery();
            }
            catch (FormatException)
            {
                Console.WriteLine("Uno de los datos a sido ingresado con un formato incorrecto!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }
        //-CambioAlumno()\\Update
        public void CambioAlumno(int mat, string nom, string apeP, string apeM, string correo,string grupo, string carre, string cel, int edad, string fech)
        {
            Conectar();
            try
            {

                SqlCommand cmd = new SqlCommand("UPDATE Alumno Set Nombre=@Nombre, ApellidoPa=@ApellidoPa,ApellidoMa=@ApellidoMa,Correo=@Correo,Grupo=@Grupo,Carrera=@Carrera,Celular=@Celular, Edad=@Edad,FechaNac=@FechaNac WHERE Matricula=@Matricula", conectada);

                cmd.Parameters.Add("@Matricula", SqlDbType.Int).Value = mat;
                cmd.Parameters.Add("@Nombre", SqlDbType.VarChar, 20).Value = nom;
                cmd.Parameters.Add("@ApellidoPa", SqlDbType.VarChar, 20).Value = apeP;
                cmd.Parameters.Add("@ApellidoMa", SqlDbType.VarChar, 20).Value = apeM;
                cmd.Parameters.Add("@Correo", SqlDbType.VarChar, 50).Value = correo;
                cmd.Parameters.Add("@Carrera", SqlDbType.VarChar, 50).Value = carre;
                cmd.Parameters.Add("@Grupo", SqlDbType.Char, 2).Value = grupo;
                cmd.Parameters.Add("@Celular", SqlDbType.VarChar, 10).Value = cel;
                cmd.Parameters.Add("@Edad", SqlDbType.Int, 3).Value = edad;
                cmd.Parameters.Add("@FechaNac", SqlDbType.DateTime,8).Value = fech;
                cmd.ExecuteNonQuery();
            }
            catch (FormatException)
            {
                Console.WriteLine("Uno de los datos a sido ingresado con un formato incorrecto!");
                Console.ReadKey();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                Desconectar();
            }
        }
        public void DeleteRegistros(int mat)
        {
            Conectar();
            try
            {
                SqlCommand comando = new SqlCommand("DELETE FROM Asistencia WHERE IDalumno = @IDalumno",conectada);
                comando.Parameters.Add("@IDalumno", SqlDbType.Int, 4).Value = mat;
                comando.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Desconectar();
            }
        }
    }
}
