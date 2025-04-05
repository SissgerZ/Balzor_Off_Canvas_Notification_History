using BlazorBootstrap;

namespace Balzor_Off_Canvas_Notification_History.Models;

public class TimedToastMessage : ToastMessage
{
    public DateTime Occurrence { get; set; }

    public IconColor IconColor { get; set; }

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

        try
        {
            var toastMessageType = typeof(ToastMessage);
            var elementIdProp = toastMessageType.GetProperty("ElementId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var idField = toastMessageType.GetField("Id", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            elementIdProp?.SetValue(this, elementIdProp.GetValue(toastMessage));
            idField?.SetValue(this, idField.GetValue(toastMessage));
        }
        catch
        {
            // intentionally ignored
        }

        Occurrence = occurrence;
        IconColor = iconColor;
    }
}
