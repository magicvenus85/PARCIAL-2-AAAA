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
    }
}

public class RecursosHumanos : IRecursosHumanos
{
    public string Email { get; set; }
    public string Password { get; set; }

    public void Menu()
    {
        AnsiConsole.MarkupLine("[green]Bienvenido Recursos Humanos[/]");
        AnsiConsole.MarkupLine("[yellow]1[/] Contratar desarrollador");
        AnsiConsole.MarkupLine("[yellow]2[/] Despedir desarrollador");
        AnsiConsole.MarkupLine("[yellow]3[/] Gestionar pagos");
    }

    public void Contratar(Desarrollador desarrollador)
    {
        // Implementar la contratación de desarrolladores
    }

    public void Despedir(Desarrollador desarrollador)
    {
        // Implementar el despido de personal
    }

    public void GestionarPagos()
    {
        // Implementar la gestión de pagos
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
    }
}

// Implementar el login y la inicialización de los datos
public class Program
{
    static void Main(string[] args)
    {
        // Crear una lista de empleados
        List<IEmpleado> empleados = new List<IEmpleado>
        {
            new Gerente { Email = "gerente123", Password = "123456" },
            new RecursosHumanos { Email = "RH1234", Password = "123456" },
            new Desarrollador { Email = "uselessdev", Password = "123456" }
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
