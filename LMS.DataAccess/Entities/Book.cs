namespace LMS.DataAccess.Entities;

public class Book {
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string ISBN { get; private set; }
    public string Genre { get; private set; }
    public int PublicationYear { get; private set; }
    public bool IsBorrowed { get; set; }
    public bool IsReserved { get; set; }

    public Book(string title, string author, string isbn, string genre, int publicationYear) {
        Title = title;
        Author = author;
        ISBN = isbn;
        Genre = genre;
        PublicationYear = publicationYear;
        IsBorrowed = false;
        IsReserved = false;
    }

    public override string ToString() {
        return $"Title: {Title}, Author: {Author}\nGenre: {Genre}, Publication Year: {PublicationYear}\nIs Borrowed: {IsBorrowed}, Is Reserved: {IsReserved}\nISBN: {ISBN}\n";
    }

}
