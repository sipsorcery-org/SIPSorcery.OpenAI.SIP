//-----------------------------------------------------------------------------
// Filename: Program.cs
//
// Description: An example SIP application that can be used to interact with
// OpenAI's real-time API https://platform.openai.com/docs/guides/realtime-sip.
//
// Usage:
// set OPENAI_API_KEY=your_openai_key
// dotnet run
//
// Author(s):
// Aaron Clauson (aaron@sipsorcery.com)
// 
// History:
// 28 Aug 2025	Aaron Clauson	Created, Dublin, Ireland.
//
// License: 
// BSD 3-Clause "New" or "Revised" License and the additional
// BDS BY-NC-SA restriction, see included LICENSE.md file.
//-----------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Serilog;
using SIPSorceryMedia.Windows;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Logging;
using SIPSorceryMedia.Abstractions;
using SIPSorcery.Media;

namespace demo;

class Program
{
    static async Task Main()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug() 
            //.MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        var loggerFactory = new SerilogLoggerFactory(Log.Logger);
        SIPSorcery.LogFactory.Set(loggerFactory);

        Log.Logger.Information("SIP OpenAI Demo Program");

        var openAiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

        if (string.IsNullOrWhiteSpace(openAiKey))
        {
            Log.Logger.Error("Please provide your OpenAI key as an environment variable. For example: set OPENAI_API_KEY=<your openai api key>");
            return;
        }

        var logger = loggerFactory.CreateLogger<Program>();

        // Send/receive audio directly from Windows audio devices.
        var windowsAudioEp = InitialiseWindowsAudioEndPoint();
       

        Console.WriteLine("Wait for ctrl-c to indicate user exit.");

        var exitTcs = new TaskCompletionSource<object?>();
        Console.CancelKeyPress += (s, e) =>
        {
            e.Cancel = true;
            exitTcs.TrySetResult(null);
        };

        await exitTcs.Task;
    }

    private static WindowsAudioEndPoint InitialiseWindowsAudioEndPoint()
    {
        var audioEncoder = new AudioEncoder(AudioCommonlyUsedFormats.OpusWebRTC);
        return new WindowsAudioEndPoint(audioEncoder);
    }
}
