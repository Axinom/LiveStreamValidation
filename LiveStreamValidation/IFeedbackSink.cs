namespace Axinom.LiveStreamValidation
{
    /// <summary>
    /// Receives validation feedback to be supplied to the user via some meaningful channel.
    /// </summary>
    public interface IFeedbackSink
    {
        /// <summary>
        /// Reports an informational message.
        /// </summary>
        void Info(string message);

        /// <summary>
        /// Reports that some part of the stream cannot be validated but that validation overall can proceed
        /// and the validator will simply skip some data. Report this as a bug to get it fixed.
        /// </summary>
        void WillSkipSomeData(string message);

        /// <summary>
        /// Reports that the content is not a valid live stream. The message contains detailed reasoning.
        /// Any number of errors may be reported during a validation pass.
        /// </summary>
        void InvalidContent(string message);

        /// <summary>
        /// Reports the manifest that was downloaded.
        /// </summary>
        void DownloadedManifest(string contents);
    }
}
