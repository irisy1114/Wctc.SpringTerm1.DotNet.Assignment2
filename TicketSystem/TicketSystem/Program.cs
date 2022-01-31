using System.Text;

namespace TicketSystem;


class Program
{
    static void Main(string[] args)
    {
        string fileName = "Files/tickets.csv";
        string filePath = AppContext.BaseDirectory + fileName;
        string userChoice;
        do
        {
            // ask user a question
            Console.WriteLine("****************************");
            Console.WriteLine("1. Read data from file.");
            Console.WriteLine("2. Create file from data.");
            Console.WriteLine("Enter any other key to exit");
            Console.WriteLine("****************************");
            userChoice = Console.ReadLine();

            if (userChoice == "1")
            {
                // read data from file
                ReadData(filePath);
            }

            if (userChoice == "2")
            {
                StreamWriter sw = null;                
                if (File.Exists(filePath))
                {
                    sw = new StreamWriter(filePath, true);
                    WriteData(sw, false);
                }
                else
                {
                    // create new file
                    var options = new FileStreamOptions();
                    options.Mode = FileMode.Create;
                    options.Access = FileAccess.ReadWrite;
                    options.Share = FileShare.ReadWrite;
                    sw = new StreamWriter(filePath, options);
                    WriteData(sw, true);
                }                               
            }
        } while(userChoice == "1" || userChoice == "2");

    }

    static void ReadData(string filePath)
    {
        if (File.Exists(filePath))
        {
            StreamReader sr = new StreamReader(filePath);
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();              
                var col = line.Split(',');
                for (int i = 0; i < col.Length; i++)
                {
                    Console.Write(col[i] + " ");
                }
                Console.WriteLine();
                
            }
            sr.Close();
        }
        else
            Console.WriteLine("File does not exist.");
    }

    static void WriteData(StreamWriter sw, bool createHeader)
    {
        Console.WriteLine("Enter the ticketID.");
        var num = Console.ReadLine();
        Console.WriteLine("Enter the summary.");
        var summary = Console.ReadLine();
        Console.WriteLine("Enter the status.");
        var status = Console.ReadLine();
        Console.WriteLine("Enter the priority.");
        var priority = Console.ReadLine();
        Console.WriteLine("Enter the submitter.");
        var submitter = Console.ReadLine();
        Console.WriteLine("Enter the assigned person.");
        var assigned = Console.ReadLine();

        var answer = "";
        // loop over watching, create a watch list to store names
        var watchList = new List<string>();
        do
        {
            Console.WriteLine("Enter who's watching.");
            var watching = Console.ReadLine();
            watchList.Add(watching);
            Console.WriteLine("Did you finish entering the watching name(Y/N)?");
            answer = Console.ReadLine().ToUpper();
        } while (answer == "N");

        // if there's no header, create new header
        if (createHeader)
            sw.WriteLine("TicketID,Summary,Status,Priority,Submitter,Assigned,Watching");
        else
            sw.WriteLine();

        sw.WriteLine($"{num},{summary},{status},{priority},{submitter},{assigned}," + String.Join('|', watchList.ToArray()));

        sw.Close();
    }
          
}
