using System;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        // список процессов
        Console.WriteLine("--- List of processes ---");
        ListOfAllRunningProcesses();
        Console.WriteLine("-------------------------\n");

        // информация по опр. процессу
        Console.WriteLine("--- Process information ---");
        Console.Write("Input ID: ");
        int thisProcessId0 = Convert.ToInt32(Console.ReadLine());
        GetSpecificProcess(thisProcessId0);
        Console.WriteLine("-------------------------\n");

        // исследование по опр. процессу
        Console.WriteLine("--- Process investigate ---");
        Console.Write("Input ID: ");
        int thisProcessId1 = Convert.ToInt32(Console.ReadLine());
        GetSpecificProcess(thisProcessId1);
        Console.WriteLine("-------------------------\n");

        // уничтожение опр.процееса
        Console.WriteLine("--- Process to kill ---");
        Console.Write("Input ID: ");
        int thisProcessId2 = Convert.ToInt32(Console.ReadLine());
        KillProcess(thisProcessId2);
        Console.WriteLine("-------------------------\n");

        Console.ReadLine();
        ;
    }
    static void ListOfAllRunningProcesses()
    {
        // получение упордоченного списка процессов
        var runningProcesses = from proc in Process.GetProcesses(".") orderby proc.Id select proc;

        // вывод ID и имени
        foreach (var p in runningProcesses)
        {
            string processInfo = $"- ID: {p.Id} \t Name: {p.ProcessName}";
            Console.WriteLine(processInfo);
        }
    }
    static void GetSpecificProcess(int thisProcessId)
    {
        Process theProc = null;
        try
        {
            theProc = Process.GetProcessById(thisProcessId);

            string processInfo = $"- ID: {theProc.Id} \t Name: {theProc.ProcessName} \t Responding: {theProc.Responding} \t Start time: {theProc.StartTime}";
            Console.WriteLine(processInfo);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
    }
    static void EnumThreadsForID(int thisProcessId)
    {
        Process theProc = null;
        try
        {
            theProc = Process.GetProcessById(thisProcessId);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }
        Console.WriteLine("Here are the threads used by: {0}", theProc.ProcessName);
        ProcessThreadCollection theThreads = theProc.Threads;
        foreach (ProcessThread pt in theThreads)
        {
            string info =
            $"Thread ID: {pt.Id} \t Start Time: {pt.StartTime.ToShortTimeString()} \t Priority: { pt.PriorityLevel}";

            Console.WriteLine(info);
        }
    }
    static void KillProcess(int thisProcessId)
    {
        Process theProc = null;
        try
        {
            theProc = Process.GetProcessById(thisProcessId);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        // уничтожить процесс 
        try
        {
            theProc.Kill();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}