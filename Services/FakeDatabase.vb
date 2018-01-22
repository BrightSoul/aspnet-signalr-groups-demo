Imports System.Collections.Concurrent
Imports AspNetSignalRGroupsDemo.AspNetSignalRGroupsDemo.Models

Namespace AspNetSignalRGroupsDemo.Services
    Public Class FakeDatabase
        Private Shared Messages As ConcurrentBag(Of Message) = New ConcurrentBag(Of Message)
        Private Shared ChatRooms As List(Of ChatRoom) = New List(Of ChatRoom) From {
            New ChatRoom With {.Id = "ChatRoom1", .Name = "Sport"},
            New ChatRoom With {.Id = "ChatRoom2", .Name = "Travels"},
            New ChatRoom With {.Id = "ChatRoom3", .Name = "Culture"},
            New ChatRoom With {.Id = "ChatRoom4", .Name = "News"}
        }

        Public Shared Function GetChatRooms() As IEnumerable(Of ChatRoom)
            Return ChatRooms
        End Function

        Public Shared Function GetMessagesForChatRoom(chatRoomId As String) As IEnumerable(Of Message)
            Return Messages.Where(Function(message) message.ChatRoomId = chatRoomId).OrderBy(Function(message) message.Sent)
        End Function

        Public Shared Sub SaveMessage(message As Message)
            Messages.Add(message)
        End Sub

    End Class
End Namespace