using System;

public class Note_F
{
    public int NoteID { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public int AuthorID { get; set; }
    public string Tags { get; set; }
    public string AttachmentPath { get; set; }
    public bool Approved { get; set; }
}
