using Securify.ShellLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateRelativePathShortcut
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var arguments = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i++)
            {
                string argument = args[i];
                var idx = argument.IndexOf(':');
                if (idx > 0)
                {
                    arguments[argument.Substring(1, idx - 1).ToLower()] = argument.Substring(idx + 1);
                }
                else
                {
                    idx = argument.IndexOf('=');
                    if (idx > 0)
                    {
                        arguments[argument.Substring(1, idx - 1).ToLower()] = argument.Substring(idx + 1);
                    }
                    else
                    {
                        arguments[argument.Substring(1).ToLower()] = string.Empty;
                    }
                }
            }

            string path;
            if (!arguments.ContainsKey("path") || String.IsNullOrEmpty(arguments["path"]))
            {
                Console.WriteLine("[X] /path is required and must contain the relative path for the target\r\n");
                return;
            }
            else
            {
                path = arguments["path"];
            }

            string output;
            if (!arguments.ContainsKey("output") || String.IsNullOrEmpty(arguments["output"]))
            {
                Console.WriteLine("[X] /output is required and must contain the path to save the generated shortcut\r\n");
                return;
            }
            else
            {
                output = arguments["output"];
            }

            string cmdLineArg = "";
            if (arguments.ContainsKey("args") && !String.IsNullOrEmpty(arguments["args"]))
            {
                cmdLineArg = arguments["args"];
            }

            string workdir = "";
            if (arguments.ContainsKey("workdir") && !String.IsNullOrEmpty(arguments["workdir"]))
            {
                cmdLineArg = arguments["workdir"];
            }

            string iconPath = "";
            if (arguments.ContainsKey("iconpath") && !String.IsNullOrEmpty(arguments["iconpath"]))
            {
                iconPath = arguments["iconpath"];
            }

            int iconIndex = 0;
            if (arguments.ContainsKey("iconindex") && !String.IsNullOrEmpty(arguments["iconindex"]))
            {
                if (!int.TryParse(arguments["iconindex"], out iconIndex))
                {
                    Console.WriteLine("[X] /iconindex must be a valid integer\r\n");
                    return;
                }
            }

            try
            {
                Shortcut shortcut = Shortcut.CreateShortcut(path, cmdLineArg, workdir, iconPath, iconIndex);
                Console.WriteLine("[+] Initial shortcut created");
                shortcut.StringData.RelativePath = path;
                Console.WriteLine("[+] Relative path set to {0}", path);
                shortcut.WriteToFile(output);
                Console.WriteLine("[+] Shortcut saved to {0}", output);
            }
            catch (Exception e)
            {
                Console.WriteLine("\r\n[X] Error: {0}\r\n", e.Message);
            }
        }
    }
}
