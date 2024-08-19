public class Patron {
    public string Name { get; private set; }
    public string PatronId { get; private set; }
    public string MemberShipNumber { get; private set; }
    public string ContactDetails { get; private set; }
    private List<BorrowingRecord> borrowingRecords;

    public Patron(string name, string patronId, string membershipNumber, string contactDetails)
    {
        Name = name;
        PatronId = patronId;
        MemberShipNumber = membershipNumber;
        ContactDetails = contactDetails;
        borrowingRecords = new List<BorrowingRecord>();
    }

    public override string ToString() {
        return $"Name: {Name}, Contact Details: {ContactDetails}\nPatron ID: {PatronId}, Membership Number: {MemberShipNumber}\n";
    }
    
    public void AddBorrowingRecord(BorrowingRecord record)
    {
        borrowingRecords.Add(record);
    }

    public List<BorrowingRecord> GetBorrowingHistory()
    {
        return new List<BorrowingRecord>(borrowingRecords);
    }

}
