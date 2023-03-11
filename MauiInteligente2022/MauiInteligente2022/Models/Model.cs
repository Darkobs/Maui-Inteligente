namespace MauiInteligente2022.Models;

public record LoginCredentials(string Username, string Password);

public record Branch(int BranchId, string Name, string Location)
{
    public Branch()
        :this(0, null!, null!)
    {

    }
}

#region NewReport

public record NewRecordStep1(string ReportId, string PhotoPath1, string PhotoPath2, string PhotoPath3, string PhotoPath4);

#endregion