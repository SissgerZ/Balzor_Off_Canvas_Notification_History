using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using System.Text.Json.Serialization;

namespace Balzor_Off_Canvas_Notification_History.Models;

public class TimedToastMessage : ToastMessage
{
    public DateTime Occurrence { get; set; }

    public IconColor IconColor { get; set; }

    public Guid Id { get; set; }

    // prevent serialization issues with json ignore
    [JsonIgnore]
    public new RenderFragment? Content { get; set; }

    public TimedToastMessage()
    {
        // required for deserialization
    }

    public TimedToastMessage(ToastMessage toastMessage,
                             IconColor iconColor,
                             DateTime occurrence)
    {
        AutoHide = toastMessage.AutoHide;
        CustomIconName = toastMessage.CustomIconName;
        HelpText = toastMessage.HelpText;
        IconName = toastMessage.IconName;
        Message = toastMessage.Message;
        Content = toastMessage.Content;
        Title = toastMessage.Title;
        Type = toastMessage.Type;

        Occurrence = occurrence;
        IconColor = iconColor;

        try
        {
            var toastMessageType = typeof(ToastMessage);
            var idField = toastMessageType.GetField("Id", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            Id = idField?.GetValue(toastMessage) as Guid? ?? Guid.NewGuid();
        }
        catch
        {
            // intentionally ignored
        }
    }
}
