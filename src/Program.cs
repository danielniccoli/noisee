using System.Diagnostics;
using System.Runtime.InteropServices;

const uint PROCESS_VM_READ = 0x10;

[DllImport("kernel32.dll")]
static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

[DllImport("kernel32.dll")]
static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

const int NOITA_SEED_VM_ADDRESS = 0xBEE850;
//const int NOITA_PLAYTIME_VM_ADDRESS = 0x2670;
//const int NOITA_INGAMETIME_VM_ADDRESS = 0xBF10C0;

Process? noitaProcess = null;
int seed = 0;

ConsoleColor defaultColor = Console.ForegroundColor;

while (true)
{
    #region WaitForNoitaProcess
    noitaProcess = null;
    Console.Write($"Waiting for Noita process... ");

    do
    {
        IEnumerable<Process> processes = Process.GetProcessesByName("noita")
            .Where(p => !p.HasExited);

        switch (processes.Count())
        {
            case 1:
                Console.WriteLine("Found!");
                noitaProcess = processes.First();
                break;
            case > 1:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Found!");
                Console.Error.WriteLine($"Warning: Found ${processes.Count()} Noita processes. Selecting most recent.");
                Console.ForegroundColor = defaultColor;
                noitaProcess = processes.OrderByDescending(p => p.StartTime).First();
                break;
            default:
                Thread.Sleep(500);
                break;
        }
    } while (noitaProcess == null);
    #endregion WaitForNoitaProcess

    #region ReadNoitaSeed
    IntPtr processHandle = OpenProcess(PROCESS_VM_READ, false, (uint)noitaProcess.Id);
    IntPtr noitaSeedOffset = IntPtr.Add(noitaProcess.MainModule.BaseAddress, NOITA_SEED_VM_ADDRESS);
    int bytesRead = 0;
    byte[] buffer = new byte[8];

    do
    {
        try
        {
            ReadProcessMemory(processHandle, noitaSeedOffset, buffer, buffer.Length, ref bytesRead);
        }
        catch
        {
            break;
        }

        int currentSeed = BitConverter.ToInt32(buffer, 0);
        if (currentSeed != 0 && currentSeed != seed)
        {
            seed = currentSeed;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Seed: {seed} - https://www.noitool.com/?seed={seed}");
            Console.ForegroundColor = defaultColor;
        }

        Thread.Sleep(500);
    } while (!noitaProcess.HasExited);
    #endregion ReadNoitaSeed
}
