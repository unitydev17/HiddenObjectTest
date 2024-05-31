using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Code
{
    public static class UnityWebRequestExtension
    {
        // public static TaskAwaiter<UnityWebRequest.Result> GetAwaiter(this UnityWebRequestAsyncOperation reqOp)
        // {
        //     var tsc = new TaskCompletionSource<UnityWebRequest.Result>();
        //     reqOp.completed += asyncOp => tsc.TrySetResult(reqOp.webRequest.result);
        //
        //     if (reqOp.isDone)
        //         tsc.TrySetResult(reqOp.webRequest.result);
        //
        //     return tsc.Task.GetAwaiter();
        // }
    }
}