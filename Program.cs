namespace Assignment;

class Program {
    private static string DATA_FILE = "data.txt";
    
    static void Main(string[] args) {
        // ask for input
        Console.WriteLine("Enter 1 to create data file.");
        Console.WriteLine("Enter 2 to parse data.");
        Console.WriteLine("Enter anything else to quit.");
        // input response
        string? resp = Console.ReadLine();

        switch (resp) {
            case "1":
                // * create data file
                // ask a question
                Console.WriteLine("How many weeks of data is needed?");
                // input the response (convert to int)
                int weeks = int.Parse(Console.ReadLine() ?? "");
                // determine start and end date
                DateTime today = DateTime.Now;
                // we want full weeks sunday - saturday
                DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
                // subtract # of weeks from endDate to get startDate
                DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
                // random number generator
                Random rnd = new Random();
                // create file
                StreamWriter sw = new StreamWriter(DATA_FILE);

                // loop for the desired # of weeks
                while (dataDate < dataEndDate) {
                    // 7 days in a week
                    int[] hours = new int[7];
                    for (int i = 0; i < hours.Length; i++) {
                        // generate random number of hours slept between 4-12 (inclusive)
                        hours[i] = rnd.Next(4, 13);
                    }

                    // M/d/yyyy,#|#|#|#|#|#|#
                    // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
                    sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
                    // add 1 week to date
                    dataDate = dataDate.AddDays(7);
                }

                sw.Close();
                break;
            case "2":
                if (!File.Exists(DATA_FILE)) break;
                StreamReader file = new StreamReader(
                    new FileStream("data.txt", FileMode.Open));

                while (!file.EndOfStream) {
                    String curLine = file.ReadLine() ?? "";

                    var rawData = (
                        dataDate: DateTime.Parse(curLine.Split(',')[0]),
                        dataSleep: curLine.Split(',')[1].Split('|')
                    );
                    
                    Console.WriteLine($"Week of {rawData.dataDate:MMM, dd, yyyy}");
                    Console.WriteLine( " Mo Tu We Th Fr Sa Su Tot Avg");
                    Console.WriteLine( " -- -- -- -- -- -- -- --- ---");

                    Console.Write(' ');
                    
                    foreach(string dataPoint in rawData.dataSleep) {
                        int numeric = int.Parse(dataPoint);
                        Console.Write($"{numeric:D} ".PadLeft(3));
                    }
                    
                    Console.Write($"{Total(rawData.dataSleep):D} ".PadLeft(4));
                    Console.Write($"{Average(rawData.dataSleep):N1} ");

                    Console.WriteLine();
                    Console.WriteLine();
                }

                break;
        }
    }

    static int Total(String[] values) {
        int sum = 0;
        
        foreach (var value in values) {
            sum += int.Parse(value);
        }

        return sum;
    }
    
    static double Average(String[] values) {
        double sum = 0;
        
        foreach (var value in values) {
            sum += int.Parse(value);
        }

        return sum / values.Length;
    }
}
