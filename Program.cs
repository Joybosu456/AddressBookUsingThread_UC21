using System.Text;

namespace AddressbookThread;

class Program
{
    public static void Main(string[] args)
    {
        //List<AdBook> listperson = new List<AdBook>();
        //listperson.Add(new AdBook(1,"Joy","GopalNagar","Asansol","westBengal","425235","niravpandit0002@gmail.com",527527622));
        //listperson.Add(new AdBook(2, "hsdgc", "esrg", "USA", "cal", "4252535", "jhon@gmail.com", 5355555565));
        //listperson.Add(new AdBook(3, "RussinanTeen", "jghbvodij", "Ve", "cal", "4252345", "Russian@gmail.com", 67687687));
        // listperson.Add(new AdBook(3, "FBGG", "CAnada", "Cana", "ca", "4252345", "gg@gmail.com", 67687687));
        //Console.WriteLine(sb.ToString()); Adressbookopreation adressbookopreation = new Adressbookopreation();
        //adressbookopreation.addressRoll(listperson);
        //adressbookopreation.addressRollThread(listperson);


        string[] words = CreateWordArray(@"http://www.gutenberg.org/files/54700/54700-0.txt");

        Parallel.Invoke(() =>
        {
            Console.WriteLine("Begin first task.....");
            GetLongestWord(words);
        },

        () =>
        {
            Console.WriteLine("Begin second task....");
            GetMostCommonWords(words);
        },
        () =>
        {
            Console.WriteLine("Begin third task....");
            GetCountForWords(words, "sleep");
        }
    );

        static string[] CreateWordArray(string uri)
        {
            Console.WriteLine($"Retrieving from {uri}");
            string s = new System.Net.WebClient().DownloadString(uri);

            return s.Split(
                new char[] { ' ', '\u000A', ',', '.', ';', ':', '-', '_', '/' },
                StringSplitOptions.RemoveEmptyEntries);
        }
    }

    private static string GetLongestWord(string[] words)
    {
        var longestWord = (from w in words
                           orderby w.Length descending
                           select w).First();
        Console.WriteLine($"Task 1 -- The LONGEST WORD is {longestWord}");
        return longestWord;
    }

    private static void GetCountForWords(string[] words, string term)
    {
        var findwords = from word in words
                        where word.ToUpper().Contains(term.ToUpper())
                        select word;
        Console.WriteLine($@"Task 3 ---- the word ""{term}"" occurs {findwords.Count()} times.");

    }

    private static void GetMostCommonWords(string[] words)
    {
        var frequencyorder = from word in words
                             where word.Length > 6
                             group word by word into g
                             orderby g.Count() descending
                             select g.Key;

        var commanwords = frequencyorder.Take(10);
        System.Text.StringBuilder sb = new StringBuilder();
        sb.AppendLine("Task 2 -- the most common words are :");
        foreach (var v in commanwords)
        {
            sb.AppendLine(" " + v);
        }



    }
}