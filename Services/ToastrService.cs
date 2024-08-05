using Microsoft.JSInterop;
using System.Threading.Tasks;

public class ToastrService
{
    private readonly IJSRuntime _jsRuntime;

    public ToastrService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task Success(string message, string title = "")
    {
        await _jsRuntime.InvokeVoidAsync("toastr.success", message, title);
    }

    public async Task Error(string message, string title = "")
    {
        await _jsRuntime.InvokeVoidAsync("toastr.error", message, title);
    }

    public async Task Warning(string message, string title = "")
    {
        await _jsRuntime.InvokeVoidAsync("toastr.warning", message, title);
    }

    public async Task Info(string message, string title = "")
    {
        await _jsRuntime.InvokeVoidAsync("toastr.info", message, title);
    }
}
