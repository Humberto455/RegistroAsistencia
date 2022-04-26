using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QuickJump
{
    class Funcion:Proceso
    {
        public Funcion()
        {

        }
        public void alta()
        {
            string nombre, apeP, apeM, correo, carre, cel, fech,grup;
            int edad;

            Console.WriteLine("\tREGISTRO DE ALUMNO\n");

            try
            {
                Console.WriteLine("Ingresa el Nombre del Alumno");
                nombre = Console.ReadLine();

                Console.WriteLine("Ingresa el apellido paterno del Alumno");
                apeP = Console.ReadLine();

                Console.WriteLine("Ingresa el apellido materno del Alumno");
                apeM = Console.ReadLine();

                Console.WriteLine("Ingresa el correo del alumno");
                correo = Console.ReadLine();

                Console.WriteLine("Ingresa la carrera del alumno");
                carre = Console.ReadLine();

                Console.WriteLine("Ingresa el grupo del alumno");
                grup = Console.ReadLine();

                Console.WriteLine("Ingresa el celular del alumno");
                cel = Console.ReadLine();

                Console.WriteLine("Ingresa la Edad del Alumno");
                edad = int.Parse(Console.ReadLine());

                Console.WriteLine("Ingresa la fecha de nacimiento del alumno Ejemplo: 2000-01-10");
                fech = Console.ReadLine();

                con.Altas(nombre, apeP, apeM, correo, carre, grup, cel, edad, fech);
                Console.WriteLine("Alta Exitosa!");

                Console.ReadKey();
            }
            catch (FormatException)
            {
                Console.WriteLine("\tEl registro a sido cancelado!\n");
                Console.WriteLine("Uno de los datos a sido ingresado con un formato incorrecto!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void baja()
        {
            int mat;
            try
            {
                Console.WriteLine("\tELIMINAR ALUMNO\n");

                Console.WriteLine("Ingresa la matricula del alumno deseado para dar de BAJA");
                mat = int.Parse(Console.ReadLine());
                con.DeleteRegistros(mat);
                con.BajaAlumno(mat);
                Console.WriteLine("El alumno ha sido dado de Baja Correctamente!");

                Console.ReadKey();
            }
            catch (FormatException)
            {
                Console.WriteLine("\tEl proceso dar de baja a sido cancelado!\n");
                Console.WriteLine("El tipo de formato de la matricula es incorrecto!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void ModificarAlumno()
        {
            string nom,apeP, apeM, correo, carre, cel, fech,grupo;
            int edad, mat, resp;

            
                Console.WriteLine("\tACTUALIZACIÓN DE DATOS DEL ALUMNO\n");
            try
            {
                Console.WriteLine("Ingresa la Matricula del Alumno");
                mat = int.Parse(Console.ReadLine());



                dt = con.ConsultaAlumno(mat);

                if (dt.Rows.Count == 0)
                {
                    Console.WriteLine("Este Alumno no existe en la base de datos");
                }
                else
                {
                    nom = dt.Rows[0][1].ToString();
                    apeP = dt.Rows[0][2].ToString();
                    apeM = dt.Rows[0][3].ToString();
                    correo = dt.Rows[0][4].ToString();
                    carre = dt.Rows[0][5].ToString();
                    grupo = dt.Rows[0][6].ToString();
                    cel = dt.Rows[0][7].ToString();
                    edad = Convert.ToInt32(dt.Rows[0][8].ToString());
                    fech = dt.Rows[0][9].ToString();

                    Console.WriteLine("Nombre: {0}\nApellido Paterno: {1}\nApellido Materno: {2}\nCorreo: {3}\nCarrera: {4}\nCelular: {5}\nEdad: {6}\n Fecha Nacimiento: {7} ", nom, apeP, apeM, correo, carre, cel, edad, fech);
                    Console.WriteLine("\nSeleccione lo que desea modificar\n");
                    Console.WriteLine("1. Nombre");
                    Console.WriteLine("2. Apellido Paterno");
                    Console.WriteLine("3. Apellido Materno");
                    Console.WriteLine("4. Correo");
                    Console.WriteLine("5. Carrera");
                    Console.WriteLine("6. Grupo");
                    Console.WriteLine("7. Celular");
                    Console.WriteLine("8. Edad");
                    Console.WriteLine("9. Fecha Nacimiento");
                    resp = Convert.ToInt32(Console.ReadLine());

                    switch (resp)
                    {
                        case 1:
                            Console.WriteLine("Capture el nombre ");
                            nom = Console.ReadLine();
                            break;
                        case 2:
                            Console.WriteLine("Capture el apellido Paterno");
                            apeP = Console.ReadLine();
                            break;
                        case 3:
                            Console.WriteLine("Capture el apellido Materno");
                            apeM = Console.ReadLine();
                            break;
                        case 4:
                            Console.WriteLine("Capture el Correo");
                            correo = Console.ReadLine();
                            break;
                        case 5:
                            Console.WriteLine("Capture la Carrera");
                            carre = Console.ReadLine();
                            break;
                        case 6:
                            Console.WriteLine("Capture el Grupo");
                            grupo = Console.ReadLine();
                            break;
                        case 7:
                            Console.WriteLine("Capture el Celular");
                            cel = Console.ReadLine();
                            break;
                        case 8:
                            Console.WriteLine("Capture la edad");
                            edad = Convert.ToInt32(Console.ReadLine());
                            break;
                        case 9:
                            Console.WriteLine("Capture la fecha de nacimiento del alumno");
                            fech = Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("Opción Incorrecta");
                            break;
                    }
                    con.CambioAlumno(mat, nom, apeP, apeM, correo, carre, grupo, cel, edad, fech);
                    Console.WriteLine("Cambios Realizados");
                }
                Console.ReadKey();
            }
            catch (FormatException)
            {
                Console.WriteLine("\tEl proceso actualización de datos del alumno a sido cancelado!\n");
                Console.WriteLine("Uno de los datos a sido ingresado con un formato incorrecto!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void RegistrarAsistencia()
        {
            int CM, CA;
            string fecha, asistencia;

            
            Console.WriteLine("\tREGISTRO DE ASISTENCIA\n");

            dt = con.ConsultaAsistencia();


            int i = 0;
            Console.WriteLine($"{dt.Columns[0].ToString()}:   Nombre Completo:");
            foreach (DataRow fila in dt.Rows)
            {
                Console.WriteLine($"   {dt.Rows[i][0].ToString()}      {dt.Rows[i][1].ToString()} {dt.Rows[i][2].ToString()} {dt.Rows[i][3].ToString()}");
                i++;
            }

            Console.WriteLine(con.ConsultaAsistencia());
            try
            {
                Console.WriteLine("Ingresa ID maestro");
                CM = int.Parse(Console.ReadLine());

                Console.WriteLine("Ingresa ID alumno");
                CA = int.Parse(Console.ReadLine());

                Console.WriteLine("Ingresa fecha ejemplo: 2000-01-01");
                fecha = Console.ReadLine();

                Console.WriteLine("Ingresa Asistencia");
                asistencia = Console.ReadLine();


                con.TomaAsistencia(CM, CA, fecha, asistencia);
                Console.WriteLine("Asistencia Tomada!");

                Console.ReadKey();
            }
            catch (FormatException)
            {
                Console.WriteLine("\tEl proceso a sido cancelado!\n");
                Console.WriteLine("Uno de los datos a sido ingresado con un formato incorrecto!");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void ModificarAsistencia()
        {
            int Clave_Maestro, Clave_Alumno, res;
            string fecha, asistencia;

            Console.WriteLine("\tACTUALIZACION DE ASISTENCIA DEL ALUMNO\n");

            Console.WriteLine("Ingresa la matricula del maestro");
            Clave_Maestro = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingresa la matricula del Alumno");
            Clave_Alumno = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingresa fecha ejemplo: 2000-01-01");
            fecha = Console.ReadLine();

            dt = con.ConsultaPorAlumno(Clave_Maestro, Clave_Alumno, fecha);

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No existe ningun registro!");
            }
            else
            {
                Console.WriteLine("\n");
                Console.WriteLine($"Matricula:{dt.Rows[0][2]}   Fecha:{dt.Rows[0][3]}  Asistencia:{dt.Rows[0][4]}");
                Console.WriteLine("\n");

                Console.WriteLine("¿Deseas Modificar la asistencia de este alumno?");
                Console.WriteLine("0 = SI               1 = NO");

                res = int.Parse(Console.ReadLine());
                try
                {
                    if (res == 0)
                    {
                        Console.WriteLine("Ingresa el nuevo valor de Asistencia");
                        asistencia = Console.ReadLine();
                        con.CambioAsistencia(Clave_Maestro, Clave_Alumno,fecha, asistencia);
                        Console.WriteLine("Modificacion Realizada!");
                    }
                    else
                    {
                        Console.WriteLine("Modificacion Cancelada!");
                        Console.ReadKey();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ingresaste un tipo de dato diferente!");
                    res = 1;
                }
                Console.ReadKey();
            }
        }
       
        public void ConsultaFaltas()
        {
            //Desarrollando...
            int i = 0;
            int j = 0;
            int mat;
            DataTable tabla = new DataTable();
            dt = con.CantidadFaltas();

            Console.WriteLine("\tCANTIDAD DE FALTAS POR ALUMNO\n");
            Console.WriteLine($"{dt.Columns[0]}    {dt.Columns[1]}    {dt.Columns[2]}");
            foreach (DataRow Fila in dt.Rows)
            {
                Console.WriteLine($"    {dt.Rows[i][0]}           {dt.Rows[i][1]}         {dt.Rows[i][2]}");
                Console.WriteLine("---------------------------------------------");
                i++;
            }
            Console.WriteLine("\t¿Buscas un alumno en especifico?\n");
            Console.WriteLine("Ingresa la matricula del alumno");
            try
            {
                mat = int.Parse(Console.ReadLine());
                Console.WriteLine("\n");

                tabla = con.CantidadFaltasAlumno(mat);

                if(tabla.Rows.Count == 0)
                {
                    Console.WriteLine("La matricula ingresada no existe! ");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"{tabla.Columns[0]}    {tabla.Columns[1]}    {tabla.Columns[2]}");
                    foreach (DataRow F in tabla.Rows)
                    {
                        Console.WriteLine($"    {tabla.Rows[j][0]}           {tabla.Rows[j][1]}         {tabla.Rows[j][2]}");
                        Console.WriteLine("---------------------------------------------");
                        j++;
                    }
                    Console.ReadKey();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("La matricula ingresada tiene formato incorrecto");
                Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }

    }
}
