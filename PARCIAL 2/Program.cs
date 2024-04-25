using System;
using System.Collections.Generic;
using Spectre.Console;

// Definición de las interfaces
public interface IEmpleado
{
    string Email { get; set; }
    string Password { get; set; }
    void Menu();
}

public interface IManager : IEmpleado
{
    List<Desarrollador> Desarrolladores { get; set; }
    List<Proyecto> Proyectos { get; set; }
}

public interface IRecursosHumanos : IEmpleado
{
    void Contratar(Desarrollador desarrollador);
    void Despedir(Desarrollador desarrollador);
    void GestionarPagos();
}

public interface IDesarrollador : IEmpleado
{
    List<Proyecto> Proyectos { get; set; }
}

// Definición de las clases
public class Proyecto
{
    public string Nombre { get; set; }
    public string Estado { get; set; } // Terminado, En curso, Pendiente de aprobación
}

public class Gerente : IManager
{
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Desarrollador> Desarrolladores { get; set; }
    public List<Proyecto> Proyectos { get; set; }

    public void Menu()
    {
        AnsiConsole.MarkupLine("[green]Bienvenido Gerente[/]");
        AnsiConsole.MarkupLine("[yellow]1[/] Ver listado de desarrolladores");
        AnsiConsole.MarkupLine("[yellow]2[/] Ver proyectos");

        string opcionStr = AnsiConsole.Ask<string>("Por favor, elija una opción: ");

        if (int.TryParse(opcionStr, out int opcion))
        {
            switch (opcion)
            {
                case 1:
                    MostrarDesarrolladores();
                    break;
                case 2:
                    MostrarProyectos();
                    break;
                default:
                    AnsiConsole.MarkupLine("[red]Opción no válida. Por favor, elija una opción válida.[/]");
                    break;
            }
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Opción no válida. Por favor, elija una opción válida.[/]");
        }
    }

    private void MostrarDesarrolladores()
    {
        AnsiConsole.MarkupLine("[green]Empleados[/]");
        foreach(var empleado in Desarrolladores)
        {
            AnsiConsole.MarkupLine($"- {empleado.Email}");
        }
    }

    private void MostrarProyectos()
    {
        AnsiConsole.MarkupLine("[green]Proyectos[/]");
        foreach(var proyecto in Proyectos)
        {
            AnsiConsole.MarkupLine($"- {proyecto.Nombre} - {proyecto.Estado}");
        }
    }
}


public class RecursosHumanos : IRecursosHumanos
{
    private List<Desarrollador> desarrolladores;

    public string Email { get; set; }
    public string Password { get; set; }

    public RecursosHumanos(List<Desarrollador> desarrolladores)
    {
        this.desarrolladores = desarrolladores;
    }

    public void Menu()
    {
        AnsiConsole.MarkupLine("[green]Bienvenido Recursos Humanos[/]");
        AnsiConsole.MarkupLine("[yellow]1[/] Contratar desarrollador");
        AnsiConsole.MarkupLine("[yellow]2[/] Despedir desarrollador");
        AnsiConsole.MarkupLine("[yellow]3[/] Gestionar pagos");
    }

    public void Contratar(Desarrollador desarrollador)
    {
        desarrolladores.Add(desarrollador);
        AnsiConsole.MarkupLine($"Desarrollador {desarrollador.Email} contratado con éxito.");
    }

    public void Despedir(Desarrollador desarrollador)
    {
        desarrolladores.Remove(desarrollador);
        AnsiConsole.MarkupLine($"Desarrollador {desarrollador.Email} despedido con éxito.");
    }

    public void GestionarPagos()
    {
        // Implementar la gestión de pagos
        foreach (var desarrollador in desarrolladores)
        {
            // Lógica para realizar pagos a desarrolladores
            // Puedes añadir más detalles o interacciones según sea necesario
            AnsiConsole.MarkupLine($"Se ha realizado el pago al desarrollador {desarrollador.Email}.");
        }
    }
}

public class Desarrollador : IDesarrollador
{
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Proyecto> Proyectos { get; set; }

    public void Menu()
    {
        AnsiConsole.MarkupLine("[green]Bienvenido Desarrollador[/]");
        AnsiConsole.MarkupLine("[yellow]1[/] Ver proyectos asignados");
        AnsiConsole.MarkupLine("[green]Proyectos[/]");
        foreach(var proyecto in Proyectos)
        {
            AnsiConsole.MarkupLine($"- {proyecto.Nombre} - {proyecto.Estado}");
        }
    }
}

// Implementar el login y la inicialización de los datos
public class Program
{
    static void Main(string[] args)
    {
        // Crear una lista de empleados
        var desarrolladores = new List<Desarrollador>
        {
            new Desarrollador { Email = "developer1", Proyectos = new List<Proyecto> { new Proyecto { Nombre = "Proyecto A", Estado = "Terminado" } } },
            new Desarrollador { Email = "developer2", Proyectos = new List<Proyecto> { new Proyecto { Nombre = "Proyecto B", Estado = "Por aprobar" } } }
        };

        List<IEmpleado> empleados = new List<IEmpleado>
        {
            new Gerente { Email = "gerente123", Password = "123456", Proyectos = new List<Proyecto>
                {
                    new Proyecto { Nombre = "Proyecto Banana", Estado = "Terminado" },
                    new Proyecto { Nombre = "Proyecto XD", Estado = "Por aprobar" },
                    new Proyecto { Nombre = "Proyecto Cemento", Estado = "Terminado" }
                },
                Desarrolladores = desarrolladores
            },
            new RecursosHumanos(desarrolladores) { Email = "RH1234", Password = "123456" },
            new Desarrollador { Email = "uselessdev", Password = "123456", Proyectos = new List<Proyecto>
                {
                    new Proyecto { Nombre = "Proyecto Banana", Estado = "Terminado" },
                    new Proyecto { Nombre = "Proyecto XD", Estado = "Por aprobar" },
                    new Proyecto { Nombre = "Proyecto Cemento", Estado = "Terminado" }
                }
            }
        };

        // Implementar el login
        Login(empleados);
    }

    static void Login(List<IEmpleado> empleados)
    {
        AnsiConsole.MarkupLine("[green]Bienvenido al sistema de gestión de empleados[/]");
        AnsiConsole.MarkupLine("Por favor, ingrese sus credenciales:");

        string email = AnsiConsole.Ask<string>("Correo electrónico: ");
        string password = AnsiConsole.Ask<string>("Contraseña: ");

        IEmpleado empleado = empleados.Find(e => e.Email == email && e.Password == password);

        if (empleado != null)
        {
            empleado.Menu();
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Credenciales incorrectas. Por favor, inténtelo de nuevo.[/]");
            Login(empleados);
        }
    }
}
