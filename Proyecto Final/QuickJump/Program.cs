using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QuickJump
{
    class Program
    {
        static void Main(string[] args)
        {
            int resp = 0;
            Funcion fun = new Funcion();
            while(resp != 7)
            {
                Console.Clear();
                Console.Write("\t\tQuickJump\n\n");
                Console.WriteLine("\tSeleccione la opcion deseada\n");
                Console.WriteLine("1. Registrar Alumno");
                Console.WriteLine("2. Editar datos del Alumno");
                Console.WriteLine("3. Eliminar Alumno");
                Console.WriteLine("4. Generar Asistencia");
                Console.WriteLine("5. Editar Asistencia");
                Console.WriteLine("6. Visualizar Faltas");
                Console.WriteLine("7. Salir");

                resp = int.Parse(Console.ReadLine());
                switch (resp)
                {
                    case 1:
                        Console.Clear();
                        fun.alta();
                        break;
                    case 2:
                        Console.Clear();
                        fun.ModificarAlumno();
                        break;
                    case 3:
                        Console.Clear();
                        fun.baja();
                        break;
                    case 4:
                        Console.Clear();
                        fun.RegistrarAsistencia();
                        break;
                    case 5:
                        Console.Clear();
                        fun.ModificarAsistencia();
                        break;
                    case 6:
                        Console.Clear();
                        fun.ConsultaFaltas();
                        break;
                    case 7:
                        Console.Clear();
                        Console.WriteLine("Saliendo del Programa...\n\nPresione enter para continuar");
                        Console.ReadKey();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opción Incorrecta");
                        Console.ReadKey();
                        break;
                }
            }

        }
    }
}
