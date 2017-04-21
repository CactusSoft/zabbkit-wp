using System;
using System.Net;
using System.Threading.Tasks;

namespace CactusSoft.Stierlitz.Application.Helpers
{
    public static class Executer
    {
        public static async Task Execute(Func<Task> task)
        {
            try
            {
                await task();
                return;
            }
            catch (WebException e)
            {
                if (e.Status != WebExceptionStatus.RequestCanceled)
                {
                    throw;
                }
            }
            await Execute(task);
        }

        public static async Task<T> Execute<T>(Func<Task<T>> task)
        {
            try
            {
                return await task();
            }
            catch (WebException e)
            {
                if (e.Status != WebExceptionStatus.RequestCanceled)
                {
                    throw;
                }
            }
            return await Execute(task);
        }
    }
}
