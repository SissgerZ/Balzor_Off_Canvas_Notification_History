using Balzor_Off_Canvas_Notification_History.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace Balzor_Off_Canvas_Notification_History.Services;

public class ToastStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public ToastStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SaveToastMessage(TimedToastMessage message)
    {
        if (message == null)
            return;

        if (message.Id == Guid.Empty)
            return;

        var json = JsonSerializer.Serialize(message, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        await _jsRuntime.InvokeVoidAsync("indexedDBHelper.saveMessage", JsonSerializer.Deserialize<object>(json));
    }

    public async Task<List<TimedToastMessage>> LoadToastMessages()
    {
        return await _jsRuntime.InvokeAsync<List<TimedToastMessage>>("indexedDBHelper.loadMessages") ?? [];
    }

    public async Task RemoveAllToastMessages()
    {
        await _jsRuntime.InvokeVoidAsync("indexedDBHelper.removeAllMessages");
    }

    public async Task RemoveToastMessage(Guid messageId)
    {
        await _jsRuntime.InvokeVoidAsync("indexedDBHelper.removeMessageById", messageId);
    }
}
