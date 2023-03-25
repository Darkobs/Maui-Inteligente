namespace MauiInteligente2022.Models;

public record LoginCredentials(string Username, string Password);

public record Branch(int BranchId, string Name, string Location)
{
    public Branch()
        :this(0, null!, null!)
    {

    }
}

public record Country(int CountryId, string CountryCode, string Name)
{
    public override string ToString() => Name;
}

#region NewReport

public record NewRecordStep1(string ReportId, string PhotoPath1, string PhotoPath2, string PhotoPath3, string PhotoPath4);

public record NewReportStep2(string ClientNumber, string ClientName, string ClientPhoneNumber, string ClientEmail, string ClientCountry, string ClientCity, string ClientDocument, string ClientDocumentNumber);

public record NewReportStep3(string ReportDescription, decimal Amount);

#endregion