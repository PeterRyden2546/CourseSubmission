using LandLord.Services;

StatusService statusService = new();
MenuService menuService = new();

await statusService.AddingStatusCodeToDbAsync();

while (true)
{
    Console.Clear();
    await menuService.MainMenu();
    Console.ReadKey();
}
