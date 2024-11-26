namespace NotesImprovs.DAL.Interfaces;

public interface IKeyable<TKey>
{
    TKey Id { get; set; }
}