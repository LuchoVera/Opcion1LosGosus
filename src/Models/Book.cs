public class Book {
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string ISBN { get; private set; }
    public string Genre { get; private set; }
    public int PublicationYear { get; private set; }
    public bool IsBorrowed { get; private set; }
    public bool IsReserved { get; private set; }

    public Book(string title, string author, string isbn, string genre, int publicationYear) {
        Title = title;
        Author = author;
        ISBN = isbn;
        Genre = genre;
        PublicationYear = publicationYear;
        IsBorrowed = false;
        IsReserved = false;
    }

    public void Borrow() 
    {
        if (!IsReserved) 
        {
            IsBorrowed = true;
        }
    }

    public bool Reserve()
    {
        if(IsBorrowed)
        {
            IsReserved = true;
        }

        return IsReserved;
    }
    public void Return() 
    { 
        IsBorrowed = false;
    }

    public void EndReserve()
    {
        IsReserved = false;
    }

    public override string ToString() {
        return $"Title: {Title}, Author: {Author}\nGenre: {Genre}, Publication Year: {PublicationYear}\nIs Borrowed: {IsBorrowed}, ISBN: {ISBN}\n";
    }

}
