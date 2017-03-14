using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamNucleus.Heos
{
    public static class ReactiveExtensions
    {
        public static IObservable<TSource> RetryAfterDelay<TSource, TException>(this IObservable<TSource> source, TimeSpan retryDelay, int retryCount)
            where TException : Exception
        {
            return source.Catch<TSource, TException>(exception =>
            {
                if (retryCount <= 0)
                {
                    return Observable.Throw<TSource>(exception);
                }

                return source.DelaySubscription(retryDelay)
                    .RetryAfterDelay<TSource, TException>(retryDelay, --retryCount);
            });
        }

        // From http://stackoverflow.com/questions/15185863/taking-a-snapshot-of-replaysubjectt-buffer
        // From http://stackoverflow.com/questions/21011688/taking-a-snapshot-of-an-iobservablet
        public static List<T> TakeSynchronousNotifications<T>(this IObservable<T> source)
        {
            var synchronousNotifications = new List<T>();
            using (source.Subscribe(synchronousNotifications.Add))
            {
                // Noop
            }
            return synchronousNotifications;
        }
    }
}
