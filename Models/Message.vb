Namespace AspNetSignalRGroupsDemo.Models
    Public Class Message
        Private m_ChatRoomId As String
        Public Property ChatRoomId() As String
            Get
                Return m_ChatRoomId
            End Get
            Set(ByVal value As String)
                m_ChatRoomId = value
            End Set
        End Property
        Private m_Username As String
        Public Property Username() As String
            Get
                Return m_Username
            End Get
            Set(ByVal value As String)
                m_Username = value
            End Set
        End Property
        Private m_sent As DateTimeOffset
        Public Property Sent() As DateTimeOffset
            Get
                Return m_sent
            End Get
            Set(ByVal value As DateTimeOffset)
                m_sent = value
            End Set
        End Property
        Private m_Text As String
        Public Property Text() As String
            Get
                Return m_Text
            End Get
            Set(ByVal value As String)
                m_Text = value
            End Set
        End Property
    End Class
End Namespace
