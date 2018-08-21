namespace Api.Helpers
{
    using System;
    using System.Collections.Generic;

    public static class ExceptionExtensions
    {
        /// <summary>
        ///     Recursively unpacks an <see cref="Exception" />, yielding the
        ///     exception itself as well as every nested
        ///     <see cref="Exception.InnerException" />.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static IEnumerable<Exception> GetExceptions(
            this Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            Exception ex = exception;

            do
            {
                yield return ex;
                ex = ex.InnerException;
            }
            while (ex != null);
        }

        /// <summary>
        ///     Recursively unpacks an <see cref="Exception" />, yielding
        ///     every nested <see cref="Exception.InnerException" />.
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static IEnumerable<Exception> GetInnerExceptions(
            this Exception exception)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception));

            Exception ex = exception.InnerException;

            while (ex != null)
            {
                yield return ex;
                ex = ex.InnerException;
            }
        }
    }
}