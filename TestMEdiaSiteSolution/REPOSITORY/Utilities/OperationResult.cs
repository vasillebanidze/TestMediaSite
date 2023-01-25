namespace REPOSITORY.Utilities;

public class OperationResult
{
    /// <summary>
    ///     ჩანაწერის დამატებისას ჩანაწერის Id-ი ბრუნდება ამ ცვლადით,
    ///     თუ საჭირო გახდა დეტალურ გვერდზე ან შემდგომი ოპერაციისთვის
    /// </summary>
    public int RecordId { get; set; }


    public bool IsSuccessfully { get; set; }
    public bool IsValidationError { get; set; }
    public bool RecordNotFoundStatus { get; set; }


    public string? ErrorString { get; set; }
}