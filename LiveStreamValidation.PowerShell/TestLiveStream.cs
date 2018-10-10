using System;
using System.IO;
using System.Management.Automation;

namespace Axinom.LiveStreamValidation
{
    [Cmdlet(VerbsDiagnostic.Test, "LiveStream")]
    public class TestLiveStream : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [Alias("Url")]
        public Uri Uri { get; set; }

        /// <summary>
        /// Writes the manifest contents to this path, if specified.
        /// </summary>
        [Parameter]
        [Alias("Save")]
        public string ManifestOutputPath { get; set; }

        protected override void ProcessRecord()
        {
            LiveStream.Validate(Uri, new PowerShellFeedback(this, ManifestOutputPath));
        }

        private sealed class PowerShellFeedback : IFeedbackSink
        {
            public PowerShellFeedback(Cmdlet cmdlet, string manifestOutputPath)
            {
                _cmdlet = cmdlet;
                _manifestOutputPath = manifestOutputPath;
            }

            private readonly Cmdlet _cmdlet;
            private readonly string _manifestOutputPath;

            public void Info(string message)
            {
                _cmdlet.WriteVerbose(message);
            }

            public void InvalidContent(string message)
            {
                // The output from this is ugly and verbose but that's PowerShell for you.
                _cmdlet.WriteError(new ErrorRecord(new Exception(message), "InvalidContent", ErrorCategory.InvalidData, null));
            }

            public void WillSkipSomeData(string message)
            {
                _cmdlet.WriteWarning(message);
            }

            public void DownloadedManifest(string contents)
            {
                if (string.IsNullOrWhiteSpace(_manifestOutputPath))
                    return;

                var directory = Path.GetDirectoryName(_manifestOutputPath);
                if (!string.IsNullOrWhiteSpace(directory))
                    Directory.CreateDirectory(directory);

                File.WriteAllText(_manifestOutputPath, contents);

                _cmdlet.WriteVerbose("Manifest saved to " + _manifestOutputPath);
            }
        }
    }
}