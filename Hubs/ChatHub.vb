Imports System.Net
Imports System.Threading.Tasks
Imports AspNetSignalRGroupsDemo.AspNetSignalRGroupsDemo.Models
Imports AspNetSignalRGroupsDemo.AspNetSignalRGroupsDemo.Services
Imports Microsoft.AspNet.SignalR
Namespace AspNetSignalRGroupsDemo.Hubs
    Public Class ChatHub
        Inherits Hub

        Public Async Function JoinChatRoom(chatRoomId As String) As Task
            'L'utente si sta unendo alla chat, mandiamogli i messaggi pregressi
            Dim messages = FakeDatabase.GetMessagesForChatRoom(chatRoomId)
            For Each message In messages
                Clients.Caller.ReceiveMessage(message)
            Next

            'Inviamo una notifica agli altri
            Dim notificationMessage = New Message() With {
            .ChatRoomId = chatRoomId,
                .Sent = DateTimeOffset.Now,
                .Text = Context.User.Identity.Name & " has entered the chat room",
                .Username = "ChatBot"
            }
            FakeDatabase.SaveMessage(notificationMessage)
            Clients.Group(chatRoomId).ReceiveMessage(notificationMessage)

            'Finalmente aggiungiamo l'utente al gruppo
            Await Groups.Add(Context.ConnectionId, chatRoomId)

        End Function

        Public Async Function LeaveChatRoom(chatRoomId As String) As Task
            Await Groups.Remove(Context.ConnectionId, chatRoomId)
            'Inviamo una notifica agli altri ancora presenti in chat
            Dim notificationMessage = New Message() With {
                .ChatRoomId = chatRoomId,
                .Sent = DateTimeOffset.Now,
                .Text = Context.User.Identity.Name & " has left the chat room",
                .Username = "ChatBot"
            }
            FakeDatabase.SaveMessage(notificationMessage)
            Clients.Group(chatRoomId).ReceiveMessage(notificationMessage)
        End Function

        Public Sub SendMessage(message As Message)
            message.Username = Context.User.Identity.Name
            message.Sent = DateTimeOffset.Now
            message.Text = WebUtility.HtmlEncode(message.Text)
            'Un utente ha inviato un messaggio, salviamolo nel db
            FakeDatabase.SaveMessage(message)
            'Poi lo inoltriamo agli altri utenti del gruppo
            Clients.Group(message.ChatRoomId).ReceiveMessage(message)
        End Sub

        Public Overrides Async Function OnConnected() As Task
            Await MyBase.OnConnected()
            Dim chatRooms = FakeDatabase.GetChatRooms()
            Clients.Caller.ReceiveChatRooms(chatRooms)
        End Function

    End Class
End Namespace
