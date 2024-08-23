public class Patron {
    public string Name { get; private set; }
    public string MemberShipNumber { get; private set; }
    public string ContactDetails { get; private set; }
    public List<BorrowingRecord> BorrowingRecords { get; private set; }
    public List<Reserve> ReservedRecords { get; private set; }
    

    public Patron(string name, string membershipNumber, string contactDetails)
    {
        Name = name;
        MemberShipNumber = membershipNumber;
        ContactDetails = contactDetails;
        BorrowingRecords = [];
        ReservedRecords = [];
    }

    public override string ToString() {
        return $"Name: {Name}, Contact Details: {ContactDetails}\nMembership Number: {MemberShipNumber}\n";
    }
}
