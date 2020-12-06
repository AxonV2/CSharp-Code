using System.Collections.Generic;

namespace Abs4CL
{
    public interface IComedia : IFilme
    {
        List<string> Actors { get; set; }
        string Director { get; set; }
    }
}