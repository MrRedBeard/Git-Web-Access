﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Configuration;

namespace GitTools
{
    public abstract class Git
    {
        public static string Run(string args, string workingDirectory)
        {
            var gitExePath = ConfigurationManager.AppSettings["GitExePath"];

            var pinfo = new ProcessStartInfo(gitExePath)
            {
                Arguments = args,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory,
            };

            using (var process = Process.Start(pinfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(error))
                    throw new Exception(error);

                return output;
            }
        }
    }
}