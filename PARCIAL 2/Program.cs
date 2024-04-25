public class Desarrollador : IDesarrollador
{
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Proyecto> Proyectos { get; set; }

    public void Menu()
    {
        bool salir = false;

        while (!salir)
        {
            AnsiConsole.MarkupLine("[green]Bienvenido Desarrollador[/]");
            AnsiConsole.MarkupLine("[yellow]1[/] Ver proyectos asignados");
            AnsiConsole.MarkupLine("[yellow]2[/] Salir");

            string opcionStr = AnsiConsole.Ask<string>("Por favor, elija una opción: ");

            if (int.TryParse(opcionStr, out int opcion))
            {
                switch (opcion)
                {
                    case 1:
                        MostrarProyectos();
                        break;
                    case 2:
                        salir = true;
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
    }

    private void MostrarProyectos()
    {
        AnsiConsole.MarkupLine("[green]Proyectos[/]");
        foreach (var proyecto in Proyectos)
        {
            AnsiConsole.MarkupLine($"- {proyecto.Nombre} - {proyecto.Estado}");
        }
    }
}
