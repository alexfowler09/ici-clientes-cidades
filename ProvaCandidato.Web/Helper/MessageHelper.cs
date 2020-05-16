using System.Web.Mvc;

namespace ProvaCandidato.Helper
{
    public static class MessageHelper
    {
        public static void DisplaySuccessMessage(Controller controller, string message, MessageType messageType)
        {
            var userMessage = new UserMessage
            {
                Message = message
            };

            if (messageType == MessageType.Success)
            {
                userMessage.CssClassName = "alert alert-success";
                userMessage.Title = "Sucesso";
            }
            else
            {
                userMessage.CssClassName = "alert alert-danger";
                userMessage.Title = "Erro";
            }

            controller.TempData["UserMessage"] = userMessage;            
        }
    }

    public class UserMessage
    {
        public string CssClassName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }        
    }

    public enum MessageType
    {
        Success = 1,
        Error = 2
    }
}