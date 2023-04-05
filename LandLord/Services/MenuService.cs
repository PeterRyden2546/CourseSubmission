using LandLord.Entities;

namespace LandLord.Services;

internal class MenuService
{
    private readonly CaseService _caseService = new();
    private readonly CommentService _commentService = new();
    private readonly UserService _userService = new();

    public async Task MainMenu()
    {
        Console.Clear();
        Console.WriteLine("############ HuvudMenyn ###############");
        Console.WriteLine("1. Lägg till användare");
        Console.WriteLine("2. Visa alla användare");
        Console.WriteLine("3. Sök efter en användare");
        Console.WriteLine("4. Skapa ett ärende");
        Console.WriteLine("5. Se alla ärende");
        Console.WriteLine("6. Se alla aktiva ärende");
        Console.WriteLine("7. Sök efter ett ärende");
        Console.WriteLine("8. Uppdatera status på ett ärende");
        Console.WriteLine("9. Skriv enkommentar till ett ärende");
        Console.WriteLine("Välj ett an följaande alternativ (1-9):");

        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await AddUserAsync();
                break;
            case "2":
                await AllUsersAsync();
                break;
            case "3":
                await GetOneUserAsync();
                break;
            case "4":
                await CreateCaseAsync();
                break;
            case "5":
                await AllCasesAsync();
                break;
            case "6":
                await AllActiveCasesAsync();
                break;
            case "7":
                await CaseAsync();
                break;
            case "8":
                await UpdateCaseAsync();
                break;
            case "9":
                await AddCommentAsync();
                break;
            default:
                break;

        }
    }

    private async Task<UserEntity> AddUserAsync()
    {
        var _entity = new UserEntity();

        Console.Clear(); 
        Console.WriteLine("################## Ny Användare ##################"); 
        Console.Write("Ange förnamn: "); 
        _entity.FirstName = Console.ReadLine() ?? ""; 
        Console.Write("Ange efternamn: "); 
        _entity.LastName = Console.ReadLine() ?? ""; 
        Console.Write("Ange telefonnummer: "); 
        _entity.PhoneNumber = Console.ReadLine() ?? "";
        Console.Write("Ange e-postadress: "); 
        _entity.Email = Console.ReadLine() ?? "";

        return await _userService.CreateUserAsync(_entity);

    }

    private async Task AllUsersAsync()
    {
        Console.Clear();
        Console.WriteLine("################## Alla Användare ##################");
        foreach(var _user in await _userService.GetAllAsync()) 
        {
            Console.WriteLine($"Id: {_user.Id}");
            Console.WriteLine($"Namn: {_user.FirstName} {_user.LastName}");
            Console.WriteLine($"Telefon nummer: {_user.PhoneNumber}");
            Console.WriteLine($"Email: {_user.Email}");
        }
    }

    private async Task GetOneUserAsync()
    {
        Console.WriteLine("Skriv in ett sökvärde på en användare");
        var _searchItem = Console.ReadLine();
        if(!string.IsNullOrEmpty(_searchItem))
        {
            var _oneUser = await _userService.GetAsync(
               x => x.FirstName == _searchItem 
            || x.LastName == _searchItem 
            || x.PhoneNumber == _searchItem
            || x.Email == _searchItem
            );

            if ( _oneUser != null ) 
            {
                Console.Clear();
                Console.WriteLine("################## En Användare ##################");
                Console.WriteLine($"Id: {_oneUser.Id}");
                Console.WriteLine($"Namn: {_oneUser.FirstName} {_oneUser.LastName}");
                Console.WriteLine($"Telefon nummer: {_oneUser.PhoneNumber}");
                Console.WriteLine($"Email: {_oneUser.Email}");
                
            }
            else
            {
                Console.WriteLine("Inget hittades!");
            }
        }
        
    }

    private async Task CreateCaseAsync()
    {
        await AllUsersAsync();

        var _entity = new CaseEntity();

        Console.WriteLine("################## Nytt Ärende ##################");
        Console.Write("Ange Använda Id: ");
        _entity.UserId = int.Parse(Console.ReadLine()!);
        Console.Clear();
        Console.Write("Ange beskrivning: ");
        _entity.Description = Console.ReadLine() ?? "";
        Console.WriteLine("Ange kundens Email:");
        _entity.Email = Console.ReadLine() ?? "";
        Console.WriteLine("Ange kundens Förnamn:");
        _entity.FirstName = Console.ReadLine() ?? "";
        Console.WriteLine("Ange kundens Efternamn:");
        _entity.LastName = Console.ReadLine() ?? "";
        Console.WriteLine("Ange kundens Telefon nummer:");
        _entity.PhoneNumber = Console.ReadLine() ?? "";

        await _caseService.CreateCaseAsync( _entity );

    }

    private async Task AllCasesAsync()
    {
        Console.Clear();
        Console.WriteLine("################## Alla Ärende ##################");

        foreach(var _case in await _caseService.GetAllCaseAsync()) 
        {
            Console.WriteLine($"Ärende nummer: {_case.Id}");
            Console.WriteLine($"Beskrivning av ärendet: {_case.Description}");
            Console.WriteLine($"Skapat: {_case.Created}");
            Console.WriteLine($"Uppdaterat: {_case.UpdateAt}");
            Console.WriteLine($"Skapad av: {_case.User.FirstName} {_case.User.LastName}");
        }
    }

    private async Task AllActiveCasesAsync()
    {
        Console.Clear();
        Console.WriteLine("################## Alla Aktiva Ärende ##################");

        foreach (var _case in await _caseService.GetAllCaseAsync())
        {
            Console.WriteLine($"Ärende nummer: {_case.Id}");
            Console.WriteLine($"Beskrivning av ärendet: {_case.Description}");
            Console.WriteLine($"Skapat: {_case.Created}");
            Console.WriteLine($"Uppdaterat: {_case.UpdateAt}");
            Console.WriteLine($"Skapad av: {_case.User.FirstName} {_case.User.LastName}");
            Console.WriteLine($"Status: {_case.Status.StatusCode}");
        
        }
    }

    private async Task CaseAsync()
    {
        Console.WriteLine("Skriv in ett sökvärde på ett ärende");
        var _searchItem = Console.ReadLine();
        if (!string.IsNullOrEmpty(_searchItem))
        {
            var _oneCase = await _caseService.GetACaseAsync(
                x => x.User.FirstName == _searchItem
                || x.User.LastName == _searchItem
                || x.User.Email == _searchItem
                || x.Email == _searchItem
                || x.FirstName == _searchItem
                || x.LastName == _searchItem
                );

            if( _oneCase != null )
            {
                Console.Clear();
                Console.WriteLine("################## Ett Ärende ##################");
                Console.WriteLine($"ÄrendeNummer: {_oneCase.Id}");
                Console.WriteLine($"Beskrivning: {_oneCase.Description}");
                Console.WriteLine($"Anmäld av: {_oneCase.FirstName} {_oneCase.LastName}");
                Console.WriteLine($"Skapad: {_oneCase.Created}");
                Console.WriteLine($"Uppdaterad: {_oneCase.UpdateAt}");
                Console.WriteLine("");
                foreach(var _comment in await _commentService.GetAllAsync(_oneCase.Id))
                {
                    Console.WriteLine($"Commentar: {_comment.Comment}");
                    Console.WriteLine($"Vem som skrev kommentaren: {_comment.Author}");
                    Console.WriteLine($"Tidpunkt: {_comment.Created}");
                    Console.WriteLine("");
                }
            }
        }
    }

    private async Task UpdateCaseAsync()
    {
        Console.Clear();
        Console.WriteLine("################## Uppdatera ett ärende ##################");
        Console.WriteLine("Skriv in ärende nummert på det ärende status");
        var _caseId = int.Parse(Console.ReadLine()!);
        Console.WriteLine("ange 2 för Pågående");
        Console.WriteLine("ange 3 för Väntar delar");
        Console.WriteLine("ange 4 för Avslutad");
        var _statusId = int.Parse(Console.ReadLine()!);

       await _caseService.UpdateStatusOnCaseAsync(_caseId, _statusId);
    }

    private async Task AddCommentAsync()
    {
        Console.Clear();
        Console.WriteLine("################## Lägg till kommentar till ärende ##################");

        Console.WriteLine("Skriv in ett sökvärde på ett ärende");
        var _searchItem = Console.ReadLine();
        if (!string.IsNullOrEmpty(_searchItem))
        {
            var _oneCase = await _caseService.GetACaseAsync(
                x => x.User.FirstName == _searchItem
                || x.User.LastName == _searchItem
                || x.User.Email == _searchItem
                || x.Email == _searchItem
                || x.FirstName == _searchItem
                || x.LastName == _searchItem
                );

            if (_oneCase != null)
            {
                var _entity = new CommentEntity();

                Console.WriteLine("Ange kommentar:");
                _entity.Comment = Console.ReadLine() ?? "";
                Console.WriteLine("Skriv ditt namn:");
                _entity.Author = Console.ReadLine() ?? "";
                _entity.CaseId = _oneCase.Id;

                await _commentService.CreateCommentAsync(_entity);
            }
        }
        else
        {
            Console.WriteLine("Inget hittades!");
        }
    }
}
