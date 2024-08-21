public class Generator
{
    private static uint ISBN = 1000000000;
    private static uint MembershipNumber = 1000000000;
    private static uint PatronId = 1;
    public static string GenerateISBN() => ("isbn-" + ISBN++).ToString();

    public static string GeneratePTRMemberNum() => ("ptr-" + MembershipNumber++).ToString();
    public static string GeneratePRTId() => ("ptrID-" + PatronId++).ToString();

}
