namespace Abs4CL
{
    public interface IFilme
    {
        float Duration { get; set; }
        string Name { get; set; }
        int Year { get; set; }

        bool LengthyCheck(float param);
        bool LengthyCheck<T>(T param) where T : Filme, new();
    }
}