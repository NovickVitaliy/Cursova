using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursovaProject.Useful_Extensions
{
  public static class LINQExtensions
  {
    public static int? IndexOfItemBasedOnPredicate(this IEnumerable<string> data, Predicate<string> predicate)
    {
      int? counter = 0;
      foreach (var item in data)
      {
        if (predicate(item))
        {
          return counter;
        }
        counter++;
      }

      return null;
    }
  }
}
